using Microsoft.AspNetCore.Mvc;

namespace ChangeMonitor.Controllers
{
    public class MonitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}