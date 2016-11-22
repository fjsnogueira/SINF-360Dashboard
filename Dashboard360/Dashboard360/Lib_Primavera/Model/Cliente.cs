using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class Cliente
    {
        public string Morada;

        /* Exemplo para POST e GET com valores específicos
         public string Morada
        {
            get
            {
                return "MORADA: " + _morada;
            }
            set
            {
                _morada = value;
            }
        }
    
*/
        public string CodCliente
        {
            get;
            set;
        }

        public string NomeCliente
        {
            get;
            set;
        }

        public string NumContribuinte
        {
            get;
            set;
        }

        public string Moeda
        {
            get;
            set;
        }

    }

    public class ClienteCounter
    {
        public string Nome
        {
            get;
            set;
        }

        public string CodCliente
        {
            get;
            set;
        }

        public double TotalCompras
        {
            get;
            set;
        }

        public int NumeroCompras
        {
            get;
            set;
        }
    }
}