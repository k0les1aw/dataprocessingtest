using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TransactionProcessing.Core;
using TransactionProcessing.Core.Services;
using TransactionProcessing.Infrastructure.Transactions;
using Xunit;

namespace TransactionProcessing.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<ILogger<TransactionProcessor>> _loggerMock;
        private readonly Mock<IOptions<TransactionProcessSettings>> _processSettingsMock;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _loggerMock = new Mock<ILogger<TransactionProcessor>>();
            _processSettingsMock = new Mock<IOptions<TransactionProcessSettings>>();
            _processSettingsMock.Setup(x => x.Value).Returns(new TransactionProcessSettings { BatchSize = 100 });
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_loggerMock.Object, _processSettingsMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task SaveTransactionsAsync_ShouldSaveTransactions_Success()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), UserId = Guid.NewGuid(), Date = DateTime.Now, Amount = 100, Category = "Food" },
                new Transaction { TransactionId = Guid.NewGuid(), UserId = Guid.NewGuid(), Date = DateTime.Now, Amount = 200, Category = "Travel" }
            };

            var dtos = transactions.Select(x => x.ToTransactionDto()).ToList();

            // Act
            await _transactionService.SaveTransactionsAsync(transactions.ToAsyncEnumerable());

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.SaveTransactionsBatchAsync(ItIs.EquivalentTo(dtos)), Times.Once);
        }
    }
}