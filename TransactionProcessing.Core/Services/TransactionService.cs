using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionProcessor> _logger;
        private readonly IOptions<TransactionProcessSettings> _processSettings;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(
            [NotNull] ILogger<TransactionProcessor> logger,
            [NotNull] IOptions<TransactionProcessSettings> processSettings,
            [NotNull] ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _processSettings = processSettings;
            _transactionRepository = transactionRepository;
        }
        public async Task SaveTransactionsAsync([NotNull] IAsyncEnumerable<Transaction> transactions)
        {
            int batchSize = _processSettings.Value.BatchSize;
            var batch = new List<Transaction>(batchSize);

            await foreach (var transaction in transactions)
            {
                batch.Add(transaction);
                if (batch.Count >= batchSize)
                {
                    await _transactionRepository.SaveTransactionsBatchAsync(batch.Select(x => x.ToTransactionDto()).ToList());
                    batch = new List<Transaction>(batchSize); // Create a new batch
                    _logger.LogInformation("Batch processed");
                }
            }

            // Save any remaining transactions in the batch
            if (batch.Count > 0)
            {
                await _transactionRepository.SaveTransactionsBatchAsync(batch.Select(x => x.ToTransactionDto()).ToList());
                _logger.LogInformation("Remaining transactions processed");
            }
        }
    }
}
