using System;
using DNS.Common;
using DNS.Common.Mongo.Testing;
using NUnit.Framework;

namespace DNS.SignalR.Host.Tests
{
    [TestFixture]
    public class TestTestFixture
    {
        private string _mongoHost;
        public string MongoHost
        {
            get { return _mongoHost ?? (_mongoHost = AppConfig.GetAppSetting<string>("MongoHost")); }
        }

        private string _mongoDatabaseName;
        public string MongoDatabaseName
        {
            get { return _mongoDatabaseName ?? (_mongoDatabaseName = AppConfig.GetAppSetting<string>("MongoDatabaseName")); }
        }

        [SetUp]
        public void SetUp()
        {
            if (!MongoRestorer.Restore(MongoHost, MongoDatabaseName, "Data"))
                throw new Exception("Problem encountered restoring MongoDb.");
        }

        [Test]
        public void TestsNothing()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestsMoreNothing()
        {
            Assert.IsTrue(true);
        }
    }
}
