using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class Empregado
    {

        public string nome
        {
            get;
            set;
        }

        public DateTime dataAdmissao
        {
            get;
            set;
        }

        public DateTime dataDemissao
        {
            get;
            set;
        }

        public string sexo
        {
            get;
            set;
        }

        public double subsidioAlim
        {
            get;
            set;
        }

        public int numeroRaparigasEmpregadas
        {
            get;
            set;
        }

        public int idade
        {
            get;
            set;
        }

        public DateTime dataNascimento
        {
            get;
            set;
        }

        public double salario
        {
            get;
            set;
        }

        
    }
}