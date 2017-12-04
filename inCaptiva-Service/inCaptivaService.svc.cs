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
<<<<<<< HEAD
=======
        [DataContract]
        public enum ListType
        {
            [EnumMember] Task,
            [EnumMember] Project,
            [EnumMember] Worker,
            [EnumMember] WorkEntry
        }

        // Remember to update the enum's if new properties are added to the objects. 
        [DataContract]
        public enum PropTask
        {
            [EnumMember] ProjectID,
            [EnumMember] StartTime,
            [EnumMember] CompletedTime,
            [EnumMember] Status,
            [EnumMember] ID,
            [EnumMember] Description,
            [EnumMember] Name,
            [EnumMember] TimeUsed,
            [EnumMember] EstimatedTime
        }
        [DataContract]
        public enum PropProject
        {
            [EnumMember] StartTime,
            [EnumMember] CompletedTime,
            [EnumMember] Status,
            [EnumMember] ID,
            [EnumMember] Name,
            [EnumMember] Description,
            [EnumMember] TimeUsed,
            [EnumMember] EstimatedTime
        }
        [DataContract]
        public enum PropWorker
        {
            [EnumMember] ID,
            [EnumMember] Name,
            [EnumMember] PhoneNumber,
            [EnumMember] Email,
            [EnumMember] JobDescription
        }
        [DataContract]
        public enum PropWorkEntry
        {
            [EnumMember] StartTime,
            [EnumMember] CompletedTime,
            [EnumMember] Status,
            [EnumMember] TaskID,
            [EnumMember] WorkerID,
            [EnumMember] ID,
            [EnumMember] TimeUsed
        }

