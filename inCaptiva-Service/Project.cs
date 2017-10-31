using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptive_Service
{
    public class Project
    {
        private object Lock;
        public DateTime StartTime { get; internal set; }
        public DateTime CompletedTime { get; internal set; }
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
        }
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        public string TimeUsed { get; internal set; } // Days:Hours:Minutes
        public Project(string name)
        {
            lock (Lock)
            {
                StartTime = DateTime.Now;
                Name = name;
                lock (Repo.Lock)
                {
                    Repo.HighestProjectID++;
                    ID = Repo.HighestProjectID;
                }
            }
        }
        public void Completed()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;  
            }
            DetermineTimeUsed();
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
                        string[] split = task.TimeUsed.Split(':');
                        int.TryParse(split[0], out int dummyDays);
                        int.TryParse(split[1], out int dummyHours);
                        int.TryParse(split[2], out int dummyMinutes);

                        Days += dummyDays;
                        Hours += dummyHours;
                        Minutes += dummyMinutes;
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

                    TimeUsed = Days + ":" + Hours + ":" + Minutes;
                }
                else
                {
                    throw new ArgumentNullException("Tasks");
                }

            }
        }
    }
}