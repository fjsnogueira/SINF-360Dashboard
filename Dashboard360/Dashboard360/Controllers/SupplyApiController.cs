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

            IEnumerable<Lib_Primavera.Model.FornecedorProductCounter> topProductsPur = Lib_Primavera.PriIntegration.ListTopProducts();
            var res = new JavaScriptSerializer().Serialize(topProductsPur);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetTotalValueOfInventory()
        {

            IEnumerable<Lib_Primavera.Model.Inventario> clientList = Lib_Primavera.PriIntegration.InventaryValue();
            var res = new JavaScriptSerializer().Serialize(clientList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


    }
}

