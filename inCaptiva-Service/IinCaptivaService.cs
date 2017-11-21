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
        bool NewProject(string name);

        [OperationContract]
        bool NewTask(int projectID, string description);

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
        bool EditTask(int taskID, string description = "", DateTime? start = null, int projectID = -1, DateTime? completed = null);

        [OperationContract]
        bool EditProject(int projectID, string name = "", DateTime? start = null, DateTime? completed = null);

        [OperationContract]
        bool DeleteWorker(int workerID);

        [OperationContract]
        bool DeleteWorkEntry(int entryID);

        [OperationContract]
        bool DeleteTask(int taskID);

        [OperationContract]
        bool DeleteProject(int projectID);

        [OperationContract]
        bool ResetService(string password);


        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
