using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using ADODB;

namespace Dashboard360.Lib_Primavera
{
    public class PriIntegration
    {


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            StdBELista objList;
            List<Model.Cliente> listClientes = new List<Model.Cliente>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo, DataCriacao FROM CLIENTES");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo"),
                        DataCriacao = objList.Valor("DataCriacao")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {


            GcpBECliente objCli = new GcpBECliente();

            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        // Top Clients for a certain product
        public static List<Model.DocVenda> GetTopClientesArtigo(string id, int year)
        {
            {
                StdBELista objListCab;
                StdBELista objListLin;
                Model.DocVenda dv = new Model.DocVenda();
                List<Model.DocVenda> listdv = new List<Model.DocVenda>();
                Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
                List<Model.LinhaDocVenda> listlindv = new
                List<Model.LinhaDocVenda>();

                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {
                    objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade,Nome, Data, NumDoc, TotalMerc, TipoDoc, Serie From CabecDoc ");
                    while (!objListCab.NoFim())
                    {
                        dv = new Model.DocVenda();
                        dv.id = objListCab.Valor("id");
                        dv.Entidade = objListCab.Valor("Entidade");
                        dv.NumDoc = objListCab.Valor("NumDoc");
                        dv.Data = objListCab.Valor("Data");
                        dv.TotalMerc = objListCab.Valor("TotalMerc");
                        dv.TipoDoc = objListCab.Valor("TipoDoc");
                        dv.Serie = objListCab.Valor("Serie");
                        dv.Nome = objListCab.Valor("Nome");
                        objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' and Artigo = '" + id + "' order By NumLinha");
                        listlindv = new List<Model.LinhaDocVenda>();

                        while (!objListLin.NoFim())
                        {
                            lindv = new Model.LinhaDocVenda();
                            lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                            lindv.CodArtigo = objListLin.Valor("Artigo");
                            lindv.DescArtigo = objListLin.Valor("Descricao");
                            lindv.Quantidade = objListLin.Valor("Quantidade");
                            lindv.Unidade = objListLin.Valor("Unidade");
                            lindv.Desconto = objListLin.Valor("Desconto1");
                            lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                            lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                            lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                            listlindv.Add(lindv);
                            objListLin.Seguinte();
                        }

                        dv.LinhasDoc = listlindv;
                        listdv.Add(dv);
                        objListCab.Seguinte();
                    }

                    return listdv;
                }
                else return null;
            }
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }



        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }


        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Venda

