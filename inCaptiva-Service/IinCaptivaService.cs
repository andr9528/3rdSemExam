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
        bool NewWorker(string name);

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
        void EditWorker(int workerID, string name = "");

        [OperationContract]
        void EditWorkEntry(int entryID, DateTime? start = null, int workerID = -1, int taskID = -1);

        [OperationContract]
        void EditTask(int taskID, string description = "", DateTime? start = null, int projectID = -1);

        [OperationContract]
        void EditProject(int projectID, string name = "", DateTime? start = null);

        [OperationContract]
        string DeleteWorker(int workerID);

        [OperationContract]
        string DeleteWorkEntry(int workerID);

        [OperationContract]
        string DeleteTask(int workerID);

        [OperationContract]
        string DeleteProject(int workerID);

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
