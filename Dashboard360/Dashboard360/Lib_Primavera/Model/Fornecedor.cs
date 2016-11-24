using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class TopFornecedor
    {
        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }

    }

    public class FornecedorProductCounter
    {
        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }
        public string UnidadeEntrada
        {
            get;
            set;
        }
    }

    public class Fornecedor
    {
        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }

    }

    public class Inventario
    {
        public double Valor
        {
            get;
            set;
        }

    }
}