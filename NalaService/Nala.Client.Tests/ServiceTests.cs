using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Nala.Client.Tests
{
	[TestFixture]
	public class ServiceTests
	{
        [Test]
		public async Task<ParsedStatement> PostStatement_ReturnsResult()
        {
            Thread.Sleep(15000);
			var input = "A quick brown fox jumped over the lazy dog";
			var service = new Service ("http://localhost:7889");
			var result = await service.AnalyzeStatement(input);
            Assert.AreEqual(input, result.statement);
            return result.parse;
        }

	    public static void Main(string[] args)
	    {
	        var serviceTests = new ServiceTests();
	        try
	        {
                Task.WaitAll(serviceTests.AssertResult());//.ContinueWith((t) => {  AssertResult(t); });
	        }
	        catch (Exception ex)
	        {

	            throw ex.InnerException;
	        }
	    }

	    private async Task AssertResult()
	    {
            ParsedStatement results = await PostStatement_ReturnsResult();
	    }
	}
}

