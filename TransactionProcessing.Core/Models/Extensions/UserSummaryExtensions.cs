using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Core
{
    /// <summary>
    /// Provides extension methods for the UserSummaryDto class.
    /// </summary>
    public static class UserSummaryExtensions
    {
        /// <summary>
        /// Converts a UserSummaryDto object to a UserSummary object.
        /// </summary>
        /// <param name="dto">The UserSummaryDto to convert. Cannot be null.</param>
        /// <returns>A UserSummary object that represents the user summary.</returns>
        public static UserSummary ToUserSummary([NotNull] this UserSummaryDto dto)
        {

            return new UserSummary
            {
                UserId = dto.UserId,
                TotalIncome = dto.TotalIncome,
                TotalExpense = dto.TotalExpense
            };
        }
    }
}