using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Web_db.Models;

namespace Web_db
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.MapHttpAttributeRoutes();
            config.EnableQuerySupport();
//            config.AddODataQueryFilter();
            
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Department>("Departments");
            config.MapODataServiceRoute(
                "ODataRoute",
                null,
                builder.GetEdmModel());
//            // Конфигурация и службы веб-API
//
//            // Маршруты веб-API
//
//            config.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{id}",
//                defaults: new { id = RouteParameter.Optional }
//            );
        }
    }
}
