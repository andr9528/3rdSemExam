using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Linq;
using MvcApp.Models;

namespace inCaptiva_UI.Controllers {
    public class HomeController : Controller {

        //INDEX
        public async Task<ActionResult> Index() {

            //var service = new InCaptivaService.InCaptivaServiceClient();
            //List<InCaptivaService.Worker> worker = await service.GetWorkersAsync();
            //List<InCaptivaService.Project> project = await service.GetProjectsAsync();
            //List<InCaptivaService.Task> task = await service.GetTasksAsync();
            //List<InCaptivaService.WorkEntry> workEntry = await service.GetWorkEntriesAsync();

            //List<IList> data = new List<IList> {
            //    worker, project, task, workEntry
            //};

            ChartImage();
            return View();
        }

        //WORKER
        public async Task<ActionResult> Worker() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Worker> worker = await service.GetWorkersAsync();
            return View(worker);
        }

        [HttpPost]
        public async Task<IActionResult> Worker(InCaptivaService.Worker newWorker) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkerAsync(newWorker.Name,newWorker.PhoneNumber,newWorker.Email,newWorker.JobDescription);

            List<InCaptivaService.Worker> worker2 = await service.GetWorkersAsync();
            return View(worker2);
        }

        //PROJECT
        public async Task<ActionResult> Project() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Project> project = await service.GetProjectsAsync();
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Project(InCaptivaService.Project project) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewProjectAsync(project.Name);

            List<InCaptivaService.Project> project2 = await service.GetProjectsAsync();
            return View(project2);
        }

        //TASK
        public async Task<ActionResult> Task() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Task> task = await service.GetTasksAsync();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Task(InCaptivaService.Task task) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewTaskAsync(task.ProjectID,task.Description);

            List<InCaptivaService.Task> task2 = await service.GetTasksAsync();
            return View(task2);
        }

        //WORKENTRY
        public async Task<ActionResult> WorkEntry() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.WorkEntry> workEntry = await service.GetWorkEntriesAsync();
            return View(workEntry);
        }

        [HttpPost]
        public async Task<IActionResult> WorkEntry(InCaptivaService.WorkEntry workEntry) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkEntryAsync(workEntry.WorkerID,workEntry.TaskID);

            List<InCaptivaService.WorkEntry> workEntry2 = await service.GetWorkEntriesAsync();
            return View(workEntry2);
        }



        //CHART ?
        public void ChartImage() {
            IEnumerable<Product> productList = new List<Product> {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275m},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95m},
                new Product {Name = "Soccer ball", Category = "Football", Price = 19.50m},
                new Product {Name = "Corner flags", Category = "Football", Price = 34.95m},
                new Product {Name = "Stadium", Category = "Football", Price = 150m},
                new Product {Name = "Thinking cap", Category = "Chess", Price = 16m}
            };

            Chart chart = new Chart(400,200,
                @"<Chart BackColor=""Gray"" BackSecondaryColor=""WhiteSmoke""
        BackGradientStyle=""DiagonalRight"" AntiAliasing=""All""
        BorderlineDashStyle = ""Solid"" BorderlineColor = ""Gray"">
        <BorderSkin SkinStyle = ""Emboss"" />
        <ChartAreas>
            <ChartArea Name=""Default"" _Template_=""All"" BackColor=""Wheat""
        BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64""
        BorderDashStyle=""Solid"" ShadowColor=""Transparent"">
        </ChartArea>
        </ChartAreas>
        </Chart>");

            chart.AddSeries(
                chartType: "Pie",
                yValues: productList.Select(e => e.Price).ToArray(),
                xValue: productList.Select(e => e.Name).ToArray()
            );
            chart.Write();
        }
    }
}
