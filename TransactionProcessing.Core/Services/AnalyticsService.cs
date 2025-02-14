using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Core
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ILogger<AnalyticsService> _logger;
        private readonly IOptions<TransactionProcessSettings> _processSettings;
        private readonly ITransactionRepository _transactionRepository;

        public AnalyticsService(
            [NotNull] ILogger<AnalyticsService> logger,
            [NotNull] IOptions<TransactionProcessSettings> processSettings,
            [NotNull] ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _processSettings = processSettings;
            _transactionRepository = transactionRepository;
        }

        public async Task<AnalyticsReport> GenerateReportAsync()
        {
            _logger.LogInformation("Start report generating...");
            int topCategoriesCount = _processSettings.Value.TopCategoriesCount;

            var userSummary = await _transactionRepository.GetUserSummaryAsync();
            var topCategories = await _transactionRepository.GetTopCategoriesAsync(topCategoriesCount);
            var highestSpender = await _transactionRepository.GetHighestSpenderAsync();

            return new AnalyticsReport
            {
                UsersSummary = userSummary.Select(x => x.ToUserSummary()).ToList(),
                TopCategories = topCategories.Select(x => x.ToCategorySummary()).ToList(),
                HighestSpender = highestSpender.ToUserSummary()
            };
        }
    }
}