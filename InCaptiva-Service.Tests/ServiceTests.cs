using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

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
            client.ResetService("Nagakaborous");
        }
        [TestCleanup]
        public void Cleanup()
        {
            client.ResetService("Nagakaborous");
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
            client.NewWorker("Jacob", "+4512345678", "Someone@Somewhere.org", "Secretary");
            Assert.AreEqual(1, client.GetWorkers().Count);
        }
        [TestMethod]
        public void CreateWorkEntry1()
        {
            client.NewWorkEntry(0, 0);
            Thread.Sleep(2000);
            client.EndWorkEntry(client.GetWorkEntries()[client.GetWorkEntries().Count - 1].ID);
            List<InCaptivaService.WorkEntry> entries = client.GetWorkEntries();
            InCaptivaService.WorkEntry entry = entries[entries.Count-1];

            Assert.AreEqual(new TimeSpan(0, 0, 2), entry.TimeUsed);
        }
        [TestMethod]
        public void DeleteTest1()
        {
            client.NewWorker("Jacob", "+412345678", "Someone@Somewhere.org", "Secretary");
            List<InCaptivaService.Worker> workers = client.GetWorkers();
            int countbefore = workers.Count;
            InCaptivaService.Worker worker = workers[countbefore - 1];

            Assert.AreEqual(1, countbefore);

            Assert.IsTrue(client.Delete(InCaptivaService.InCaptivaServiceListType.Worker, worker.ID));

            Assert.AreEqual(0, countbefore - 1);
            
        }
    }
}
