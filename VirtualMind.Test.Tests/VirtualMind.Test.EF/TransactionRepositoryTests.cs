public class TransactionRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _contextOptions;
    private readonly AppDbContext _context;
    private readonly TransactionRepository _repository;

    public TransactionRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabaseTrans")
            .Options;

        _context = new AppDbContext(_contextOptions);
        _repository = new TransactionRepository(_context);
    }

    [Fact]
    public async Task CanPurchase_ReturnsCorrectAmountForUserAndCurrency()
    {
        // Arrange
        var userId = 1;
        var currencyCode = "USD";
        var expectedTransaction = new Transaction { UserId = userId, CurrencyCode = currencyCode, TransactionDate = DateTime.Now, PurchasedAmount = 50 };
        _context.Add(expectedTransaction);
        _context.SaveChanges();

        // Act
        var result = await _repository.CanPurchase(userId, currencyCode);

        // Assert
        Assert.Equal(50M, result);
    }

    [Fact]
    public async Task CreateTransaction_ReturnsTrue()
    {
        // Arrange
        var userId = 1;
        var currencyCode = "BRL";
        // Act
        var result = await _repository.CreateTransaction(userId, currencyCode,50,10);

        // Assert
        Assert.Equal(result.UserId, userId);
        Assert.Equal(result.CurrencyCode, currencyCode);
        Assert.Equal(result.PurchasedAmount, 10M) ;
        Assert.Equal(result.AmountInPeso, 50M) ;
    }


}
