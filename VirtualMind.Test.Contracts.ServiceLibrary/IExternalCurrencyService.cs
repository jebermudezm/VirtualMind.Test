namespace VirtualMind.Test.Contracts.ServiceLibrary
{
    public interface IExternalCurrencyService
    {
        Task<decimal?> GetExchangeRate(string currencyCode);
    }
}
