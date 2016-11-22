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
        public HttpResponseMessage GetAllSales()
        {

            IEnumerable<Lib_Primavera.Model.CabecDoc> salesList = Lib_Primavera.PriIntegration.ListaVendas();
            var res = new JavaScriptSerializer().Serialize(salesList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
