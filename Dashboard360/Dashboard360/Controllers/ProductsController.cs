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
    public class ProductsController : Controller
    {
        // Profucts View
        // --/Products/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ShowDetails(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49751/api/products/" + id);

            var product = await response.Content.ReadAsAsync<Artigo>();
            System.Diagnostics.Debug.WriteLine("id: " + id);
            ViewData["productID"] = id;
            return View(product);
        }
    }
}
