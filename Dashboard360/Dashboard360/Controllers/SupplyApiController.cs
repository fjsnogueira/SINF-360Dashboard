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
        public HttpResponseMessage GetTopProductsPurchased()
        {

            string[] tiposDocs = { "NC", "ND", "VD", "DV", "AVE" };

            List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
            IEnumerable<Lib_Primavera.Model.DocCompra> topProductsPur = Lib_Primavera.PriIntegration.ListaCompras();

            foreach (DocCompra itDocVenda in topProductsPur)
            {
              
               // if (itDocVenda.Data.Year == 2016 && (itDocVenda.TipoDoc[0] == 'F' || tiposDocs.Contains(itDocVenda.TipoDoc)))
                //{
                    List<LinhaDocCompra> linhasDoc = itDocVenda.LinhasDoc;
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
                //}
            }

            var toReturn = TopArtigos.OrderByDescending(prod => prod.VolumeVendas).ToList();

            toReturn = toReturn.GetRange(0, 10);
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetTotalValueOfInventory()
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> totalInventory = Lib_Primavera.PriIntegration.InventaryValue();
            var res = new JavaScriptSerializer().Serialize(totalInventory);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        public HttpResponseMessage GetTotalPurchasedValue()
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> totalPurchased = Lib_Primavera.PriIntegration.TotalPurchasedValue();
            var res = new JavaScriptSerializer().Serialize(totalPurchased);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetAveragePurchasedValue()
        {

            IEnumerable<Lib_Primavera.Model.DocCompra> averagePurchasedValue = Lib_Primavera.PriIntegration.TotalPurchasedValue();
            var res = new JavaScriptSerializer().Serialize(averagePurchasedValue);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


    }
}