>>>>>>> 7a7b08ec6dbe6e2799101c173f23194b2b9a8372
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<Project> GetProjects()
        {
            try
            {
                List<Project> list;
                lock (Repo.Lock)
                {
                    list = new List<Project>(Repo.Projects);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public List<Task> GetTasks()
        {
            try
            {
                List<Task> list;
                lock (Repo.Lock)
                {
                    list = new List<Task>(Repo.Tasks);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public List<WorkEntry> GetWorkEntries()
        {
            try
            {
                List<WorkEntry> list;
                lock (Repo.Lock)
                {
                    list = new List<WorkEntry>(Repo.WorkEntries);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public List<Worker> GetWorkers()
        {
            try
            {
                List<Worker> list;
                lock (Repo.Lock)
                {
                    list = new List<Worker>(Repo.Workers);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool NewProject(string name, string description)
        {
            try
            {
                Project project = new Project(name, description);
                lock (Repo.Lock)
                {
                    Repo.Projects.Add(project);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool NewTask(int projectID, string name, string description, TimeSpan estimatedTime)
        {
            try
            {
                Task task = new Task(projectID, description, name, estimatedTime);
                lock (Repo.Lock)
                {
                    Repo.Tasks.Add(task);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool NewWorker(string name, string phoneNumber, string email, string jobDescription)
        {
            try
            {
                if (!email.Contains('@'))
                {
                    throw new Exception("@");
                }

                Worker worker = new Worker(name, phoneNumber, email, jobDescription);
                lock (Repo.Lock)
                {
                    Repo.Workers.Add(worker);
                }
                return true;
            }
            catch (Exception e)
            {
                if (e.Message == "@")
                {
                    throw new Exception("Invalid Email - Missing a '@'");
                }
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool NewWorkEntry(int workerID, int taskID)
        {
            try
            {
                WorkEntry entry = new WorkEntry(taskID, workerID);
                lock (Repo.Lock)
                {
                    Repo.WorkEntries.Add(entry);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool StartBreak(int workEntryID)
        {
            try
            {
                WorkEntry entry;
                lock (Repo.Lock)
                {
                    entry = Repo.WorkEntries.Find(x => x.ID == workEntryID);
                }
                return entry.StartBreak();
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a work entry with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EndBreak(int workEntryID)
        {
            try
            {
                WorkEntry entry;
                lock (Repo.Lock)
                {
                    entry = Repo.WorkEntries.Find(x => x.ID == workEntryID);
                }
                return entry.EndBreak();
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a work entry with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EditWorker(int workerID, string name = "", string phoneNumber = "", string email = "", string jobDescription = "")
        {
            try
            {
                Worker worker;
                int index;
                lock (Repo.Lock)
                {
                    worker = Repo.Workers.Find(x => x.ID == workerID);
                    index = Repo.Workers.FindIndex(X => X.ID == workerID);
                }
                if (name != "")
                {
                    worker.Name = name;
                }
                if (phoneNumber != "")
                {
                    worker.PhoneNumber = phoneNumber;
                }
                if (jobDescription != "")
                {
                    worker.JobDescription = jobDescription;
                }
                if (email != "")
                {
                    if (!email.Contains('@'))
                    {
                        throw new Exception("@");
                    }
                    worker.Email = email;
                }
                lock (Repo.Lock)
                {
                    Repo.Workers[index] = worker;
                }
                return true;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a worker with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                if (e.Message == "@")
                {
                    throw new Exception("Invalid Email - Missing a '@'");
                }
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EditWorkEntry(int entryID, DateTime? start = null, int workerID = -1, int taskID = -1, DateTime? completed = null)
        {
            try
            {
                WorkEntry entry;
                int index;
                lock (Repo.Lock)
                {
                    entry = Repo.WorkEntries.Find(x => x.ID == entryID);
                    index = Repo.WorkEntries.FindIndex(X => X.ID == entryID);
                }
                if (start != null)
                {
                    entry.StartTime = (DateTime)start;
                }
                if (workerID != -1)
                {
                    entry.WorkerID = workerID;
                }
                if (taskID != -1)
                {
                    entry.TaskID = taskID;
                }
                if (completed != null)
                {
                    entry.CompletedTime = (DateTime)completed;
                }
                lock (Repo.Lock)
                {
                    Repo.WorkEntries[index] = entry;
                }
                return true;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a work entry with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EditTask(int taskID, string description = "", DateTime? start = null, int projectID = -1, DateTime? completed = null, string name = "")
        {
            try
            {
                Task task;
                int index;
                lock (Repo.Lock)
                {
                    task = Repo.Tasks.Find(x => x.ID == taskID);
                    index = Repo.Tasks.FindIndex(x => x.ID == taskID);
                }
                if (description != "")
                {
                    task.Description = description;
                }
                if (start != null)
                {
                    task.StartTime = (DateTime)start;
                }
                if (projectID != -1)
                {
                    task.ProjectID = projectID;
                }
                if (completed != null)
                {
                    task.CompletedTime = (DateTime)completed;
                }
                if (name != "")
                {
                    task.Name = name;
                }
                lock (Repo.Lock)
                {
                    Repo.Tasks[index] = task;
                }
                return true;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a task with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EditProject(int projectID, string name = "", DateTime? start = null, DateTime? completed = null, string description = "")
        {
            try
            {
                Project project;
                int index;
                lock (Repo.Lock)
                {
                    project = Repo.Projects.Find(x => x.ID == projectID);
                    index = Repo.Projects.FindIndex(x => x.ID == projectID);
                }
                if (name != "")
                {
                    project.Name = name;
                }
                if (start != null)
                {
                    project.StartTime = (DateTime)start;
                }
                if (completed != null)
                {
                    project.CompletedTime = (DateTime)completed;
                }
                if (description != "")
                {
                    project.Description = description;
                }
                lock (Repo.Lock)
                {
                    Repo.Projects[index] = project;
                }
                return true;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a project with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }
        public bool Delete(int type, int id)
        {
            try
            {
                if (type == 1)
                {
                    Repo.Workers.Remove(Repo.Workers.Find(x => x.ID == id));
                }
                else if (type == 2)
                {
                    Repo.WorkEntries.Remove(Repo.WorkEntries.Find(x => x.ID == id));
                }
                else if (type == 3)
                {
                    Repo.Tasks.Remove(Repo.Tasks.Find(x => x.ID == id));
                }
                else if (type == 4)
                {
                    Repo.Projects.Remove(Repo.Projects.Find(x => x.ID == id));
                }
                else
                {
                    throw new Exception("W");
                }

                return true;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Did not find a target to delete with that ID - " + e.Message);
            }
            catch (Exception e)
            {
                if (e.Message == "W")
                {
                    throw new Exception("Invalid Value for 'What', must be a value of 1 to 4");
                }
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EndWorkEntry(int entryID)
        {
            try
            {
                bool output;
                lock (Repo.Lock)
                {
                    output = Repo.WorkEntries.Find(x => x.ID == entryID).EndEntry();
                }
                return output;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
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

<<<<<<< HEAD
        
=======
        public bool EndTask(int taskID)
        {
            try
            {
                bool output;
                lock (Repo.Lock)
                {
                    output = Repo.Tasks.Find(x => x.ID == taskID).Completed();
                }
                return output;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool EndProject(int projectID)
        {
            try
            {
                bool output;
                lock (Repo.Lock)
                {
                    output = Repo.Projects.Find(x => x.ID == projectID).Completed();
                }
                return output;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong... - " + e.Message);
            }
        }

        public bool Sort(PropTask prop = PropTask.ID)
        {
            switch (prop)
            {
                case PropTask.ProjectID:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.ProjectID).ToList();
                    }
                    return true;
                case PropTask.StartTime:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.StartTime).ToList();
                    }
                    return true;
                case PropTask.CompletedTime:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.CompletedTime).ToList();
                    }
                    return true;
                case PropTask.Status:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.Status).ToList();
                    }
                    return true;
                case PropTask.ID:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.ID).ToList();
                    }
                    return true;
                case PropTask.Description:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.Description).ToList();
                    }
                    return true;
                case PropTask.Name:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.Name).ToList();
                    }
                    return true;
                case PropTask.TimeUsed:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.TimeUsed).ToList();
                    }
                    return true;
                case PropTask.EstimatedTime:
                    lock (Repo.Lock)
                    {
                        Repo.Tasks = Repo.Tasks.OrderBy(x => x.EstimatedTime).ToList();
                    }
                    return true;
                default:
                    return false;
            }
        }

        public bool Sort(PropProject prop = PropProject.ID)
        {
            switch (prop)
            {
                case PropProject.StartTime:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.StartTime).ToList();
                    }
                    return true;
                case PropProject.CompletedTime:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.CompletedTime).ToList();
                    }
                    return true;
                case PropProject.Status:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.Status).ToList();
                    }
                    return true;
                case PropProject.ID:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.ID).ToList();
                    }
                    return true;
                case PropProject.Name:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.Name).ToList();
                    }
                    return true;
                case PropProject.Description:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.Description).ToList();
                    }
                    return true;
                case PropProject.TimeUsed:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.TimeUsed).ToList();
                    }
                    return true;
                case PropProject.EstimatedTime:
                    lock (Repo.Lock)
                    {
                        Repo.Projects = Repo.Projects.OrderBy(x => x.EstimatedTime).ToList();
                    }
                    return true;
                default:
                    return false;
            }
        }

        public bool Sort(PropWorker prop = PropWorker.ID)
        {
            switch (prop)
            {
                case PropWorker.ID:
                    lock (Repo.Lock)
                    {
                        Repo.Workers = Repo.Workers.OrderBy(x => x.ID).ToList();
                    }
                    return true;
                case PropWorker.Name:
                    lock (Repo.Lock)
                    {
                        Repo.Workers = Repo.Workers.OrderBy(x => x.Name).ToList();
                    }
                    return true;
                case PropWorker.PhoneNumber:
                    lock (Repo.Lock)
                    {
                        Repo.Workers = Repo.Workers.OrderBy(x => x.PhoneNumber).ToList();
                    }
                    return true;
                case PropWorker.Email:
                    lock (Repo.Lock)
                    {
                        Repo.Workers = Repo.Workers.OrderBy(x => x.Email).ToList();
                    }
                    return true;
                case PropWorker.JobDescription:
                    lock (Repo.Lock)
                    {
                        Repo.Workers = Repo.Workers.OrderBy(x => x.JobDescription).ToList();
                    }
                    return true;
                default:
                    return false;
            }
        }

        public bool Sort(PropWorkEntry prop = PropWorkEntry.ID)
        {
            switch (prop)
            {
                case PropWorkEntry.StartTime:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.StartTime).ToList();
                    }
                    return true;
                case PropWorkEntry.CompletedTime:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.CompletedTime).ToList();
                    }
                    return true;
                case PropWorkEntry.Status:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.Status).ToList();
                    }
                    return true;
                case PropWorkEntry.TaskID:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.TaskID).ToList();
                    }
                    return true;
                case PropWorkEntry.WorkerID:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.WorkerID).ToList();
                    }
                    return true;
                case PropWorkEntry.ID:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.ID).ToList();
                    }
                    return true;
                case PropWorkEntry.TimeUsed:
                    lock (Repo.Lock)
                    {
                        Repo.WorkEntries = Repo.WorkEntries.OrderBy(x => x.TimeUsed).ToList();
                    }
                    return true;
                default:
                    return false;
            }
        }
>>>>>>> 7a7b08ec6dbe6e2799101c173f23194b2b9a8372
    }
}
