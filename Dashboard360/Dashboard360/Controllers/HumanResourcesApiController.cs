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
        public HttpResponseMessage GetNumberOfEmployees(string year)
        {
            List<Lib_Primavera.Model.Empregado> TrabalhadoresYear = new List<Lib_Primavera.Model.Empregado>();
            IEnumerable<Lib_Primavera.Model.Empregado> numberOfEmployees = Lib_Primavera.PriIntegration.ListOfEmployees();



            foreach (Empregado it in numberOfEmployees)
            {

                if (it.dataAdmissao.Year <= Convert.ToDouble(year))
                {
                    if (it.dataDemissao.Year >= Convert.ToDouble(year) || (it.dataDemissao.Year == 1))
                    {
                        TrabalhadoresYear.Add(new Empregado
                        {
                            nome = it.nome,
                            sexo = it.sexo,
                            dataNascimento = it.dataNascimento,
                            salario = it.salario,
                            dataAdmissao = it.dataAdmissao,
                            subsidioAlim = it.subsidioAlim

                        });
                    }

                }
            }



            var res = new JavaScriptSerializer().Serialize(TrabalhadoresYear);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetNumberOfFemaleEmployees(string year)
        {

            IEnumerable<Lib_Primavera.Model.Empregado> numberOfFemaleEmployees = Lib_Primavera.PriIntegration.ListOfEmployees();
            List<Lib_Primavera.Model.Empregado> TrabalhadoresFemininos = new List<Lib_Primavera.Model.Empregado>();


            foreach (Empregado it in numberOfFemaleEmployees)
            {
                if (it.dataAdmissao.Year <= Convert.ToDouble(year))
                {
                    if (it.dataDemissao.Year >= Convert.ToDouble(year) || (it.dataDemissao.Year == 1))
                    {
                        TrabalhadoresFemininos.Add(new Empregado
                        {
                            nome = it.nome,
                            sexo = it.sexo,
                            dataNascimento = it.dataNascimento,
                            salario = it.salario,
                            dataAdmissao = it.dataAdmissao,
                            subsidioAlim = it.subsidioAlim

                        });
                    }

                }
            }



            var res = new JavaScriptSerializer().Serialize(TrabalhadoresFemininos);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetAverageEmployeeByAge(string year)
        {

            IEnumerable<Lib_Primavera.Model.Empregado> averageEmployeeByAge = Lib_Primavera.PriIntegration.ListOfEmployees();
            List<Lib_Primavera.Model.Empregado> ListaParaIdades = new List<Lib_Primavera.Model.Empregado>();

            foreach (Empregado it in averageEmployeeByAge)
            {
                if (it.dataAdmissao.Year <= Convert.ToDouble(year))
                {
                    if (it.dataDemissao.Year >= Convert.ToDouble(year) || (it.dataDemissao.Year == 1))
                    {
                        ListaParaIdades.Add(new Empregado
                        {
                            nome = it.nome,
                            sexo = it.sexo,
                            dataNascimento = it.dataNascimento,
                            salario = it.salario,
                            dataAdmissao = it.dataAdmissao,
                            subsidioAlim = it.subsidioAlim

                        });
                    }

                }
            }


            var res = new JavaScriptSerializer().Serialize(ListaParaIdades);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetHumanResourcesExpenses(string year)
        {

            IEnumerable<Lib_Primavera.Model.Empregado> humanResourcesExpenses = Lib_Primavera.PriIntegration.ListOfEmployees();
            var res = new JavaScriptSerializer().Serialize(humanResourcesExpenses);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetCustomersPerEmployees(string year)
        {

            IEnumerable<Lib_Primavera.Model.Empregado> employees = Lib_Primavera.PriIntegration.ListOfEmployees();
            IEnumerable<Lib_Primavera.Model.Cliente> clients = Lib_Primavera.PriIntegration.ListaClientes();
            List<Lib_Primavera.Model.Empregado> TrabalhadoresYear = new List<Lib_Primavera.Model.Empregado>();



            foreach (Empregado it in employees)
            {

                if (it.dataAdmissao.Year <= Convert.ToDouble(year))
                {
                    if (it.dataDemissao.Year >= Convert.ToDouble(year) || (it.dataDemissao.Year == 1))
                    {
                        TrabalhadoresYear.Add(new Empregado
                        {
                            nome = it.nome,
                            sexo = it.sexo,
                            dataNascimento = it.dataNascimento,
                            salario = it.salario,
                            dataAdmissao = it.dataAdmissao,
                            subsidioAlim = it.subsidioAlim

                        });
                    }

                }
            }

            var ratio = Math.Truncate(Decimal.Divide(clients.Count(), TrabalhadoresYear.Count()) * 1000) / 1000;

            var res = new JavaScriptSerializer().Serialize(ratio);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetAverageEmplyomentInCompany()
        {

            IEnumerable<Lib_Primavera.Model.Empregado> averageEmply = Lib_Primavera.PriIntegration.ListOfEmployees();
            var res = new JavaScriptSerializer().Serialize(averageEmply);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        public HttpResponseMessage GetRevenuePerEmployee(string year)
        {

            var revenue = GetAllSalesValues(year);
            IEnumerable<Lib_Primavera.Model.Empregado> employees = Lib_Primavera.PriIntegration.ListOfEmployees();
            List<Lib_Primavera.Model.Empregado> TrabalhadoresYear = new List<Lib_Primavera.Model.Empregado>();



            foreach (Empregado it in employees)
            {

                if (it.dataAdmissao.Year <= Convert.ToDouble(year))
                {
                    if (it.dataDemissao.Year >= Convert.ToDouble(year) || (it.dataDemissao.Year == 1))
                    {
                        TrabalhadoresYear.Add(new Empregado
                        {
                            nome = it.nome,
                            sexo = it.sexo,
                            dataNascimento = it.dataNascimento,
                            salario = it.salario,
                            dataAdmissao = it.dataAdmissao,
                            subsidioAlim = it.subsidioAlim

                        });
                    }

                }
            }


            var ratio = Math.Truncate(revenue / TrabalhadoresYear.Count() * 1000) / 1000;

            var res = new JavaScriptSerializer().Serialize(ratio);
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        [System.Web.Http.HttpGet]
        public double GetAllSalesValues(string year)
        {
            string[] tiposDocs = { "NC", "ND", "VD", "DV", "AVE" };
            double totalSales = 0;
            IEnumerable<Lib_Primavera.Model.DocVenda> salesList = Lib_Primavera.PriIntegration.ListaVendas(year);

            foreach (DocVenda it in salesList)
            {

                if (tiposDocs.Contains(it.TipoDoc) || it.TipoDoc[0] == 'F')
                    totalSales += (it.TotalMerc - it.TotalDesc + it.TotalOutros);
            }

            return totalSales;
        }
    }
}

