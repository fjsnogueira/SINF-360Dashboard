﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class LinhaDocVenda
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

        public string IdCabecDoc
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public double Quantidade
        {
            get;
            set;
        }

        public string Unidade
        {
            get;
            set;
        }

        public double Desconto
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public double TotalILiquido
        {
            get;
            set;
        }

        public double TotalLiquido
        {
            get;
            set;
        }


    }
}