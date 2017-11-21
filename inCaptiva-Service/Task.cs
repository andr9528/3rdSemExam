using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace inCaptiva_Service
{
    [DataContract]
    public class Task
    {
        private object Lock = new object();
        private TimeSpan timeUsed = new TimeSpan();
        [DataMember]
        public int ProjectID { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime? CompletedTime { get; set; }
        [DataMember]
        public bool Status
        {
            get
            {
                if (CompletedTime != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set { }
        }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public TimeSpan TimeUsed
        {
            get
            {
                DetermineTimeUsed();
                return timeUsed;
            }
            set { }
        }

        public Task(int projectID, string description)
        {
            lock (Lock)
            {

                StartTime = DateTime.Now;


                ProjectID = projectID;
                Description = description;

                lock (Repo.Lock)
                {
                    Repo.HighestTaskID++;
                    ID = Repo.HighestTaskID;
                }

            }
        }
        public void Completed()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;
            }
        }
        public void DetermineTimeUsed()
        {
            lock (Lock)
            {
                List<WorkEntry> Entries;

                lock (Repo.Lock)
                {
                    Entries = Repo.WorkEntries.FindAll(x => x.TaskID == ID);
                }

                if (Entries != null)
                {
                    foreach (var entry in Entries)
                    {
                        timeUsed += entry.TimeUsed;
                        
                    }
                }
                else
                {
                    throw new ArgumentNullException("Entries");
                }

            }
        }
    }
}