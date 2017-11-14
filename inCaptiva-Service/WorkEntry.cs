using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace inCaptiva_Service
{
    [DataContract]
    public class WorkEntry
    {
        private object Lock = new object();
        private int[] timeUsed = new int[3];
        private List<DateTime> StartBreaks = new List<DateTime>();
        private List<DateTime> EndBreaks = new List<DateTime>();

        [DataMember]
        public DateTime StartTime { get; internal set; }
        [DataMember]
        public DateTime CompletedTime { get; internal set; }
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
        public int TaskID { get; set; }
        [DataMember]
        public int WorkerID { get; set; }
        [DataMember]
        public int ID { get; set; }
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

        private void DetermineTimeUsed()
        {
            lock (Lock)
            {
                TimeSpan Used;

                if (Status)
                {
                    Used = CompletedTime - StartTime;
                }
                else
                {
                    Used = DateTime.Now - StartTime;
                }

                timeUsed[0] = Used.Days;
                timeUsed[1] = Used.Hours;
                timeUsed[2] = Used.Minutes;
            }
        }

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
            }
        }
        public bool StartBreak()
        {
            if (StartBreaks.Count == EndBreaks.Count )
            {
                StartBreaks.Add(DateTime.Now);
                return true;
            }
            return false;
            
        }
        public bool EndBreak()
        {
            if (EndBreaks.Count+1 == StartBreaks.Count)
            {
                EndBreaks.Add(DateTime.Now);
                return true;
            }
            return false;
        }
    }
}