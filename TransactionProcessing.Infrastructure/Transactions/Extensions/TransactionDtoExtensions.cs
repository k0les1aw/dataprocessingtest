using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Infrastructure.Transactions
{
    public static class TransactionDtoExtensions
    {
        /// <summary>
        /// Converts a TransactionDto object to a TransactionEntity object.
        /// </summary>
        /// <param name="dto">The TransactionDto to convert. Cannot be null.</param>
        /// <returns>A TransactionEntity object that represents the transaction.</returns>
        public static TransactionEntity ToEntity([NotNull] this TransactionDto dto)
        {
            return new TransactionEntity
            {
                TransactionId = dto.TransactionId,
                UserId = dto.UserId,
                Date = dto.Date,
                Amount = dto.Amount,
                Category = dto.Category,
                Description = dto.Description,
                Merchant = dto.Merchant
            };
        }
    }
}
