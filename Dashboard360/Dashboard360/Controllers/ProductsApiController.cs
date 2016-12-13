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
        public HttpResponseMessage GetAllProducts(string year)
        {

            IEnumerable<Lib_Primavera.Model.Artigo> ProductsList = Lib_Primavera.PriIntegration.ListaArtigos();
            var res = new JavaScriptSerializer().Serialize(ProductsList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        // GET api/products/{productID}
        [System.Web.Http.HttpGet]
        public List<Lib_Primavera.Model.DocVenda> GetProductSales(string productID)
        {
            List<Lib_Primavera.Model.DocVenda> vendas = Lib_Primavera.PriIntegration.ListaVendasArtigo(productID);
            if (vendas == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return vendas;
            }
        }


        [System.Web.Http.HttpGet]
        public Artigo GetProductInventory(string productID)
        {
            /* Lib_Primavera.Model.Artigo product = Lib_Primavera.PriIntegration.GetArtigo(productID);
             if (product == null)
             {
                 throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
             }
             else
             {
                 return product;
             }*/
            return null;
        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTopBuyers(string id, int year)
        {
            List<ClienteCounter> topBuyers = new List<ClienteCounter>();
            List<CabecDoc> productSales = Lib_Primavera.PriIntegration.GetTopClientesArtigo(id, year);
            string[] DocTypes = { "NC", "ND", "VD", "DV", "AVE" };
            foreach (CabecDoc it in productSales)
            {
                if (it.Data.Year == year && (it.TipoDoc[0] == 'F' || DocTypes.Contains(it.TipoDoc)))
                {
                    if (topBuyers.Exists(cli => cli.CodCliente == it.Entidade))
                    {
                        topBuyers.Find(cli => cli.CodCliente == it.Entidade).TotalCompras += (it.TotalMerc + it.TotalOutros - it.TotalDesc);
                        topBuyers.Find(cli => cli.CodCliente == it.Entidade).NumeroCompras++;
                    }
                    else
                    {
                        topBuyers.Add(new ClienteCounter
                        {
                            CodCliente = it.Entidade,
                            Nome = it.Nome,
                            TotalCompras = (it.TotalMerc + it.TotalOutros - it.TotalDesc),
                            NumeroCompras = 1
                        });
                    }
                }

            }
            var res = new JavaScriptSerializer().Serialize(topBuyers);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        [System.Web.Http.HttpGet]
        public Artigo GetProductDetails(string productID)
        {
            Lib_Primavera.Model.Artigo product = Lib_Primavera.PriIntegration.GetArtigo(productID);
            if (product == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return product;
            }
        }


        // GET:     api/Clients/GetTop10ProductsSold
        // Returns: Top 10 Products Sold
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTop10ProductsSold(string year)
        {
            string[] tiposDocs = { "NC", "ND", "VD", "DV", "AVE" };
            List<Lib_Primavera.Model.DocVenda> DocsVenda = Lib_Primavera.PriIntegration.GetVendasProduto(year);
            List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
            foreach (DocVenda itDocVenda in DocsVenda)
            {
                if (itDocVenda.Data.Year == 2016 && (itDocVenda.TipoDoc[0] == 'F' || tiposDocs.Contains(itDocVenda.TipoDoc)))
                {
                    List<LinhaDocVenda> linhasDoc = itDocVenda.LinhasDoc;
                    foreach (LinhaDocVenda it in linhasDoc)
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

            /* List<Lib_Primavera.Model.LinhaDocVenda> ListaVendas = Lib_Primavera.PriIntegration.GetVendasProduto(year);
              List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
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
                              VolumeVendas = it.TotalLiquido

                          });
                      }
                      else
                      {
                          TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).VolumeVendas += it.TotalLiquido;
                          TopArtigos.Find(art => art.CodArtigo == it.CodArtigo).QuantidadeVendida += it.Quantidade;
                      }
                  }


              }*/
            var toReturn = TopArtigos.OrderByDescending(prod => prod.VolumeVendas).ToList();

            toReturn = toReturn.GetRange(0, 10);
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetTopProductsByClient(string id)
        {
            // TODO: check if there's top 10 clients in Enterprise View
            List<Lib_Primavera.Model.ArtigoCounter> TopArtigos = new List<Lib_Primavera.Model.ArtigoCounter>();
            List<Lib_Primavera.Model.LinhaDocVenda> ListaVendas = Lib_Primavera.PriIntegration.GetVendasCliente(id);

            foreach (LinhaDocVenda it in ListaVendas)
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
            var toReturn = TopArtigos.OrderByDescending(prod => prod.VolumeVendas).ToList();
            // toReturn = toReturn.GetRange(0, 10);
            var res = new JavaScriptSerializer().Serialize(toReturn);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
