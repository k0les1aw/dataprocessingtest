using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Core
{
    /// <summary>
    /// Provides extension methods for the Transaction class.
    /// </summary>
    public static class TransactionExtensions
    {
        /// <summary>
        /// Converts a Transaction object to a TransactionDto object.
        /// </summary>
        /// <param name="transaction">The transaction to convert. Cannot be null.</param>
        /// <returns>A TransactionDto object that represents the transaction.</returns>
        public static TransactionDto ToTransactionDto([NotNull] this Transaction transaction)
        {
            return new TransactionDto()
            {
                TransactionId = transaction.TransactionId,
                UserId = transaction.UserId,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Category = transaction.Category,
                Description = transaction.Description,
                Merchant = transaction.Merchant
            };
        }
    }
}
