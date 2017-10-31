using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptive_Service
{
    public class Project
    {
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
        public string TimeUsed { get; set; }
        public Project()
        {
            StartTime = DateTime.Now;
        }
        public void Completed()
        {
            CompletedTime = DateTime.Now;
        }
    }
}