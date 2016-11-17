using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Dashboard360.Items;
using Dashboard360.Lib_Primavera.Model;

namespace Dashboard360.Controllers
{
    public class FinancialController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
