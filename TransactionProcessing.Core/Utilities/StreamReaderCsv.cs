using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Core
{
    public static class StreamReaderCsv
    {
        const string CsvFileExtension = ".csv";

        public static StreamReader GetStreamReader([NotNull] string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("File path cannot be null or empty", nameof(filepath));
            }
            if (!IsCsvFilePath(filepath))
            {
                throw new ArgumentException($"File should have {CsvFileExtension} extension");
            }
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"File not found: {filepath}");
            }

            return new StreamReader(filepath);
        }

        private static bool IsCsvFilePath(string filePath)
        {
            return filePath.EndsWith(CsvFileExtension, StringComparison.OrdinalIgnoreCase);
        }
    }

}
