using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TransactionProcessing.Core;
using TransactionProcessing.Infrastructure.Transactions;
using Xunit;

namespace TransactionProcessing.Tests.Services
{
    public class AnalyticsServiceTests
    {
        private readonly Mock<ILogger<AnalyticsService>> _loggerMock;
        private readonly Mock<IOptions<TransactionProcessSettings>> _processSettingsMock;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly AnalyticsService _analyticsService;

        public AnalyticsServiceTests()
        {
            _loggerMock = new Mock<ILogger<AnalyticsService>>();
            _processSettingsMock = new Mock<IOptions<TransactionProcessSettings>>();
            _transactionRepositoryMock = new Mock<ITransactionRepository>();

            var processSettings = new TransactionProcessSettings
            {
                TopCategoriesCount = 5,
                ReportFilePath = "test_report.json"
            };
            _processSettingsMock.Setup(x => x.Value).Returns(processSettings);

            _analyticsService = new AnalyticsService(
                _loggerMock.Object,
                _processSettingsMock.Object,
                _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task GenerateReportAsync_ShouldGenerateReport_Success()
        {
            // Arrange
            var userSummary = new List<UserSummaryDto>
            {
                new UserSummaryDto { UserId = Guid.NewGuid(), TotalIncome = 1000, TotalExpense = 500 },
                new UserSummaryDto { UserId = Guid.NewGuid(), TotalIncome = 2000, TotalExpense = 1500 }
            };
            var topCategories = new List<CategorySummaryDto>
            {
                new CategorySummaryDto { Category = "Food", TransactionsCount = 10 },
                new CategorySummaryDto { Category = "Travel", TransactionsCount = 5 }
            };
            var highestSpender = new UserSummaryDto { UserId = Guid.NewGuid(), TotalIncome = 3000, TotalExpense = 2500 };

            _transactionRepositoryMock.Setup(repo => repo.GetUserSummaryAsync()).ReturnsAsync(userSummary);
            _transactionRepositoryMock.Setup(repo => repo.GetTopCategoriesAsync(It.IsAny<int>())).ReturnsAsync(topCategories);
            _transactionRepositoryMock.Setup(repo => repo.GetHighestSpenderAsync()).ReturnsAsync(highestSpender);

            // Act
            var report = await _analyticsService.GenerateReportAsync();

            // Assert
            _transactionRepositoryMock.Verify(repo => repo.GetUserSummaryAsync(), Times.Once);
            _transactionRepositoryMock.Verify(repo => repo.GetTopCategoriesAsync(It.IsAny<int>()), Times.Once);
            _transactionRepositoryMock.Verify(repo => repo.GetHighestSpenderAsync(), Times.Once);

            Assert.NotNull(report);
            report.UsersSummary.Should().BeEquivalentTo(userSummary.Select(x => x.ToUserSummary()).ToList());
            report.TopCategories.Should().BeEquivalentTo(topCategories.Select(x => x.ToCategorySummary()).ToList());
            report.HighestSpender.Should().BeEquivalentTo(highestSpender.ToUserSummary());
        }
    }
}