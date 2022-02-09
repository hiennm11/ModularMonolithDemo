using Demo.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Modules.Module2.Views.Shared.Components.Demo
{
    public class DemoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(this.GetViewPath());
        }
    }
}
