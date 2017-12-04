using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace inCaptiva_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IInCaptivaService
    {
        [OperationContract]
        string GetData(int value); // test method

        [OperationContract]
        List<Task> GetTasks();

        [OperationContract]
        List<Worker> GetWorkers();

        [OperationContract]
        List<Project> GetProjects();

        [OperationContract]
        List<WorkEntry> GetWorkEntries();

        [OperationContract]
        bool NewWorker(string name, string phoneNumber, string email, string jobDescription);

        [OperationContract]
        bool NewProject(string name, string description);

        [OperationContract]
        bool NewTask(int projectID, string name, string description, TimeSpan estimatedTime);

        [OperationContract]
        bool NewWorkEntry(int workerID, int taskID);

        [OperationContract]
        bool StartBreak(int workEntryID);

        [OperationContract]
        bool EndBreak(int workEntryID);

        [OperationContract]
        bool EditWorker(int workerID, string name = "", string phoneNumber = "", string email = "", string jobDescription = "");

        [OperationContract]
        bool EditWorkEntry(int entryID, DateTime? start = null, int workerID = -1, int taskID = -1, DateTime? completed = null);

        [OperationContract]
        bool EditTask(int taskID, string description = "", DateTime? start = null, int projectID = -1, DateTime? completed = null, string name = "");

        [OperationContract]
        bool EditProject(int projectID, string name = "", DateTime? start = null, DateTime? completed = null, string description = "");

        [OperationContract]
        bool EndWorkEntry(int entryID);

        [OperationContract]
<<<<<<< HEAD
        bool Delete(int type, int id);
=======
        bool EndTask(int taskID);

        [OperationContract]
        bool EndProject(int projectID);

        [OperationContract]
        bool Delete(InCaptivaService.ListType type, int id);

        [OperationContract(Name = "SortTask")]
        bool Sort(InCaptivaService.PropTask prop = InCaptivaService.PropTask.ID);

        [OperationContract(Name = "SortProject")]
        bool Sort(InCaptivaService.PropProject prop = InCaptivaService.PropProject.ID);

        [OperationContract(Name = "SortWorker")]
        bool Sort(InCaptivaService.PropWorker prop = InCaptivaService.PropWorker.ID);

        [OperationContract(Name = "SortWorkEntry")]
        bool Sort(InCaptivaService.PropWorkEntry prop = InCaptivaService.PropWorkEntry.ID);

        [OperationContract]
        bool AddTestData();
>>>>>>> 7a7b08ec6dbe6e2799101c173f23194b2b9a8372

        [OperationContract]
        bool ResetService(string password);

    }
}
