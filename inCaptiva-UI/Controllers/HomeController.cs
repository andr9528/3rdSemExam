﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Delete(int what, int id)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.DeleteAsync(1, id);

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
        public async Task<IActionResult> WorkEntry(InCaptivaService.WorkEntry workEntry)
        {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkEntryAsync(workEntry.WorkerID, workEntry.TaskID);

            return RedirectToAction("WorkEntry");
        }
        [HttpPost]
        public async Task<IActionResult> WorkEntry(int entryID)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/mvc/razor-pages/?tabs=visual-studio#using-multiple-handlers
            //https://www.pluralsight.com/guides/microsoft-net/asp-net-mvc-using-multiple-submit-buttons-with-default-model-binding-and-controller-actions?status=in-review&aid=7010a000002BWqGAAW&promo=&oid=&utm_source=google&utm_medium=ppc&utm_campaign=EMEA_Dynamic&utm_content=&utm_term=&gclid=EAIaIQobChMIrdXHmN7k1wIVB4myCh2dfgUkEAAYASAAEgJ8yvD_BwE
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.EndWorkEntryAsync(entryID);

            return RedirectToAction("WorkEntry");
        }

    }
}
