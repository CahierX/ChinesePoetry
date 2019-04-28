using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChinesePoetry
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
             config.MapHttpAttributeRoutes();
            //新加的规则
            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //新加的规则
            config.Routes.MapHttpRoute(
                name: "DefaultApi1",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
