using System;
using System.Text.Json;
using VirtualMind.Test.Contracts.ServiceLibrary;
using VirtualMind.Test.Library.Wraper;

namespace VirtualMind.Test.ExternalServices
{
    public class ExchangeRateService : IExternalCurrencyService
    {
        private readonly IHttpClientWrapper _httpClient;
        public ExchangeRateService(IHttpClientWrapper httpClient) 
        {
            _httpClient = httpClient;
        }
        public async Task<decimal?> GetExchangeRate(string currencyCode)
        {
            switch (currencyCode.ToUpper())
            {
                case "USD":
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://www.bancoprovincia.com.ar/Principal/Dolar");
                    var response = await ReadContentAs(await _httpClient.SendAsync(request));

                    decimal usdRate;
                    if (decimal.TryParse(response.FirstOrDefault(), out usdRate))
                    {
                        return usdRate;
                    }
                    break;

                case "BRL":
                    var usdForBrl = await GetExchangeRate("USD");
                    if (usdForBrl.HasValue)
                    {
                        return usdForBrl.Value / 4;
                    }
                    break;

                default:
                    return null;
            }

            return null;
        }


        private async Task<List<string>> ReadContentAs(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<List<string>>(content);
        }
    }
}