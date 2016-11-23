using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Dashboard360.Controllers
{
    public class ReceivablesController : ApiController
    {
        // GET api/receivables
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllReceivables()
        {
            IEnumerable<Lib_Primavera.Model.Pendente> pendingList = Lib_Primavera.PriIntegration.GetPending(true);
            var res = new JavaScriptSerializer().Serialize(pendingList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}