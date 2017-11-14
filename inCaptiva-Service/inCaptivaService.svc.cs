using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace inCaptiva_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class InCaptivaService : IInCaptivaService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<Project> GetProjects()
        {
            List<Project> list;
            lock (Repo.Lock)
            {
                list = new List<Project>(Repo.Projects);
            }
            return list;
        }

        public List<Task> GetTasks()
        {
            List<Task> list;
            lock (Repo.Lock)
            {
                list = new List<Task>(Repo.Tasks);
            }
            return list;
        }

        public List<WorkEntry> GetWorkEntries()
        {
            List<WorkEntry> list;
            lock (Repo.Lock)
            {
                list = new List<WorkEntry>(Repo.WorkEntries);
            }
            return list;
        }

        public List<Worker> GetWorkers()
        {
            List<Worker> list;
            lock (Repo.Lock)
            {
                list = new List<Worker>(Repo.Workers);
            }
            return list;
        }

        public void NewProject(string name)
        {
            Project project = new Project(name);
            lock (Repo.Lock)
            {
                Repo.Projects.Add(project);
            }
        }

        public void NewTask(int projectID, string description) // kills the service
        {
            Task task = new Task(projectID, description);
            lock (Repo.Lock)
            {
                Repo.Tasks.Add(task);
            }
        }

        public void NewWorker(string name)
        {
            Worker worker = new Worker(name);
            lock (Repo.Lock)
            {
                Repo.Workers.Add(worker);
            }
        }

        public void NewWorkEntry(int workerID, int taskID)
        {
            WorkEntry entry = new WorkEntry(taskID, workerID);
            lock (Repo.Lock)
            {
                Repo.WorkEntries.Add(entry);
            }
        }

        public bool StartBreak(int workEntryID)
        {
            WorkEntry entry;
            lock (Repo.Lock)
            {
                entry = Repo.WorkEntries.Find(x => x.ID == workEntryID);
            }
            return entry.StartBreak();
        }

        public bool EndBreak(int workEntryID)
        {
            WorkEntry entry;
            lock (Repo.Lock)
            {
                entry = Repo.WorkEntries.Find(x => x.ID == workEntryID);
            }
            return entry.EndBreak();
        }

        public void EditWorker(int workerID, string name = "")
        {
            throw new NotImplementedException();
        }

        public void EditWorkEntry(int entryID, DateTime? start = null, int workerID = -1, int taskID = -1)
        {
            throw new NotImplementedException();
        }

        public void EditTask(int taskID, string description = "", DateTime? start = null, int projectID = -1)
        {
            throw new NotImplementedException();
        }

        public void EditProject(int projectID, string name = "", DateTime? start = null)
        {
            throw new NotImplementedException();
        }

        public string DeleteWorker(int workerID)
        {
            throw new NotImplementedException();
        }

        public string DeleteWorkEntry(int workerID)
        {
            throw new NotImplementedException();
        }

        public string DeleteTask(int workerID)
        {
            throw new NotImplementedException();
        }

        public string DeleteProject(int workerID)
        {
            throw new NotImplementedException();
        }

        public bool ResetService(string password)
        {
            if (password == "Nagakaborous")
            {
                lock (Repo.Lock)
                {
                    Repo.Projects.Clear();
                    Repo.Tasks.Clear();
                    Repo.WorkEntries.Clear();
                    Repo.Workers.Clear();
                    Repo.HighestEntryID = 0;
                    Repo.HighestProjectID = 0;
                    Repo.HighestTaskID = 0;
                    Repo.HighestWorkerID = 0;
                }
                return true;
            }
            return false;
        }
    }
}
