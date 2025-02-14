using FluentAssertions;
using FluentAssertions.Execution;
using Moq;

namespace TransactionProcessing.Tests
{
    public static class ItIs
    {
        public static bool MatchingAssertion(Action assertion)
        {
            using var assertionScope = new AssertionScope();
            assertion();
            return !assertionScope.Discard().Any();
        }

        public static T EquivalentTo<T>(T obj) where T : class
        {
            return It.Is<T>(seq => MatchingAssertion(() => seq.Should().BeEquivalentTo(obj, "")));
        }
    }
}
