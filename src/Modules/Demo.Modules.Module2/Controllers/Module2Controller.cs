using Microsoft.AspNetCore.Mvc;

namespace Demo.Modules.Module2.Controllers
{
    public class Module2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
