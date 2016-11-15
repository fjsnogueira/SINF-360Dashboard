using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
//using 
//using project.Lib_Primavera.Model;
//using project.Items;


namespace _360Dashboard.Controllers
{
    public class ClientsApiController : ApiController
    {
        //GET api/clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            // return all clients
            IEnumerable<Lib_Primavera.Model.Cliente> clients = Lib_Primavera.PriIntegration.ListaClientes();


            var json = new JavaScriptSerializer().Serialize(clients);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
    }
}
