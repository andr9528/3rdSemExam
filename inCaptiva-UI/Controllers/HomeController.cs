using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inCaptiva_UI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace inCaptiva_UI.Controllers {
    public class HomeController : Controller {

        public async Task<ActionResult> Index() {
            var service = new InCaptivaService.InCaptivaServiceClient();
            await service.NewWorkerAsync("Bob");
            await service.NewWorkerAsync("Hans");
            await service.NewWorkerAsync("Ryt");
            await service.NewWorkerAsync("Knud");
            List<InCaptivaService.Worker> data = await service.GetWorkersAsync();
            return View(data);
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier });
        }
    }
}
