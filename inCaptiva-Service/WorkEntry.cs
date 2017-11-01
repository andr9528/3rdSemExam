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
        public string TimeUsed { get; internal set; } // Days:Hours:Minutes

        public WorkEntry(int taskID, int workerID)
        {
            lock (Lock)
            {
                StartTime = DateTime.Now;
                TaskID = taskID;
                WorkerID = workerID;
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

        }
        public bool EndBreak()
        {

        }
    }
}