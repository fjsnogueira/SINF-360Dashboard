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

            // Total Sales for Client 
            config.Routes.MapHttpRoute(
                name: "GetTotalSalesByClient",
                routeTemplate: "api/Clients/{clientID}/totalSalesByClient/{client}",
                defaults: new { controller = "ClientsApi", Action = "GetTotalSalesByClient" }
                );

            // Get top 10 Clients
            config.Routes.MapHttpRoute(
                name: "GetTop10ProductsSold",
                routeTemplate: "api/Products/GetTop10ProductsSold/{year}",
                defaults: new { controller = "ProductsApi", Action = "GetTop10ProductsSold" }
            );

            // Top Products by client
            config.Routes.MapHttpRoute(
                name: "GetTopProductsByClient",
                routeTemplate: "api/Products/topProductsByClient/{id}", 
                defaults: new { controller = "ProductsApi", Action = "GetTopProductsByClient" }
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
                routeTemplate: "api/Purchases/{year}",
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

            // Get product details
            config.Routes.MapHttpRoute(
                name: "GetProductDetails",
                routeTemplate: "api/products/{productID}",
                defaults: new { controller = "ProductsApi", Action = "GetProductDetails" }
            );

            // Get Top Buyers For Product 
            config.Routes.MapHttpRoute(
                name: "GetTopBuyers",
                routeTemplate: "api/products/{id}/GetTopBuyers/{year}",
                defaults: new { controller = "ProductsApi", Action = "GetTopBuyers" }
            );

            // Get Product Sales
            config.Routes.MapHttpRoute(
                name: "GetProductSales",
                routeTemplate: "api/products/GetSales/{productID}",
                defaults: new { controller = "ProductsApi", Action = "GetProductSales" }
            );

            // Get Product Inventory
            config.Routes.MapHttpRoute(
                name: "GetProductInventory",
                routeTemplate: "api/products/GetProductInventory/{productID}",
                defaults: new { controller = "ProductsApi", Action = "GetProductInventory" }
            );

            // Get Product Orders
            config.Routes.MapHttpRoute(
                name: "GetProductOrders",
                routeTemplate: "api/Products/GetProductOrders/{productID}",
                defaults: new { controller = "ProductsApi", Action = "GetProductOrders" }
            );



            // Get Seasonality
            config.Routes.MapHttpRoute(
                name: "GetSeasonality",
                routeTemplate: "api/Products/GetSeasonality/{productID}",
                defaults: new { controller = "ProductsApi", Action = "GetSeasonality" }
            );

            // Get Product Stock
            config.Routes.MapHttpRoute(
               name: "GetStock",
               routeTemplate: "api/products/GetStock/{productID}",
               defaults: new { controller = "ProductsApi", Action = "GetStock" }
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
            *
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
