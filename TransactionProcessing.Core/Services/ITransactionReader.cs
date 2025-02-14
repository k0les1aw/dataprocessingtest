using System.Diagnostics.CodeAnalysis;

namespace TransactionProcessing.Core
{
    /// <summary>
    /// Interface for reading transactions from a file.
    /// </summary>
    /// <typeparam name="T">The type of transaction.</typeparam>
    public interface ITransactionReader<T>
    {
        /// <summary>
        /// Reads transactions from the specified file.
        /// </summary>
        /// <param name="filepath">The path to the file containing transactions.</param>
        /// <returns>An enumerable collection of transactions.</returns>
        IAsyncEnumerable<T> ReadFromFileAsync([NotNull] string filepath);

        // "Url-based" variant 
        // IAsyncEnumerable<T> ReadFromUrl([NotNull] string url);
    }
}
