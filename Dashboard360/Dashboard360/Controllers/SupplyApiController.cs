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
    public class SupplyApiController : ApiController
    {
        // GET:     api/Supply/GetTopProductsPurchased
        // Returns: Top Products Purchased
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTopProductsPurchased(string year)
        {

            string[] tiposDocs = { "VNC", "VND", "VVD", "VFA", "VFG", "VFI", "VFM", "VFO", "VFP", "VFR" };

            List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
            IEnumerable<Lib_Primavera.Model.DocCompra> topProductsPur = Lib_Primavera.PriIntegration.ListaComprasNew(year);

            foreach (DocCompra itDocCompra in topProductsPur)
            {
                if (tiposDocs.Contains(itDocCompra.TipoDoc))
                {
                    List<LinhaDocCompra> linhasDoc = itDocCompra.LinhasDoc;
                    foreach (LinhaDocCompra it in linhasDoc)
                    {
                        if (!TopArtigos.Exists(art => art.CodArtigo == it.CodArtigo))
                        {
                            TopArtigos.Add(new ArtigoCounter
                            {
                                CodArtigo = it.CodArtigo,
                                DescArtigo = it.DescArtigo,
                                QuantidadeVendida = it.Quantidade,
                                VolumeVendas = it.TotalLiquido

                            });
                        }
                        else
                        {
                            TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).VolumeVendas += it.TotalLiquido;
                            TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).QuantidadeVendida += it.Quantidade;
                        }
                    }
                }
            }

            var toReturn = TopArtigos.OrderBy(prod => prod.VolumeVendas).ToList();

            if (toReturn.Count() >= 10)
            {
                toReturn = toReturn.GetRange(0, 10);
            }

            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetTotalValueOfInventory(string year)
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> totalInventory = Lib_Primavera.PriIntegration.InventaryValue(year);

            var res = new JavaScriptSerializer().Serialize(totalInventory);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        public HttpResponseMessage GetTotalPurchasedValue(string year)
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> totalPurchased = Lib_Primavera.PriIntegration.TotalPurchasedValue(year);

            var res = new JavaScriptSerializer().Serialize(totalPurchased);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetInventoryTop10Product(string year)
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> totalPurchased = Lib_Primavera.PriIntegration.GetInventarioArtigo(year);

            var res = new JavaScriptSerializer().Serialize(totalPurchased);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

       

        public HttpResponseMessage GetAveragePurchasedValue(string year)
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> averagePurchasedValue = Lib_Primavera.PriIntegration.TotalPurchasedValue(year);

            var res = new JavaScriptSerializer().Serialize(averagePurchasedValue);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetTopSuppliers(string year)
        {
            string[] tiposDocs = { "VNC", "VND", "VVD", "VFA", "VFG", "VFI", "VFM", "VFO", "VFP", "VFR" };
            // TODO: check if there's top 10 clients in Enterprise View
            List<Lib_Primavera.Model.ClienteCounter> topBuyers = new List<Lib_Primavera.Model.ClienteCounter>();
            IEnumerable<Lib_Primavera.Model.DocCompra> purchasesList = Lib_Primavera.PriIntegration.ListaComprasNew(year);

            foreach (DocCompra it in purchasesList)
            {
                if (tiposDocs.Contains(it.TipoDoc))
                {
                    if (!topBuyers.Exists(cli => cli.CodCliente == it.Entidade))
                    {
                        topBuyers.Add(new ClienteCounter
                        {
                            CodCliente = it.Entidade,
                            TotalCompras = (it.TotalMerc - it.TotalDesc + it.TotalOutros),
                            NumeroCompras = 1
                        });

                    }
                    else
                    {
                        topBuyers.Find(cli => cli.CodCliente == it.Entidade).NumeroCompras++;
                        topBuyers.Find(cli => cli.CodCliente == it.Entidade).TotalCompras += (it.TotalMerc - it.TotalDesc + it.TotalOutros);
                    }
                }

            }
            // order
            var toReturn = topBuyers.OrderBy(cli => cli.TotalCompras).ToList();
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


    }
}
