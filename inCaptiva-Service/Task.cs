using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptive_Service
{
    public class Task
    {
        public int ProjectID { get; internal set; }
        public int WorkerID { get; set; }
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
        public string TimeUsed { get; internal set; }

        public Task()
        {
            StartTime = DateTime.Now;
        }
        public void Completed()
        {
            CompletedTime = DateTime.Now;
        }
    }
}