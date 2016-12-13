using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class Artigo
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

        public double Stock
        {
            get;
            set;
        }

    }

    public class ArtigoCounter
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

        public double QuantidadeVendida
        {
            get;
            set;
        }

        public double VolumeVendas
        {
            get;
            set;
        }
    }
}