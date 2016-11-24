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
      /*  [System.Web.Http.HttpGet]
        public Cliente GetClient(string clientID)
        {
            System.Diagnostics.Debug.WriteLine("[ClientsApiController] entrou");
            Lib_Primavera.Model.Cliente client = Lib_Primavera.PriIntegration.GetCliente(clientID);
            if (client == null)
            {
                System.Diagnostics.Debug.WriteLine("[ClientsApiController] ERROR: client not found");
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return client;
        }*/

        // GET:     api/Clients/GetTop10Clients
        // Returns: Top 10 Clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTop10Clients()
        {
            // TODO: check if there's top 10 clients in Enterprise View
            List<Lib_Primavera.Model.ClienteCounter> topClients = new List<Lib_Primavera.Model.ClienteCounter>();
            IEnumerable<Lib_Primavera.Model.CabecDoc> salesList = Lib_Primavera.PriIntegration.ListaVendas();

            foreach (CabecDoc it in salesList)
            {
                if (!topClients.Exists(cli => cli.CodCliente == it.Entidade)){
                    topClients.Add(new ClienteCounter
                    {
                        CodCliente = it.Entidade,
                        Nome = it.Nome,
                        TotalCompras = (it.TotalMerc + it.TotalIva),
                        NumeroCompras = 1
                    });

                }
                else
                {
                    topClients.Find(cli => cli.CodCliente == it.Entidade).NumeroCompras++;
                    topClients.Find(cli => cli.CodCliente == it.Entidade).TotalCompras += (it.TotalMerc + it.TotalIva);
                }
            }
            // order
            var toReturn = topClients.OrderByDescending(cli => cli.TotalCompras).ToList();
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}

