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
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string JobDescription { get; set; }


        public Worker(string name, string phoneNumber, string email, string jobDescription)
        {
            lock (Lock)
            {
                Name = name;
                PhoneNumber = phoneNumber;
                JobDescription = jobDescription;
                Email = email;

                lock (Repo.Lock)
                {
                    ID = Repo.HighestWorkerID;
                    Repo.HighestWorkerID++;
                }
            }
        }
    }
}