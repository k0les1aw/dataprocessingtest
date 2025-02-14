using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Core;

namespace TransactionProcessing.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet TransactionProcessing.App.dll <file_path>");
                return;
            }

            string filePath = args[0];

            var services = ServiceConfiguration.ConfigureServices();

            App app = services.GetRequiredService<App>();
            await app.Start($"/app/data/{filePath}");
        }
    }

    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly TransactionProcessor _processor;

        public App(
            [NotNull] ILogger<App> logger,
            [NotNull] TransactionProcessor processor)
        {
            _logger = logger;
            _processor = processor;
        }

        public async Task Start(string filepath)
        {
            _logger.LogInformation("App started...");

            await _processor.ExtractAndProcess(filepath);

            _logger.LogInformation("App closed.");
        }
    }
}
