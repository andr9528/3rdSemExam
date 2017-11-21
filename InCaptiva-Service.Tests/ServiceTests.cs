using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InCaptiva_Service.Tests
{
    [TestClass]
    public class ServiceTests
    {
        InCaptivaService.InCaptivaServiceClient client;

        [TestInitialize]
        public void Initialize()
        {
            client = new InCaptivaService.InCaptivaServiceClient();
            
        }
        [TestCleanup]
        public void Cleanup()
        {
            //client.ResetService("Nagakaborous");
            client.Close();
        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual("You entered: 5", client.GetData(5));
        }

        [TestMethod]
        public void CreateWorker1()
        {
            client.NewWorker("Jacob", "+4522287257", "Someone@Somewhere.org", "Secretary");
            Assert.AreEqual(1, client.GetWorkers().Count);
        }
    }
}
