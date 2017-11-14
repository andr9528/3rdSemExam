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
        private int[] timeUsed = new int[3];
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime CompletedTime { get; set; }
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
        public int[] TimeUsed
        {
            get
            {
                DetermineTimeUsed();
                return timeUsed;
            }
            set { }
        }
        public Project(string name, int id = -1, DateTime? start = null)
        {
            lock (Lock)
            {
                if (start == null)
                {
                    StartTime = DateTime.Now;
                }
                else
                {
                    StartTime = (DateTime)start;
                }
                Name = name;
                if (id == -1)
                {
                    lock (Repo.Lock)
                    {
                        Repo.HighestProjectID++;
                        ID = Repo.HighestProjectID;
                    } 
                }
                else
                {
                    ID = id;
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
                List<Task> Tasks;

                lock (Repo.Lock)
                {
                    Tasks = Repo.Tasks.FindAll(x => x.ProjectID == ID);
                }

                int Minutes = 0;
                int Hours = 0;
                int Days = 0;

                if (Tasks != null)
                {
                    foreach (var task in Tasks)
                    {
                        Days += task.TimeUsed[0];
                        Hours += task.TimeUsed[1];
                        Minutes += task.TimeUsed[2];
                    }

                    while (Minutes >= 60)
                    {
                        Hours++;
                        Minutes -= 60;
                    }
                    while (Hours >= 24)
                    {
                        Days++;
                        Hours -= 24;
                    }

                    timeUsed[0] = Days;
                    timeUsed[1] = Hours;
                    timeUsed[2] = Minutes;
                }
                else
                {
                    throw new ArgumentNullException("Tasks");
                }

            }
        }
    }
}