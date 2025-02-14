using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Infrastructure
{
    [ExcludeFromCodeCoverage]
    [Obsolete("This class is needed to run \"dotnet ef...\" commands from command line on development. Do not use directly.")]
    internal class TransactionsDbContextFactory : IDesignTimeDbContextFactory<TransactionsDbContext>
    {
        private const string DesignTimeConnectionString = "Server=.\\SQLEXPRESS; Database=TransactionsDb; Trusted_Connection=True;";

        public TransactionsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TransactionsDbContext>();
            builder.UseSqlServer(DesignTimeConnectionString);
            return new TransactionsDbContext(builder.Options);
        }
    }
}
