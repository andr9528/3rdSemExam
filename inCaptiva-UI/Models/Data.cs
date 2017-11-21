using System;
using System.Collections.Generic;
using InCaptivaService;

namespace inCaptiva_UI.Models
{
    public class Data
    {
        public InCaptivaServiceClient Service = new InCaptivaService.InCaptivaServiceClient();
        public List<InCaptivaService.Worker> Worker;
        public List<InCaptivaService.Project> Project;
        public List<InCaptivaService.Task> Task;
        public List<InCaptivaService.WorkEntry> WorkEntry;
    }
}