using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using inCaptiva_UI.Models;

namespace inCaptiva_UI.Controllers
{
    public class HomeController : Controller
    {

        //INDEX
        public async Task<ActionResult> Index()
        {

            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Worker> worker = await service.GetWorkersAsync();
            List<InCaptivaService.Project> project = await service.GetProjectsAsync();
            List<InCaptivaService.Task> task = await service.GetTasksAsync();
            List<InCaptivaService.WorkEntry> workEntry = await service.GetWorkEntriesAsync();

            Data data = new Data
            {
                Worker = worker,
                Project = project,
                Task = task,
                WorkEntry = workEntry
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string whatever)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.AddTestDataAsync();
            return RedirectToAction("Index");
        }

        //WORKER
        public async Task<ActionResult> Worker()
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Worker> worker = await service.GetWorkersAsync();
            return View(worker);
        }

        [HttpPost]
        public async Task<IActionResult> Worker(InCaptivaService.Worker newWorker)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkerAsync(newWorker.Name, newWorker.PhoneNumber, newWorker.Email, newWorker.JobDescription);
            return RedirectToAction("Worker");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InCaptivaService.InCaptivaServiceListType what, int id)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.DeleteAsync(what, id);
            
            return RedirectToAction("Worker");
        }

        //PROJECT
        public async Task<ActionResult> Project()
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Project> project = await service.GetProjectsAsync();
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Project(InCaptivaService.Project project)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewProjectAsync(project.Name, project.Description);

            return RedirectToAction("Project");
        }

        //TASK
        public async Task<ActionResult> Task()
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Task> task = await service.GetTasksAsync();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Task(InCaptivaService.Task task)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewTaskAsync(task.ProjectID, task.Name, task.Description, task.EstimatedTime);

            return RedirectToAction("Task");
        }

        //WORKENTRY
        public async Task<ActionResult> WorkEntry()
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.WorkEntry> workEntry = await service.GetWorkEntriesAsync();
            return View(workEntry);
        }

        [HttpPost]
        public async Task<IActionResult> WorkEntry(InCaptivaService.WorkEntry workEntry, int entryID, string lol)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            if (lol == "test")
            {
                await service.NewWorkEntryAsync(workEntry.WorkerID, workEntry.TaskID);
            }
            else if (lol == "test2")
            {
                await service.EndWorkEntryAsync(entryID);
            }
            
            return RedirectToAction("WorkEntry");
        }
    }
}
