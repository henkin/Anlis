using System.Threading.Tasks;
using NUnit.Framework;

namespace Nala.Client.Tests
{
	[TestFixture]
	public class ServiceTests
	{
        [Test]
		public async Task PostStatement_ReturnsResult()
		{
			var input = "A quick brown fox jumped over the lazy dog";
			var service = new Service ();
			var result = await service.AnalyzeStatement(input);
            Assert.AreEqual(input, result.Statement);
		}
	}
}

