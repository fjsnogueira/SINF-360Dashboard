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
                routeTemplate: "api/Clients",
                defaults: new { controller = "ClientsApi", Action = "GetAllClients" }
            );

            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10Clients",
                routeTemplate: "api/Clients/GetTop10Clients",
                defaults: new { controller = "ClientsApi", Action = "GetTop10Clients" }
            );


            /*
             * Sales
             */

            // Get all sales
            config.Routes.MapHttpRoute(
                name: "Sales",
                routeTemplate: "api/Sales",
                defaults: new { controller = "SalesApi", Action = "GetAllSales" }
            );

            /*
             * Purchases
             */
            config.Routes.MapHttpRoute(
                name: "Purchases",
                routeTemplate: "api/Purchases",
                defaults: new { controller = "PurchasesApi", Action = "GetAllPurchases" }
            );


            /*
             * Products
             */

            // Get all sales
            config.Routes.MapHttpRoute(
                name: "Products",
                routeTemplate: "api/Products",
                defaults: new { controller = "ProductsApi", Action = "GetAllProducts" }
            );

            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10ProductsSold",
                routeTemplate: "api/Products/GetTop10ProductsSold",
                defaults: new { controller = "ProductsApi", Action = "GetTop10ProductsSold" }
            );

            /*
             * Supply
             */

            // Get Top Products Purchased
            config.Routes.MapHttpRoute(
                name: "GetTopProductsPurchased",
                routeTemplate: "api/getTopProductsPurchased",
                defaults: new { controller = "SupplyApi", Action = "GetTopProductsPurchased" }
            );

            config.Routes.MapHttpRoute(
                name: "GetTotalValueOfInventory",
                routeTemplate: "api/getTotalValueOfInventory",
                defaults: new { controller = "SupplyApi", Action = "GetTotalValueOfInventory" }
            );

            /*
            * Human Resources
            */

            // Get Number Of Employees
            config.Routes.MapHttpRoute(
                name: "GetNumberOfEmployees",
                routeTemplate: "api/getNumberOfEmployees",
                defaults: new { controller = "HumanResourcesApi", Action = "GetNumberOfEmployees" }
            );

            // Get Number Of Female Employees
            config.Routes.MapHttpRoute(
                name: "GetNumberOfFemaleEmployees",
                routeTemplate: "api/getNumberOfFemaleEmployees",
                defaults: new { controller = "HumanResourcesApi", Action = "GetNumberOfFemaleEmployees" }
            );

            // Get Average of employees by Age
            config.Routes.MapHttpRoute(
                name: "GetAverageEmployeeByAge",
                routeTemplate: "api/getNumberOfFemaleEmployees",
                defaults: new { controller = "HumanResourcesApi", Action = "GetAverageEmployeeByAge" }
            );

        


            /*
             * Other
             */
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            config.EnableSystemDiagnosticsTracing();
        }
    }
}
