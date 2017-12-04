using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace inCaptiva_Service
{
    [DataContract]
    public class Project
    {
        private object Lock = new object();
        private TimeSpan timeUsed = new TimeSpan();
        private TimeSpan estimatedTime = new TimeSpan();
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
        public string Name { get; set; }
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
        public TimeSpan EstimatedTime
        {
            get
            {
                DetermineEstimatedTime();
                return estimatedTime;
            }
            set { }
        }

        private void DetermineEstimatedTime()
        {
            lock (Lock)
            {
                List<Task> Tasks;

                lock (Repo.Lock)
                {
                    Tasks = Repo.Tasks.FindAll(x => x.ProjectID == ID);
                }

                if (Tasks != null)
                {
                    foreach (var task in Tasks)
                    {
                        estimatedTime += task.EstimatedTime;
                    }
                }
                else
                {
                    throw new ArgumentNullException("Tasks");
                }

            }
        }

        public Project(string name, string description)
        {
            lock (Lock)
            {

                StartTime = DateTime.Now;

                Name = name;
                Description = description;

                lock (Repo.Lock)
                {
                    Repo.HighestProjectID++;
                    ID = Repo.HighestProjectID;
                }

            }
        }
        public bool Completed()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;
            }
            return true;
        }
        private void DetermineTimeUsed()
        {
            lock (Lock)
            {
                List<Task> Tasks;

                lock (Repo.Lock)
                {
                    Tasks = Repo.Tasks.FindAll(x => x.ProjectID == ID);
                }

                if (Tasks != null)
                {
                    foreach (var task in Tasks)
                    {
                        timeUsed += task.TimeUsed;
                    }
                }
                else
                {
                    throw new ArgumentNullException("Tasks");
                }

            }
        }
    }
}