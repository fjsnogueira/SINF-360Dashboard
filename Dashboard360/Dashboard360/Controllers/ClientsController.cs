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
    public class ClientsController : Controller
    {
        // Customer Detail View
        // ../Clients/ShowDetails/{clientID}
        public async Task<ActionResult> ShowDetails(string clientID)
        {
            var httpClient = new HttpClient();
            System.Diagnostics.Debug.WriteLine("clientID: " + clientID);
            var res = await httpClient.GetAsync("http://localhost:49751/api/Clients/ALCAD");
            var _res = res.Content.ReadAsAsync<string>();
            System.Diagnostics.Debug.WriteLine("_res: " + _res);
            var client = await res.Content.ReadAsAsync<Cliente>();
           // System.Diagnostics.Debug.WriteLine(client.getCodCliente());
            ViewData["clientID"] = clientID;
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