        public static List<Model.DocVenda> GetVendasCliente(string cliente)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, TipoDoc, Serie From CabecDoc where Entidade = '" + cliente + "'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static List<Model.DocVenda> GetVendasProduto(string ano)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, TipoDoc, Serie From CabecDoc");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;

        }



        #endregion Venda;


        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {

            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();
                    myArt.Stock = objArtigo.get_StkActual();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }


        public static double GetStockArtigo(string codArtigo)
        {
            StdBELista stockArtigo;
            double stock = 0;
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                stockArtigo = PriEngine.Engine.Consulta("select STKActual from Artigo where Artigo ='" + codArtigo + "'");
                while (!stockArtigo.NoFim())
                {
                    stock = stockArtigo.Valor("STKActual");
                }
            }
            return stock;
        }



        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo


        #region DocCompra


        public static List<Model.DocCompra> ListaCompras(string ano)
        {

            StdBELista objListCab;
            Model.DocCompra compra = new Model.DocCompra();
            List<Model.DocCompra> listaCompras = new List<Model.DocCompra>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, DataDoc, NumDoc, TipoDoc, TotalMerc, TotalDesc, TotalOutros, Serie From CabecCompras WHERE year(DataDoc)='" + ano + "'");
                while (!objListCab.NoFim())
                {
                    compra = new Model.DocCompra();
                    compra.id = objListCab.Valor("id");
                    compra.Entidade = objListCab.Valor("Entidade");
                    compra.NumDoc = objListCab.Valor("NumDoc");
                    compra.Data = objListCab.Valor("DataDoc");
                    compra.TotalMerc = objListCab.Valor("TotalMerc");
                    compra.TotalDesc = objListCab.Valor("TotalDesc");
                    compra.TotalOutros = objListCab.Valor("TotalOutros");
                    compra.Serie = objListCab.Valor("Serie");
                    compra.TipoDoc = objListCab.Valor("TipoDoc");
                    listaCompras.Add(compra);
                    objListCab.Seguinte();
                }
            }
            return listaCompras;
        }
        /**
         * */
        public static List<Model.DocCompra> ListaComprasNew(string ano)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new
            List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, DataDoc, NumDoc, TipoDoc, TotalMerc,TotalOutros, TotalDesc,  Serie From CabecCompras where year(DataDoc)='" + ano + "'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.TipoDoc = objListCab.Valor("TipoDoc");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.TotalOutros = objListCab.Valor("TotalOutros");
                    dc.TotalDesc = objListCab.Valor("TotalDesc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT IdCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("IdCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;
                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }

        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }


        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra


        #region DocsVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        // Returns a list of all sales of a certain product
        public static List<Model.DocVenda> ListaVendasArtigo(string id)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, TotalDesc, TotalOutros, TipoDoc, Serie From CabecDoc");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.TotalDesc = objListCab.Valor("TotalDesc");
                    dv.TotalOutros = objListCab.Valor("TotalOutros");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' AND Artigo ='" + id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }




        // Returns a list of all sales
        public static List<Model.DocVenda> ListaVendas(string ano)
        {
            StdBELista objListCab;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TipoDoc, TotalMerc, TotalDesc, TotalOutros, Serie From CabecDoc WHERE year(Data)=" + ano);
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.TotalDesc = objListCab.Valor("TotalDesc");
                    dv.TotalOutros = objListCab.Valor("TotalOutros");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }

        // Returns a list of all sales
        public static List<Model.DocVenda> ListaVendasCliente(string cliente)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, TipoDoc, NumDoc, TotalMerc, TotalOutros, TotalDesc, Serie From CabecDoc where Entidade='" + cliente + "'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.TotalOutros = objListCab.Valor("TotalOutros");
                    dv.TotalDesc = objListCab.Valor("TotalDesc");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }




        public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocsVenda


        #region Pendentes
        public static List<Model.Pendente> GetPending(bool receivable)
        {
            StdBELista objListCab;
            List<Model.Pendente> result = new List<Model.Pendente>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta(
                    "SELECT ValorPendente, Moeda " +
                    "FROM Pendentes " +
                    "WHERE TipoEntidade = " + (receivable ? "'C'" : "'F'"));
                while (!objListCab.NoFim())
                {
                    Model.Pendente p = new Model.Pendente();
                    p.PendingValue = objListCab.Valor("ValorPendente");
                    p.PendingCurrency = objListCab.Valor("Moeda");
                    result.Add(p);
                    objListCab.Seguinte();
                }
            }
            return result;
        }
        #endregion


        # region Supply

        public static List<Model.LinhaDocCompra> ListTopProducts(string ano)
        {

            StdBELista objList;

            List<Model.LinhaDocCompra> ListaVendas = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT IdCabecCompras, DataDoc, TipoDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TaxaIva, PrecoLiquido from LinhasCompras WHERE year(DataDoc)=" + ano);
                while (!objList.NoFim())
                {
                    ListaVendas.Add(new Model.LinhaDocCompra
                    {
                        IdCabecDoc = objList.Valor("IdCabecCompras"),
                        CodArtigo = objList.Valor("Artigo"),
                        DescArtigo = objList.Valor("Descricao"),
                        Quantidade = objList.Valor("Quantidade"),
                        Unidade = objList.Valor("Unidade"),
                        Desconto = objList.Valor("Desconto1"),
                        PrecoUnitario = objList.Valor("PrecUnit"),
                        TotalLiquido = objList.Valor("PrecoLiquido"),
                        TipoDoc = objList.Valor("TipoDoc"),
                        Data = objList.Valor("DataDoc")

                    });


                    objList.Seguinte();
                }




                return ListaVendas;
            }
            else
                return null;
        }


        public static List<Model.DocCompra> InventaryValue(string year)
        {
            StdBELista objList;
            List<Model.DocCompra> InventaryValue = new List<Model.DocCompra>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Artigo, STKActual, PCMedio, DataUltEntrada FROM ARTIGO");


                while (!objList.NoFim())
                {
                    DateTime inicioAno = new DateTime(Convert.ToInt32(year), 01, 01);
                    DateTime fimAno = new DateTime(Convert.ToInt32(year), 12, 31);

                    if (objList.Valor("DataUltEntrada").Ticks >= inicioAno.Ticks && objList.Valor("DataUltEntrada").Ticks <= fimAno.Ticks)
                    {
                        InventaryValue.Add(new Model.DocCompra
                        {
                            Nome = objList.Valor("Artigo"),
                            stockAtual = objList.Valor("STKActual"),
                            PrecoMedio = objList.Valor("PCMedio")
                        });
                    }
                    objList.Seguinte();
                }

                return InventaryValue;
            }
            else
                return null;
        }



        public static List<Model.DocCompra> GetInventarioArtigo(string artigo)
        {
            StdBELista objList;
            List<Model.DocCompra> InventaryValue = new List<Model.DocCompra>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Artigo, STKActual FROM ARTIGO where Artigo='" + artigo + "'");


                while (!objList.NoFim())
                {
                    InventaryValue.Add(new Model.DocCompra
                    {
                        Nome = objList.Valor("Artigo"),
                        stockAtual = objList.Valor("STKActual"),
                        PrecoMedio = objList.Valor("PCMedio")
                    });

                    objList.Seguinte();
                }

                return InventaryValue;
            }
            else
                return null;
        }

        public static List<Model.DocCompra> TotalPurchasedValue(string year)
        {
            StdBELista objList;
            List<Model.DocCompra> purchasedlist = new List<Model.DocCompra>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Nome, TipoDoc, TotalMerc, TotalDesc, TotalOutros, TotalIva, DataIntroducao FROM  CabecCompras");

                while (!objList.NoFim())
                {
                    DateTime inicioAno = new DateTime(Convert.ToInt32(year), 01, 01);
                    DateTime fimAno = new DateTime(Convert.ToInt32(year), 12, 31);

                    if (objList.Valor("DataIntroducao").Ticks >= inicioAno.Ticks && objList.Valor("DataIntroducao").Ticks <= fimAno.Ticks)
                    {
                        purchasedlist.Add(new Model.DocCompra
                        {
                            Nome = objList.Valor("Nome"),
                            TotalMerc = objList.Valor("TotalMerc"),
                            TotalDesc = objList.Valor("TotalDesc"),
                            TotalOutros = objList.Valor("TotalOutros"),
                            TotalIva = objList.Valor("TotalIva"),
                            TipoDoc = objList.Valor("TipoDoc")
                        });

                    }

                    objList.Seguinte();
                }


                return purchasedlist;
            }
            else
                return null;
        }


        #endregion Suppl


        # region HumanResources

        # region HumanResources

        public static List<Model.Empregado> ListOfEmployees()
        {
            StdBELista objList;
            List<Model.Empregado> listOfEmployees = new List<Model.Empregado>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Nome, Sexo, DataNascimento, VencimentoMensal, DataDemissao, DataAdmissao, ValorSubsAlim FROM FUNCIONARIOS");


                while (!objList.NoFim())
                {

                    if (!(objList.Valor("DataDemissao").GetType() == typeof(DateTime)))
                    {
                        listOfEmployees.Add(new Model.Empregado
                        {
                            nome = objList.Valor("Nome"),
                            sexo = objList.Valor("Sexo"),
                            dataNascimento = objList.Valor("DataNascimento"),
                            salario = objList.Valor("VencimentoMensal"),
                            dataAdmissao = objList.Valor("DataAdmissao"),
                            subsidioAlim = System.Convert.ToDouble(objList.Valor("ValorSubsAlim"))
                        });

                    }
                    else
                    {

                        listOfEmployees.Add(new Model.Empregado
                        {
                            nome = objList.Valor("Nome"),
                            sexo = objList.Valor("Sexo"),
                            dataNascimento = objList.Valor("DataNascimento"),
                            salario = objList.Valor("VencimentoMensal"),
                            dataAdmissao = objList.Valor("DataAdmissao"),
                            dataDemissao = objList.Valor("DataDemissao"),
                            subsidioAlim = System.Convert.ToDouble(objList.Valor("ValorSubsAlim"))
                        });

                    }

                    objList.Seguinte();
                }



                return listOfEmployees;
            }
            else
                return null;
        }

        #endregion HumanResources



        #endregion HumanResources

        #region Financial
        public static List<Model.Pendente> GetPending(string year, bool receivable)
        {
            StdBELista objListCab;
            List<Model.Pendente> result = new List<Model.Pendente>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta(
                    "SELECT ValorPendente, Moeda " +
                    "FROM Pendentes " +
                    "WHERE TipoEntidade =" + (receivable ? " 'C' " : " 'F' ") +
                    "AND YEAR(DataDoc) = " + year);
                while (!objListCab.NoFim())
                {
                    Model.Pendente p = new Model.Pendente();
                    p.PendingValue = objListCab.Valor("ValorPendente");
                    p.PendingCurrency = objListCab.Valor("Moeda");
                    result.Add(p);
                    objListCab.Seguinte();
                }
            }
            return result;
        }

        public static List<Model.AcumuladosFluxos> GetFlow(string year)
        {
            StdBELista objListCab;
            List<Model.AcumuladosFluxos> result = new List<Model.AcumuladosFluxos>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta(
                    "SELECT * " +
                    "FROM AcumuladosFluxos " +
                    "WHERE Fluxo = 11 " +
                    "AND Ano = " + year);
                while (!objListCab.NoFim())
                {
                    Model.AcumuladosFluxos p = new Model.AcumuladosFluxos();
                    p.Fluxo = objListCab.Valor("Fluxo");
                    p.Moeda = objListCab.Valor("Moeda");
                    p.Mes00 = objListCab.Valor("Mes00");
                    p.Mes01 = objListCab.Valor("Mes01");
                    p.Mes02 = objListCab.Valor("Mes02");
                    p.Mes03 = objListCab.Valor("Mes03");
                    p.Mes04 = objListCab.Valor("Mes04");
                    p.Mes05 = objListCab.Valor("Mes05");
                    p.Mes06 = objListCab.Valor("Mes06");
                    p.Mes07 = objListCab.Valor("Mes07");
                    p.Mes08 = objListCab.Valor("Mes08");
                    p.Mes09 = objListCab.Valor("Mes09");
                    p.Mes10 = objListCab.Valor("Mes10");
                    p.Mes11 = objListCab.Valor("Mes11");
                    p.Mes12 = objListCab.Valor("Mes12");
                    p.Mes13 = objListCab.Valor("Mes13");
                    p.Mes14 = objListCab.Valor("Mes14");
                    p.Mes15 = objListCab.Valor("Mes15");
                    result.Add(p);
                    objListCab.Seguinte();
                }
            }
            return result;
        }

        public static List<Model.AcumuladosContas> GetBalance(string year)
        {
            StdBELista objListCab;
            List<Model.AcumuladosContas> result = new List<Model.AcumuladosContas>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta(
                    "SELECT * " +
                    "FROM AcumuladosContas " +
                    "WHERE LEN(Conta) < 3 " +
                    "AND Ano = " + year);
                while (!objListCab.NoFim())
                {
                    Model.AcumuladosContas p = new Model.AcumuladosContas();
                    p.Conta = objListCab.Valor("Conta");
                    p.Mes00CR = objListCab.Valor("Mes00CR");
                    p.Mes00DB = objListCab.Valor("Mes00DB");
                    p.Mes01CR = objListCab.Valor("Mes01CR");
                    p.Mes01DB = objListCab.Valor("Mes01DB");
                    p.Mes02CR = objListCab.Valor("Mes02CR");
                    p.Mes02DB = objListCab.Valor("Mes02DB");
                    p.Mes03CR = objListCab.Valor("Mes03CR");
                    p.Mes03DB = objListCab.Valor("Mes03DB");
                    p.Mes04CR = objListCab.Valor("Mes04CR");
                    p.Mes04DB = objListCab.Valor("Mes04DB");
                    p.Mes05CR = objListCab.Valor("Mes05CR");
                    p.Mes05DB = objListCab.Valor("Mes05DB");
                    p.Mes06CR = objListCab.Valor("Mes06CR");
                    p.Mes06DB = objListCab.Valor("Mes06DB");
                    p.Mes07CR = objListCab.Valor("Mes07CR");
                    p.Mes07DB = objListCab.Valor("Mes07DB");
                    p.Mes08CR = objListCab.Valor("Mes08CR");
                    p.Mes08DB = objListCab.Valor("Mes08DB");
                    p.Mes09CR = objListCab.Valor("Mes09CR");
                    p.Mes09DB = objListCab.Valor("Mes09DB");
                    p.Mes10CR = objListCab.Valor("Mes10CR");
                    p.Mes10DB = objListCab.Valor("Mes10DB");
                    p.Mes11CR = objListCab.Valor("Mes11CR");
                    p.Mes11DB = objListCab.Valor("Mes11DB");
                    p.Mes12CR = objListCab.Valor("Mes12CR");
                    p.Mes12DB = objListCab.Valor("Mes12DB");
                    p.Mes13CR = objListCab.Valor("Mes13CR");
                    p.Mes13DB = objListCab.Valor("Mes13DB");
                    p.Mes14CR = objListCab.Valor("Mes14CR");
                    p.Mes14DB = objListCab.Valor("Mes14DB");
                    p.Mes15CR = objListCab.Valor("Mes15CR");
                    p.Mes15DB = objListCab.Valor("Mes15DB");
                    p.Moeda = objListCab.Valor("Moeda");
                    result.Add(p);
                    objListCab.Seguinte();
                }
            }
            return result;
        }
        #endregion


    }
}

