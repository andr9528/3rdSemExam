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
        public IActionResult Index(InCaptivaService.Worker NewWorker) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            service.NewWorkerAsync(NewWorker.Name);
            return View();
        }

        public async Task<ActionResult> Project() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            List<InCaptivaService.Project> project = await service.GetProjectsAsync();
            return View(project);
        }

        [HttpPost]
        public IActionResult Project(InCaptivaService.Project project) {
            var service = new InCaptivaService.InCaptivaServiceClient();
            service.NewProjectAsync(project.Name);
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier });
        }
    }
}
