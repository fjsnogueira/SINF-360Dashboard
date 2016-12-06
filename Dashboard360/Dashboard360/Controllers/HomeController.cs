using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard360.Controllers
{
    public class HomeController : Controller
    {

        public static List<Lib_Primavera.Model.ClienteCounter> topClientsCache;
        public static List<Lib_Primavera.Model.ArtigoCounter> topProductsCache;
        public static float avgExpensePerCustomerCache;

        public ActionResult Index()
        {
            return View();
        }
    }
}
