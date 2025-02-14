using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Core.Services
{
    public interface ITransactionService
    {
        Task SaveTransactionsAsync([NotNull] IAsyncEnumerable<Transaction> transactions);
    }
}
