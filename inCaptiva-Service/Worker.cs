using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptiva_Service
{
    public class Worker
    {
        private object Lock;
        public int ID { get; internal set; }
        public string Name { get; internal set; }

        public Worker(string name)
        {
            lock (Lock)
            {
                Name = name;
                lock (Repo.Lock)
                {
                    Repo.HighestWorkerID++;
                    ID = Repo.HighestWorkerID;
                }
            }
        }
    }
}