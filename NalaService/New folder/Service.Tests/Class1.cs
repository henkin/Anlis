using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Service.Tests
{
    [TestFixture]
    public class UpdaterTests
    {

        [Test]
        public void GetLatestVersion()
        {
            var update = new UpdaterService("Nala.Worker");
            var version = update.GetLatestVersion();
            Assert.IsNotNull(version);
        }
    }
}
