using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Infrastructure
{
    public class TransactionsDbContext : DbContext
    {
        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : base(options) 
        { }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TransactionEntityConfiguration().Configure(modelBuilder.Entity<TransactionEntity>());
        }
    }
}
