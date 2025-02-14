namespace TransactionProcessing.Core
{
    public class TransactionProcessSettings
    {
        public int BatchSize { get; set; }
        public int TopCategoriesCount { get; set; }
        public string ReportFilePath { get; set; }
    }
}