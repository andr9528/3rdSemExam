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

                

                lock (Lock)
                {
                    if (StartBreaks.Count == EndBreaks.Count)
                    {
                        Used -= DetermineBreakTime(0);
                    }
                    else if (StartBreaks.Count > EndBreaks.Count)
                    {
                        Used -= DetermineBreakTime(1);
                    }
                    else
                    {
                        throw new Exception("HOW DID YOU GET HERE!" +
                            " - Failed to determine time used for Entry with ID of" + ID + " ");

                    }
                }

                timeUsed[0] = Used.Days;
                timeUsed[1] = Used.Hours;
                timeUsed[2] = Used.Minutes;
            }
        }
        private TimeSpan DetermineBreakTime(int a)
        {
            TimeSpan output = new TimeSpan();

            for (int i = 0; i < StartBreaks.Count-a; i++)
            {
                output += EndBreaks[i] - StartBreaks[i];
            }

            if (a == 1)
            {
                output += DateTime.Now - StartBreaks.LastOrDefault();
            }

            return output;
        }

        public WorkEntry(int taskID, int workerID)
        {
            lock (Lock)
            {
                StartTime = DateTime.Now; 
                TaskID = taskID;
                WorkerID = workerID;
                lock (Repo.Lock)
                {
                    Repo.HighestEntryID++;
                    ID = Repo.HighestEntryID;
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