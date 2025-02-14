using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Infrastructure.Transactions
{
    /// <summary>
    /// Interface for transaction repository.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Saves a batch of transactions asynchronously.
        /// </summary>
        /// <param name="transactions">The list of transactions to save. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task SaveTransactionsBatchAsync([NotNull] List<TransactionDto> transactions);

        /// <summary>
        /// Retrieves a summary of users asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of UserSummaryDto.</returns>
        Task<IEnumerable<UserSummaryDto>> GetUserSummaryAsync();

        /// <summary>
        /// Retrieves the top categories asynchronously.
        /// </summary>
        /// <param name="topCategories">The number of top categories to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of CategorySummaryDto.</returns>
        Task<IEnumerable<CategorySummaryDto>> GetTopCategoriesAsync(int topCategories);

        /// <summary>
        /// Retrieves the highest spender asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a UserSummaryDto representing the highest spender.</returns>
        Task<UserSummaryDto> GetHighestSpenderAsync();
    }
}
