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
        string[] tiposDocs = { "NC", "ND", "VD", "DV", "AVE" };
        // GET:     api/Clients/
        // Returns: all clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllClients()
        {
            IEnumerable<Lib_Primavera.Model.Cliente> clientList = Lib_Primavera.PriIntegration.ListaClientes();
            var res = new JavaScriptSerializer().Serialize(clientList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        // Get Client details
        // GET api/clients/{entity}
        [System.Web.Http.HttpGet]
        public Cliente GetClientDetails(string clientID)
        {
            Lib_Primavera.Model.Cliente client = Lib_Primavera.PriIntegration.GetCliente(clientID);
            if (client == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return client;
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTotalSalesByClient(string client)
        {
            System.Diagnostics.Debug.WriteLine(client);
            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendasCliente(client);

            var res = new JavaScriptSerializer().Serialize(salesList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }



        // GET:     api/Clients/GetTop10Clients
        // Returns: Top 10 Clients
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTop10Clients(string year)
        {
            // TODO: check if there's top 10 clients in Enterprise View
            List<Lib_Primavera.Model.ClienteCounter> topClients = new List<Lib_Primavera.Model.ClienteCounter>();
            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendas(year);

            foreach (DocVenda it in salesList)
            {
                if (tiposDocs.Contains(it.TipoDoc) || it.TipoDoc[0] == 'F')
                {
                    if (!topClients.Exists(cli => cli.CodCliente == it.Entidade))
                    {
                        topClients.Add(new ClienteCounter
                        {
                            CodCliente = it.Entidade,
                            TotalCompras = (it.TotalMerc - it.TotalDesc + it.TotalOutros),
                            NumeroCompras = 1
                        });

                    }
                    else
                    {
                        topClients.Find(cli => cli.CodCliente == it.Entidade).NumeroCompras++;
                        topClients.Find(cli => cli.CodCliente == it.Entidade).TotalCompras += (it.TotalMerc - it.TotalDesc + it.TotalOutros);
                    }
                }

            }
            // order
            var toReturn = topClients.OrderByDescending(cli => cli.TotalCompras).ToList();
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // GET:     
        // Returns: 
        [System.Web.Http.HttpGet]
        public int GetNumClients()
        {
            IEnumerable<Lib_Primavera.Model.Cliente> clientList = Lib_Primavera.PriIntegration.ListaClientes();
            return clientList.Count();
        }

        public int GetNumActiveClients(string year)
        {
            System.Diagnostics.Debug.WriteLine(year);
            List<Lib_Primavera.Model.ClienteCounter> CliCounter = new List<Lib_Primavera.Model.ClienteCounter>();
            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendas(year);

            foreach (DocVenda it in salesList)
            {
                if (!CliCounter.Exists(cli => cli.CodCliente == it.Entidade))
                {
                    CliCounter.Add(new ClienteCounter
                    {
                        CodCliente = it.Entidade,
                        Nome = it.Entidade,
                    });
                }

            }
            return CliCounter.Count();
        }
    }
}

