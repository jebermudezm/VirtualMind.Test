
using VirtualMind.Test.Contracts.ServiceLibrary.Params;

public class TransactionsControllerTests
{
    private readonly Mock<ITransactionService> _mockTransactionService;
    private readonly TransactionsController _controller;

    public TransactionsControllerTests()
    {
        _mockTransactionService = new Mock<ITransactionService>();
        _controller = new TransactionsController(_mockTransactionService.Object);
    }

    [Fact]
    public async Task Purchase_ValidInput_CreatesTransaction()
    {
        // Arrange
        var parameters = new ParameterTransaction { UserId = 1, CurrencyCode = "USD", AmountInPeso = 1000m};
        var mockTransaction = new Transaction { Id = 123 }; 

        _mockTransactionService.Setup(s => s.CanPurchase(parameters.UserId, parameters.CurrencyCode, parameters.AmountInPeso))
            .ReturnsAsync(true);
        _mockTransactionService.Setup(s => s.CreateTransaction(parameters.UserId, parameters.CurrencyCode, parameters.AmountInPeso))
            .ReturnsAsync(mockTransaction);

        // Act
        var result = await _controller.Purchase(parameters);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.Purchase), createdResult.ActionName);
        Assert.Equal(mockTransaction, createdResult.Value);
    }

    [Fact]
    public async Task Purchase_ExceedsLimit_ReturnsBadRequest()
    {
        // Arrange
        var parameters = new ParameterTransaction { UserId = 1, CurrencyCode = "USD", AmountInPeso = 5000m };

        _mockTransactionService.Setup(s => s.CanPurchase(parameters.UserId, parameters.CurrencyCode, parameters.AmountInPeso))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Purchase(parameters);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Purchase exceeds the monthly limit for the selected currency.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetExchangeRate_ValidCurrency_ReturnsRate()
    {
        // Arrange
        var currencyCode = "USD";
        var mockRate = 1.2m;

        _mockTransactionService.Setup(s => s.CalculatePurchasedAmount(currencyCode, 1M))
            .ReturnsAsync(mockRate);

        // Act
        var result = await _controller.GetExchangeRate(currencyCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockRate, okResult.Value);
    }

    [Fact]
    public async Task GetExchangeRate_InvalidCurrency_ReturnsBadRequest()
    {
        // Arrange
        var currencyCode = "INVALID";
        var exceptionMessage = "Invalid currency code.";

        _mockTransactionService.Setup(s => s.CalculatePurchasedAmount(currencyCode, 1M))
            .ThrowsAsync(new ArgumentException(exceptionMessage));

        // Act
        var result = await _controller.GetExchangeRate(currencyCode);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, badRequestResult.Value);
    }
}

