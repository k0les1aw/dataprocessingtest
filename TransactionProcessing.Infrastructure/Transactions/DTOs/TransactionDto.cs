namespace TransactionProcessing.Infrastructure.Transactions
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Merchant { get; set; } = string.Empty;
    }
}
