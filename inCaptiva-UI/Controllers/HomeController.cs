using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inCaptiva_UI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace inCaptiva_UI.Controllers {
    public class HomeController : Controller {

        public async Task<ActionResult> Index() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Worker> worker = await service.GetWorkersAsync();
            return View(worker);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InCaptivaService.Worker NewWorker) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkerAsync(NewWorker.Name);

            List<InCaptivaService.Worker> worker2 = await service.GetWorkersAsync();
            return View(worker2);
        }

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

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier });
        }
    }
}
