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
    public class ClientsController : Controller
    {

        public async Task<ActionResult> ShowDetails(string clientID)
        {
            var httpClient = new HttpClient();
            var res = await httpClient.GetAsync("http://localhost:49751/api/Clients + id");
            var client = await res.Content.ReadAsAsync<Cliente>();
            ViewData["clientID"] = clientID;
            return View(client);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
