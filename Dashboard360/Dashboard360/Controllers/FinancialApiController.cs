using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Dashboard360.Controllers
{
    public class FinancialApiController : ApiController
    {
        // GET:     api/payables/{year}
        // Returns: all payables
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllPayables(string year)
        {
            IEnumerable<Lib_Primavera.Model.Pendente> pendingList = Lib_Primavera.PriIntegration.GetPending(year, false);
            var res = new JavaScriptSerializer().Serialize(pendingList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // GET:     api/receivables/{year}
        // Returns: all receivables
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllReceivables(string year)
        {
            IEnumerable<Lib_Primavera.Model.Pendente> pendingList = Lib_Primavera.PriIntegration.GetPending(year, true);
            var res = new JavaScriptSerializer().Serialize(pendingList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        // GET:     api/flow/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFlow(string year)
        {
            IEnumerable<Lib_Primavera.Model.AcumuladosFluxos> pendingList = Lib_Primavera.PriIntegration.GetFlow(year);
            var res = new JavaScriptSerializer().Serialize(pendingList);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
