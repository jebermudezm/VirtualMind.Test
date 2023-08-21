using Microsoft.EntityFrameworkCore;
using VirtualMind.Test.Contracts.ServiceLibrary;
using VirtualMind.Test.EF.Context;
using VirtualMind.Test.EF.Repository;
using VirtualMind.Test.ExternalServices;
using VirtualMind.Test.Impl.ServiceLibrary;
using VirtualMind.Test.Library.Contracts;
using VirtualMind.Test.Library.Wraper;

namespace VirtualMind.Test.WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMainModules(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("VirtualMind.DBConnection")));
            return services;
        }

        public static IServiceCollection AddServiceModules(this IServiceCollection services)
        {
            services.AddTransient<IExternalCurrencyService, ExchangeRateService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<HttpClient>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            return services;
        }

        public static IServiceCollection AddRepositoryModules(this IServiceCollection services)
        {
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
