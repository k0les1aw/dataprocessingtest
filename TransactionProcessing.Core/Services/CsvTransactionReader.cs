using CsvHelper;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TransactionProcessing.Core
{
    public class CsvTransactionReader<T> : ITransactionReader<T>
    {
        public async IAsyncEnumerable<T> ReadFromFileAsync([NotNull] string filepath)
        {
            using (StreamReader reader = StreamReaderCsv.GetStreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await foreach (var record in csv.GetRecordsAsync<T>())
                {
                    yield return record;
                }
            }
        }
    }
}
