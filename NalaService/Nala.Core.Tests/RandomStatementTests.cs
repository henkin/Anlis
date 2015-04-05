using NUnit.Framework;

namespace Nala.Core.Tests
{
    [TestFixture]
    public class RandomStatementTests
    {
        [Test]
        public void GetRandomStatement()
        {
            var statement = RandomPhraseService.GetRandomPhraseFromResource();
            Assert.IsNotNull(statement);
        }
    }
}