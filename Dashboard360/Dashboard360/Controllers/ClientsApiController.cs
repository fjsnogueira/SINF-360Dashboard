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


        // Get Client details
        // GET api/clients/{entity}
        [System.Web.Http.HttpGet]
        public Cliente GetClientDetails(string entity)
        {
            // return the target client through entity
            Lib_Primavera.Model.Cliente cliente = Lib_Primavera.PriIntegration.GetCliente(entity);
            if (cliente == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return cliente;
            }
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
                if (it.TipoDoc != "CBA" && it.TipoDoc != "GR")
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

