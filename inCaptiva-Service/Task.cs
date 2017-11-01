using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptiva_Service
{
    public class Task
    {
        private object Lock;
        public int ProjectID { get; internal set; }
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
        public string Description { get; internal set; }
        public string TimeUsed { get; internal set; } // Days:Hours:Minutes

        public Task(int projectID, string description, int id = -1, DateTime? start = null)
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
                ProjectID = projectID;
                Description = description;
                if (id == -1)
                {
                    lock (Repo.Lock)
                    {
                        Repo.HighestTaskID++;
                        ID = Repo.HighestTaskID;
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
            DetermineTimeUsed();
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

                int Minutes = 0;
                int Hours = 0;
                int Days = 0;

                if (Entries != null)
                {
                    foreach (var entry in Entries)
                    {
                        string[] split = entry.TimeUsed.Split(':');
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
                    throw new ArgumentNullException("Entries");
                }

            }
        }
    }
}