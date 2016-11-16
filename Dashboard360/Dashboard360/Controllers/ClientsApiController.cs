using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Globalization;



using Dashboard360.Lib_Primavera.Model;
using Dashboard360.Items;

namespace Dashboard360.Controllers
{
    public class ClientsApiController : ApiController
    {
        // GET:     api/Clients/
        // Returns: all clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<Lib_Primavera.Model.Cliente> clients = Lib_Primavera.PriIntegration.ListaClientes();
           // var json = new JavaScriptSerializer().Serialize(clients);
            string res = "deu";
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // Test if it's working
        [System.Web.Http.HttpGet]
        public string Test()
        {
            return "clients working";
        }
    }
}

