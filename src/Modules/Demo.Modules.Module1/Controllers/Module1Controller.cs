using Microsoft.AspNetCore.Mvc;

namespace Demo.Modules.Module1.Controllers
{
    public class Module1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
