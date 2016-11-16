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
using System.Web.Script.Serialization;
using Dashboard360.Lib_Primavera.Model;
using Dashboard360.Items;

namespace Dashboard360.Controllers
{
    public class ClientsApiController : ApiController
    {
        // GET:     api/Clients/
        // Returns: all clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllClients()
        {
            IEnumerable<Lib_Primavera.Model.Cliente> clientList = Lib_Primavera.PriIntegration.ListaClientes();
            var res = new JavaScriptSerializer().Serialize(clientList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // GET:     api/Clients/{clientID}
        // Returns: clientID's information
        [System.Web.Http.HttpGet]
        public Cliente GetClient(string clientCode)
        {
            Lib_Primavera.Model.Cliente client = Lib_Primavera.PriIntegration.GetCliente(clientCode);
            if (client == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return client;
        }

        // GET:     api/clients/top
        // Returns: Top 10 Clients
/*        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTop10Clients()
        {
            // TODO: check if there's top 10 clients in Enterprise View

            var res = null;// new JavaScriptSerializer().Serialize(smth);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }*/
    }
}

