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
    public class ProductsApiController : ApiController
    {
        // GET:     api/Products/
        // Returns: all products
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllProducts()
        {

            IEnumerable<Lib_Primavera.Model.Artigo> ProductsList = Lib_Primavera.PriIntegration.ListaArtigos();
            var res = new JavaScriptSerializer().Serialize(ProductsList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // GET:     api/Clients/GetTop10ProductsSold
        // Returns: Top 10 Products Sold
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTop10ProductsSold()
        {
            // TODO: check if there's top 10 clients in Enterprise View
            List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
            List<Lib_Primavera.Model.LinhaDocVenda> ListaVendas = Lib_Primavera.PriIntegration.GetVendasProduto();

            foreach (LinhaDocVenda it in ListaVendas)
            {
                if (it.Data.Year == 2016)
                {
                    if (!TopArtigos.Exists(art => art.CodArtigo == it.CodArtigo))
                    {
                        TopArtigos.Add(new ArtigoCounter
                        {
                            CodArtigo = it.CodArtigo,
                            DescArtigo = it.DescArtigo,
                            QuantidadeVendida = it.Quantidade,
                            VolumeVendas = it.TotalILiquido

                        });
                    }
                    else
                    {
                        TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).VolumeVendas += it.TotalILiquido;
                        TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).QuantidadeVendida += it.Quantidade;
                    }

                }
            }

            var toReturn = TopArtigos.OrderByDescending(prod => prod.VolumeVendas).ToList();
            toReturn = toReturn.GetRange(0, 10);
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
