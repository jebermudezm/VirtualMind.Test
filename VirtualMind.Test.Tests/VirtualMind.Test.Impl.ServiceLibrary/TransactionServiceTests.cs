public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _transactionRepoMock;
    private readonly Mock<IExternalCurrencyService> _externalCurrencyServiceMock;
    private readonly TransactionService _transactionService;

    public TransactionServiceTests()
    {
        _transactionRepoMock = new Mock<ITransactionRepository>();
        _externalCurrencyServiceMock = new Mock<IExternalCurrencyService>();
        _transactionService = new TransactionService(_transactionRepoMock.Object, _externalCurrencyServiceMock.Object);
    }

    [Fact]
    public async Task TestCalculatePurchasedAmount_USD()
    {
        string currencyCode = "USD";
        decimal amountInPeso = 2000;
        decimal exchangeRate = 100;
        _externalCurrencyServiceMock.Setup(m => m.GetExchangeRate(currencyCode)).ReturnsAsync(exchangeRate);

        var result = await _transactionService.CalculatePurchasedAmount(currencyCode, amountInPeso);

        Assert.Equal(20, result);
    }

    [Fact]
    public async Task TestCanPurchase_WithinLimit()
    {
        int userId = 1;
        string currencyCode = "USD";
        decimal amountInPeso = 2000;
        decimal currentPurchases = 100;
        decimal exchangeRate = 100;

        _transactionRepoMock.Setup(m => m.CanPurchase(userId, currencyCode)).ReturnsAsync(currentPurchases);
        _externalCurrencyServiceMock.Setup(m => m.GetExchangeRate(currencyCode)).ReturnsAsync(exchangeRate);

        var result = await _transactionService.CanPurchase(userId, currencyCode, amountInPeso);

        Assert.True(result);
    }

    [Fact]
    public async Task TestCreateTransaction()
    {
        int userId = 1;
        string currencyCode = "USD";
        decimal amountInPeso = 2000;
        decimal exchangeRate = 100;

        var expectedTransaction = new Transaction(); 

        _externalCurrencyServiceMock.Setup(m => m.GetExchangeRate(currencyCode)).ReturnsAsync(exchangeRate);
        _transactionRepoMock.Setup(m => m.CreateTransaction(userId, currencyCode, amountInPeso, 20)).ReturnsAsync(expectedTransaction);

        var result = await _transactionService.CreateTransaction(userId, currencyCode, amountInPeso);

        Assert.Equal(expectedTransaction, result); 
    }
}
