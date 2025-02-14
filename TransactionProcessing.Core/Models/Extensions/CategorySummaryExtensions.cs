using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Core
{
    /// <summary>
    /// Provides extension methods for the CategorySummaryDto class.
    /// </summary>
    public static class CategorySummaryExtensions
    {
        /// <summary>
        /// Converts a CategorySummaryDto object to a CategorySummary object.
        /// </summary>
        /// <param name="dto">The CategorySummaryDto to convert. Cannot be null.</param>
        /// <returns>A CategorySummary object that represents the category summary.</returns>
        public static CategorySummary ToCategorySummary([NotNull] this CategorySummaryDto dto)
        {
            return new CategorySummary
            {
                Category = dto.Category,
                TransactionsCount = dto.TransactionsCount
            };
        }
    }
}