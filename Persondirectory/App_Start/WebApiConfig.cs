﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Persondirectory
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{recid}",
                defaults: new { recid = RouteParameter.Optional }                
            );
            /*config.Routes.MapHttpRoute(
               name: "ApiByName",
               routeTemplate: "api/{controller}/{action}",
               defaults:new { recname=RouteParameter.Optional}
            );*/
        }
    }
}
