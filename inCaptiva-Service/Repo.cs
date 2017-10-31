using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptive_Service
{
    public static class Repo
    {
        public static object Lock;
        public static List<Worker> Workers = new List<Worker>();
        public static List<Task> Tasks = new List<Task>();
        public static List<Project> Projects = new List<Project>();
        public static List<WorkEntry> WorkEntries = new List<WorkEntry>();

        public static int HighestProjectID;
        public static int HighestTaskID;
        public static int HighestWorkerID;
    }
}