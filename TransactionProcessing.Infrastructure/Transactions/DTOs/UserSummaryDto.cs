namespace TransactionProcessing.Infrastructure.Transactions
{
    public class UserSummaryDto
    {
        public Guid UserId { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
