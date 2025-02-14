using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using TransactionProcessing.Infrastructure.Transactions;

namespace TransactionProcessing.Infrastructure
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionsDbContext _context;
        private readonly ILogger<TransactionRepository> _log;

        public TransactionRepository(
            [NotNull] TransactionsDbContext context,
            [NotNull] ILogger<TransactionRepository> log)
        {
            _context = context;
            _log = log;
        }

        public async Task SaveTransactionsBatchAsync([NotNull] List<TransactionDto> transactions)
        {
            try
            {
                var entities = transactions.Select(x => x.ToEntity()).ToArray();
                await _context.Transactions.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                _log.LogInformation($"Inserted batch of {transactions.Count} transactions.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error inserting batch: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserSummaryDto>> GetUserSummaryAsync()
        {
            return await _context.Transactions
                .Where(t => t.Amount != 0) // Only include transactions with an amount
                .GroupBy(t => t.UserId)
                .Select(g => new UserSummaryDto
                {
                    UserId = g.Key,
                    TotalIncome = g.Where(t => t.Amount > 0).Sum(t => t.Amount),
                    TotalExpense = g.Where(t => t.Amount < 0).Sum(t => t.Amount)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategorySummaryDto>> GetTopCategoriesAsync(int topCategories)
        {
            return await _context.Transactions
                .GroupBy(t => t.Category)
                .Select(g => new CategorySummaryDto
                {
                    Category = g.Key,
                    TransactionsCount = g.Count()
                })
                .OrderByDescending(c => c.TransactionsCount) // Top categories first
                .Take(topCategories) // Top 3
                .ToListAsync();
        }

        public async Task<UserSummaryDto> GetHighestSpenderAsync()
        {
            return await _context.Transactions
                .Where(t => t.Amount < 0) // Only expenses
                .GroupBy(t => t.UserId)
                .Select(g => new UserSummaryDto
                {
                    UserId = g.Key,
                    TotalIncome = g.Where(t => t.Amount > 0).Sum(t => t.Amount),
                    TotalExpense = g.Sum(t => t.Amount)
                })
                .OrderByDescending(u => u.TotalExpense) // Highest spender first
                .FirstOrDefaultAsync();
        }
    }
}
