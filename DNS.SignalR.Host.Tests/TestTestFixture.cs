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
                
        }

        [Test]
        public void MongoRestorer_Restores_IntegersBetween0and10Inclusive()
        {
            //Arrange
            //Act
            //Assert
            Assert.IsTrue(MongoRestorer.Restore(MongoHost, MongoDatabaseName, "Data")); //TODO [Darren,20140715] This currently only asserts there was no error, restore may not have happened            
        }
    }
}
