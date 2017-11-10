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
        private object Lock;
        [DataMember]
        public int ID { get; internal set; }
        [DataMember]
        public string Name { get; internal set; }

        public Worker(string name, int id = -1)
        {
            lock (Lock)
            {
                Name = name;
                if (id == -1)
                {
                    lock (Repo.Lock)
                    {
                        Repo.HighestWorkerID++;
                        ID = Repo.HighestWorkerID;
                    } 
                }
                else
                {
                    ID = id;
                }
            }
        }
    }
}