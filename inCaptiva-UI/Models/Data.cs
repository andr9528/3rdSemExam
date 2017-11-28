using System.Collections.Generic;
using InCaptivaService;
using System.Collections;

namespace inCaptiva_UI.Models
{
    public class Data : IEnumerable<Data>
    {
        public List<Worker> Worker;
        public List<Project> Project;
        public List<Task> Task;
        public List<WorkEntry> WorkEntry;

        public List<Data> data { get; private set; }

        public Data()
        {
            data = new List<Data>();
        }

        public void Add(Data item)
        {
            data.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Data> GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}