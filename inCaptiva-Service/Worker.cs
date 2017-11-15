using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace inCaptiva_Service
{
    [DataContract]
    public class Worker
    {
        private object Lock = new object();
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }

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