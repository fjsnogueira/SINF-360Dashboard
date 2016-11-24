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
    public class PurchasesApiController : ApiController
    {
        // GET:     api/Purchases/
        // Returns: all purchases
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllPurchases()
        {

            List<Lib_Primavera.Model.DocCompra> PurchaseList = Lib_Primavera.PriIntegration.ListaComprasFA();
            var res = new JavaScriptSerializer().Serialize(PurchaseList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
