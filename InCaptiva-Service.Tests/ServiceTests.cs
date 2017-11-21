using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        [TestMethod]
        public void CreateWorkEntry1()
        {
            client.NewWorkEntry(0, 0);
            
            List<InCaptivaService.WorkEntry> entries = client.GetWorkEntries();
            InCaptivaService.WorkEntry entry = entries[0];

            

        }
    }
}
