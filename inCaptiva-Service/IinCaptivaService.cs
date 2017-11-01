﻿using System;
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
        void NewWorker(string name);

        [OperationContract]
        void NewProject(string name);

        [OperationContract]
        void NewTask(string description);

        [OperationContract]
        void WorkEntry(int workerID, int taskID);

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