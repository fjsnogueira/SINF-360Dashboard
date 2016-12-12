using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using Dashboard360.Lib_Primavera.Model;

namespace Dashboard360.Controllers
{
    public class ClientsController : Controller
    {
        // Customer Detail View
        // ../Clients/ShowDetails/{clientID}
        public async Task<ActionResult> ShowDetails(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49751/api/clients/" + id);

            var client = await response.Content.ReadAsAsync<Cliente>();

            System.Diagnostics.Debug.WriteLine(client.CodCliente);
            ViewData["clientID"] = id;
            return View(client);
        }

        int Year;
        // Customer View
        // --/Clients/
        public ActionResult Index()
        {
            Year = DateTime.Today.Year;
            ViewData["Year"] = Year;
            return View();
        }
    }
}
