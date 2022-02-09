using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Demo.Infrastructure.Extensions
{
    public static class ViewComponentExtension
    {
        public static string GetViewPath(this ViewComponent viewComponent, string viewName = "Default", bool isViewComponentInArea = false)
        {
            var moduleName = viewComponent.GetType().Assembly.GetName().Name.Split('.').Last();
            var componentShortName = viewComponent.ViewComponentContext.ViewComponentDescriptor.ShortName;

            string viewPath = $"/Views/Shared/Components/{componentShortName}/{viewName}.cshtml";

            if (isViewComponentInArea)
            {
                viewPath = $"/Areas/{moduleName}" + viewPath;
            }          

            return viewPath;
        }
    }
}
