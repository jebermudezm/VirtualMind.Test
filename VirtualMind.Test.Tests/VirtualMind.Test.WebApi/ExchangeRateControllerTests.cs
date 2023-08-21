
public class ExchangeRateControllerTests
{
    private readonly Mock<IExternalCurrencyService> _mockExchangeRateService;
    private readonly ExchangeRateController _controller;

    public ExchangeRateControllerTests()
    {
        _mockExchangeRateService = new Mock<IExternalCurrencyService>();
        _controller = new ExchangeRateController(_mockExchangeRateService.Object);
    }

    [Fact]
    public async Task GetExchangeRate_ValidCurrencyCode_ReturnsOk()
    {
        // Arrange
        var currencyCode = "USD";
        _mockExchangeRateService.Setup(s => s.GetExchangeRate(currencyCode))
            .ReturnsAsync(1.2m);

        // Act
        var result = await _controller.GetExchangeRate(currencyCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1.2m, okResult.Value);
    }

    [Fact]
    public async Task GetExchangeRate_InvalidCurrencyCode_ReturnsBadRequest()
    {
        // Arrange
        var currencyCode = "INVALID";
        _mockExchangeRateService.Setup(s => s.GetExchangeRate(currencyCode))
            .ReturnsAsync((decimal?)null);

        // Act
        var result = await _controller.GetExchangeRate(currencyCode);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid currency code.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetExchangeRate_ServiceThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        var currencyCode = "EXCEPTION";
        _mockExchangeRateService.Setup(s => s.GetExchangeRate(currencyCode))
            .ThrowsAsync(new Exception("Some error"));

        // Act
        var result = await _controller.GetExchangeRate(currencyCode);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.StartsWith("Internal server error:", objectResult.Value.ToString());
    }
}
