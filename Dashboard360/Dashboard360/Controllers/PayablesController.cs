using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Dashboard360.Controllers
{
    public class PayablesController : ApiController
    {
        // GET api/payables
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllReceivables()
        {
            IEnumerable<Lib_Primavera.Model.Pendente> pendingList = Lib_Primavera.PriIntegration.GetPending(false);
            var res = new JavaScriptSerializer().Serialize(pendingList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}