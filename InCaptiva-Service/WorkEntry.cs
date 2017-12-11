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
        private TimeSpan timeUsed = new TimeSpan();
        private List<DateTime> StartBreaks = new List<DateTime>();
        private List<DateTime> EndBreaks = new List<DateTime>();

        [DataMember]
        public DateTime StartTime { get; internal set; }
        [DataMember]
        public DateTime? CompletedTime { get; internal set; }
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
        public TimeSpan TimeUsed
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
                    DateTime startTime = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day,
                        StartTime.Hour, StartTime.Minute, StartTime.Second);
                    DateTime completedTime = (DateTime)CompletedTime;
                    completedTime = new DateTime(completedTime.Year, completedTime.Month, completedTime.Day,
                        completedTime.Hour, completedTime.Minute, completedTime.Second);

                    Used = completedTime - startTime;
                }
                else
                {
                    DateTime startTime = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day,
                        StartTime.Hour, StartTime.Minute, StartTime.Second);
                    DateTime now = DateTime.Now;
                    now = new DateTime(now.Year, now.Month, now.Day,
                        now.Hour, now.Minute, now.Second);

                    Used = now - startTime;
                }

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
                
                timeUsed = Used;
               
            }
        }
        private TimeSpan DetermineBreakTime(int a)
        {
            TimeSpan output = new TimeSpan();

            for (int i = 0; i < StartBreaks.Count - a; i++)
            {
                DateTime start = new DateTime(StartBreaks[i].Year, StartBreaks[i].Month, StartBreaks[i].Day,
                    StartBreaks[i].Hour, StartBreaks[i].Minute, StartBreaks[i].Second);
                DateTime end = new DateTime(EndBreaks[i].Year, EndBreaks[i].Month, EndBreaks[i].Day,
                    EndBreaks[i].Hour, EndBreaks[i].Minute, EndBreaks[i].Second);

                output += end - start;
            }

            if (a == 1)
            {
                DateTime start = new DateTime(StartBreaks.LastOrDefault().Year, StartBreaks.LastOrDefault().Month,
                    StartBreaks.LastOrDefault().Day, StartBreaks.LastOrDefault().Hour,
                    StartBreaks.LastOrDefault().Minute, StartBreaks.LastOrDefault().Second);
                DateTime now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day,
                        now.Hour, now.Minute, now.Second);

                output += now - start;
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
                    ID = Repo.HighestEntryID;
                    Repo.HighestEntryID++;
                }
            }
        }
        public bool EndEntry()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;
                return true;
            }
        }
        public bool StartBreak()
        {
            if (StartBreaks.Count == EndBreaks.Count)
            {
                StartBreaks.Add(DateTime.Now);
                return true;
            }
            return false;

        }
        public bool EndBreak()
        {
            if (EndBreaks.Count + 1 == StartBreaks.Count)
            {
                EndBreaks.Add(DateTime.Now);
                return true;
            }
            return false;
        }
    }
}