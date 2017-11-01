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
            throw new NotImplementedException();
        }

        public void NewTask(string description)
        {
            throw new NotImplementedException();
        }

        public void NewWorker(string name)
        {
            throw new NotImplementedException();
        }

        public void WorkEntry(int workerID, int taskID)
        {
            throw new NotImplementedException();
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
