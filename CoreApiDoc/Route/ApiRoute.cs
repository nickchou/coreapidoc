using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiDoc.Route
{
    public class ApiRoute : IRouter
    {
        private string _url;

        private IRouter _mvcRouter;

        private string _controllerName;

        private string _actionName;

        public ApiRoute(IServiceProvider provider, string url, string controllerName, string actionName)
        {
            _url = url;
            _mvcRouter = provider.GetRequiredService<MvcRouteHandler>();
            _actionName = actionName;
            _controllerName = controllerName;
        }
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            var requestPath = context.HttpContext.Request.Path;
            if (requestPath.Value.Trim('/') == _url)
            {
                context.RouteData.Values["controller"] = _controllerName;
                context.RouteData.Values["action"] = _actionName;
                await _mvcRouter.RouteAsync(context);
            }
        }
    }
}
