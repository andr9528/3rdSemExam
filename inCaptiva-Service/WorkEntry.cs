using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptiva_Service
{
    public class WorkEntry
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
        public int TaskID { get; internal set; }
        public int WorkerID { get; internal set; }
        public int ID { get; internal set; }
        public string TimeUsed { get; internal set; } // Days:Hours:Minutes

        public WorkEntry(int taskID, int workerID, DateTime? start = null, int id = -1)
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
                TaskID = taskID;
                WorkerID = workerID;
                if (id == -1)
                {
                    lock (Repo.Lock)
                    {
                        Repo.HighestEntryID++;
                        ID = Repo.HighestEntryID;
                    }
                }
            }
        }
        public void EndShift()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;

                TimeSpan Used = CompletedTime - StartTime;

                TimeUsed = Used.Days + ":" + Used.Hours + ":" + Used.Minutes;
            }
        }
        public void StartBreak()
        {
            throw new NotImplementedException();
        }
        public bool EndBreak()
        {
            throw new NotImplementedException();
        }
    }
}