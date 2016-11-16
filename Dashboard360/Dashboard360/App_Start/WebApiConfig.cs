using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Dashboard360
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            /*
             * Clients
             */

            // Get all clients
            config.Routes.MapHttpRoute(
                name: "Clients",
                routeTemplate: "api/clients",
                defaults: new { controller = "ClientsApi", Action = "GetAllClients" }
            );

            // Get Client
            config.Routes.MapHttpRoute(
                name: "GetClient",
                routeTemplate: "api/clients/{clientID}",
                defaults: new { controller = "ClientsApi", Action = "GetClient" }
            );

            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10Clients",
                routeTemplate: "api/clients/top10clients",
                defaults: new { controller = "ClientsApi", Action = "GetTop10Clients" }
            );

            /*
             * Other
             */
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
