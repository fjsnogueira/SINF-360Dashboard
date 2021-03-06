﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard360.Lib_Primavera.Model
{
    public class DocVenda
    {
        public string Nome
        {
            get;
            set;
        }

        public string id
        {
            get;
            set;
        }

        public string TipoDoc
        {
            get;
            set;
        }

        public string Entidade
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public double TotalDesc
        {
            get;
            set;
        }


        public double TotalOutros
        {
            get;
            set;
        }


        public string Serie
        {
            get;
            set;
        }

        public List<Model.LinhaDocVenda> LinhasDoc
        {
            get;
            set;
        }


    }
}