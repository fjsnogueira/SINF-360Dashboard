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
    public class HumanResourcesApiController : ApiController
    {
        // GET:     api/HumanResources/GetNumberOfEmployees
        // Returns: Number of employees
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetNumberOfEmployees()
        {

            IEnumerable<Lib_Primavera.Model.Empregado> numberOfEmployees = Lib_Primavera.PriIntegration.ListOfEmployees();
            var res = new JavaScriptSerializer().Serialize(numberOfEmployees);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetNumberOfFemaleEmployees()
        {

            IEnumerable<Lib_Primavera.Model.Empregado> numberOfFemaleEmployees = Lib_Primavera.PriIntegration.ListOfFemaleEmployees();
            var res = new JavaScriptSerializer().Serialize(numberOfFemaleEmployees);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetAverageEmployeeByAge()
        {

            IEnumerable<Lib_Primavera.Model.Empregado> averageEmployeeByAge = Lib_Primavera.PriIntegration.ListOfAgeOfEmployees();
            var res = new JavaScriptSerializer().Serialize(averageEmployeeByAge);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


    }
}

