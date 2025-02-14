using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using TransactionProcessing.Core.Services;

namespace TransactionProcessing.Core
{
    public class TransactionProcessor 
    {
        private readonly ILogger<TransactionProcessor> _logger;
        private readonly IOptions<TransactionProcessSettings> _processSettings;
        private readonly ITransactionService _transactionService;
        private readonly IAnalyticsService _analyticsService;
        private readonly ITransactionReader<Transaction> _transactionReader;
        public TransactionProcessor(
            [NotNull] ILogger<TransactionProcessor> logger,
            [NotNull] IOptions<TransactionProcessSettings> processSettings,
            [NotNull] ITransactionService transactionService,
            [NotNull] IAnalyticsService analyticsService,
            [NotNull] ITransactionReader<Transaction> transactionReader)
        {
            _logger = logger;
            _processSettings = processSettings;
            _transactionService = transactionService;
            _analyticsService = analyticsService;
            _transactionReader = transactionReader;
        }

        public async Task ExtractAndProcess([NotNull] string filepath)
        {
            _logger.LogInformation("Start extraction from file...");
            var transactions = _transactionReader.ReadFromFileAsync(filepath);

            _logger.LogInformation("Save transactions...");
            await _transactionService.SaveTransactionsAsync(transactions);

            _logger.LogInformation("Report generation...");
            var report = await _analyticsService.GenerateReportAsync();

            _logger.LogInformation("Saving report...");
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(report, serializeOptions);

            string reportFilepath = _processSettings.Value.ReportFilePath;
            await File.WriteAllTextAsync(reportFilepath, json);
            _logger.LogInformation("Report saved!");
        }
    }
}
