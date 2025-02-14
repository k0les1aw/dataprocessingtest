using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TransactionProcessing.Core;
using TransactionProcessing.Core.Services;
using TransactionProcessing.Infrastructure;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.App
{
    internal static class ServiceConfiguration
    {
        internal static ServiceProvider ConfigureServices()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging(options =>
                {
                    options.ClearProviders();
                    options.AddConsole();
                })
                .AddDbContext<TransactionsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddTransient<ITransactionRepository, TransactionRepository>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<IAnalyticsService, AnalyticsService>()
                .AddTransient(typeof(ITransactionReader<>), typeof(CsvTransactionReader<>))
                .AddTransient<TransactionProcessor>()
                .Configure<TransactionProcessSettings>(Configuration.GetSection("TransactionProcessSettings"))
                .AddSingleton<App>()
                .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TransactionsDbContext>();
            dbContext.Database.Migrate();


            return serviceProvider;
        }
    }
}
