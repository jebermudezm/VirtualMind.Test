public class ExchangeRateServiceTests
{
    private readonly Mock<IHttpClientWrapper> _httpClientWrapperMock;
    private readonly ExchangeRateService _exchangeRateService;

    public ExchangeRateServiceTests()
    {
        _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
        _exchangeRateService = new ExchangeRateService(_httpClientWrapperMock.Object);
    }

    [Fact]
    public async Task TestGetUSDExchangeRate()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("[\"100.50\", \"101.50\"]")
        };

        _httpClientWrapperMock.Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _exchangeRateService.GetExchangeRate("USD");

        // Assert
        Assert.Equal(100.5m, result.Value);
    }

}
