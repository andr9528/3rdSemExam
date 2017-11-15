using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inCaptiva_Service
{
    public static class Repo
    {
        public static object Lock = new object();

        public static List<Worker> Workers = new List<Worker>();
        public static List<Task> Tasks = new List<Task>();
        public static List<Project> Projects = new List<Project>();
        public static List<WorkEntry> WorkEntries = new List<WorkEntry>();

        public static int HighestProjectID = 0;
        public static int HighestTaskID = 0;
        public static int HighestWorkerID = 0;
        public static int HighestEntryID = 0;
    }
}