using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TransactionProcessing.Infrastructure.Transactions
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            // Configure the primary key for the TransactionEntity
            builder.HasKey(u => u.TransactionId);

            // Index on the Category column to Category-based analytics
            builder.HasIndex(u => u.Category);

            // Index on the UserId and Amount columns to User-based analytics
            builder.HasIndex(u => new { u.UserId, u.Amount });
        }
    }
}
