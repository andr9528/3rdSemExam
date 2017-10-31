﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptive_Service
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
        public string TimeUsed { get; internal set; }

        public void EndShift()
        {
            lock (Lock)
            {
                CompletedTime = DateTime.Now;

                
            }
            
        }
    }
}