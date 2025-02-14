namespace TransactionProcessing.Core
{
    /// <summary>
    /// Interface for analytics service.
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Generates a report asynchronously.
        /// </summary>
        /// <returns>A analytics report.</returns>
        Task<AnalyticsReport> GenerateReportAsync();
    }
}