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
    public class SalesApiController : ApiController
    {
        // GET:     api/Sales/
        // Returns: all sales
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllSales(string year)
        {

            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendas(year);


            var res = new JavaScriptSerializer().Serialize(salesList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [System.Web.Http.HttpGet]
        public double GetAllSalesValues(string year)
        {
            double totalSales = 0;
            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendas(year);

            foreach (DocVenda it in salesList)
            {
                if (it.TipoDoc != "CBA" && it.TipoDoc != "GR")
                    totalSales += (it.TotalMerc - it.TotalDesc + it.TotalOutros);
            }

            return totalSales;
        }

    }
}
