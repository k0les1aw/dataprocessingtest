namespace TransactionProcessing.Core
{
    /// <summary>
    /// Represents the analytics report.
    /// </summary>
    public class AnalyticsReport
    {
        /// <summary>
        /// Gets or sets the summary of users.
        /// </summary>
        public IEnumerable<UserSummary> UsersSummary { get; set; }

        /// <summary>
        /// Gets or sets the top categories.
        /// </summary>
        public IEnumerable<CategorySummary> TopCategories { get; set; }

        /// <summary>
        /// Gets or sets the highest spender.
        /// </summary>
        public UserSummary HighestSpender { get; set; }
    }
}
