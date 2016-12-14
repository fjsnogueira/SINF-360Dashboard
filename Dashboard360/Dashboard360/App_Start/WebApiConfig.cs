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

            // Get client details
            config.Routes.MapHttpRoute(
                name: "GetClientDetails",
                routeTemplate: "api/Clients/{clientID}",
                defaults: new { controller = "ClientsApi", Action = "GetClientDetails" }
            );



            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10Clients",
                routeTemplate: "api/Clients/GetTop10Clients/{year}",
                defaults: new { controller = "ClientsApi", Action = "GetTop10Clients" }
            );

            config.Routes.MapHttpRoute(
                name: "GetNumClients",
                routeTemplate: "api/Clients/GetNumClients",
                defaults: new { controller = "ClientsApi", Action = "GetNumClients" }
            );

            config.Routes.MapHttpRoute(
                name: "GetNumActiveClients",
                routeTemplate: "api/Clients/GetNumActiveClients/{year}",
                defaults: new { controller = "ClientsApi", Action = "GetNumActiveClients" }
            );


            /*
             * Sales
             */

            // Get all sales
            config.Routes.MapHttpRoute(
                name: "Sales",
                routeTemplate: "api/Sales/{year}",
                defaults: new { controller = "SalesApi", Action = "GetAllSales" }
            );

            config.Routes.MapHttpRoute(
                name: "SalesValues",
                routeTemplate: "api/SalesValues/{year}",
                defaults: new { controller = "SalesApi", Action = "GetAllSalesValues" }
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
                routeTemplate: "api/Products/",
                defaults: new { controller = "ProductsApi", Action = "GetAllProducts" }
            );

            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10ProductsSold",
                routeTemplate: "api/Products/GetTop10ProductsSold/{year}",
                defaults: new { controller = "ProductsApi", Action = "GetTop10ProductsSold" }
            );

            /*
             * Supply
             */

            // Get Top Products Purchased
            config.Routes.MapHttpRoute(
                name: "GetTopProductsPurchased",
                routeTemplate: "api/GetTopProductsPurchased",
                defaults: new { controller = "SupplyApi", Action = "GetTopProductsPurchased" }
            );

            // Get total value of Inventory
            config.Routes.MapHttpRoute(
                name: "GetTotalValueOfInventory",
                routeTemplate: "api/GetTotalValueOfInventory",
                defaults: new { controller = "SupplyApi", Action = "GetTotalValueOfInventory" }
            );

            // Get total purchased value
            config.Routes.MapHttpRoute(
                name: "GetTotalPurchasedValue",
                routeTemplate: "api/GetTotalPurchasedValue",
                defaults: new { controller = "SupplyApi", Action = "GetTotalPurchasedValue" }
            );

            // Get average purchased value
            config.Routes.MapHttpRoute(
                name: "GetAveragePurchasedValue",
                routeTemplate: "api/GetAveragePurchasedValue",
                defaults: new { controller = "SupplyApi", Action = "GetAveragePurchasedValue" }
            );






            /*
            * Human Resources
            */

            // Get Number Of Employees
            config.Routes.MapHttpRoute(
                name: "GetNumberOfEmployees",
                routeTemplate: "api/GetNumberOfEmployees/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetNumberOfEmployees" }
            );

            // Get Number Of Female Employees
            config.Routes.MapHttpRoute(
                name: "GetNumberOfFemaleEmployees",
                routeTemplate: "api/getNumberOfFemaleEmployees/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetNumberOfFemaleEmployees" }
            );

            // Get Average of employees by Age
            config.Routes.MapHttpRoute(
                name: "GetAverageEmployeeByAge",
                routeTemplate: "api/GetAverageEmployeeByAge/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetAverageEmployeeByAge" }
            );

            // Get Human Resources Expenses
            config.Routes.MapHttpRoute(
                name: "GetHumanResourcesExpenses",
                routeTemplate: "api/GetHumanResourcesExpenses/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetHumanResourcesExpenses" }
            );

            // Get Customers Per Employees
            config.Routes.MapHttpRoute(
                name: "GetCustomersPerEmployees",
                routeTemplate: "api/GetCustomersPerEmployees/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetCustomersPerEmployees" }
            );

            // Get Customers Per Employees
            config.Routes.MapHttpRoute(
                name: "GetAverageEmplyomentInCompany",
                routeTemplate: "api/GetAverageEmplyomentInCompany",
                defaults: new { controller = "HumanResourcesApi", Action = "GetAverageEmplyomentInCompany" }
            );

            // Get Revenue Per Employee
            config.Routes.MapHttpRoute(
                name: "GetRevenuePerEmployee",
                routeTemplate: "api/GetRevenuePerEmployee/{year}",
                defaults: new { controller = "HumanResourcesApi", Action = "GetRevenuePerEmployee" }
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
