using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Dashboard360.Lib_Primavera.Model;

namespace Dashboard360.Controllers
{
    public class SupplyController : Controller
    {
        // Supply View

        int Year;

        public ActionResult Index()
        {
            Year = DateTime.Today.Year;
            ViewData["Year"] = Year;
            return View();
        }
    }
}
