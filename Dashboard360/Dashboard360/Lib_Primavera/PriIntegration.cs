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
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TipoDoc, TotalMerc, TotalDesc, TotalOutros, Serie From CabecDoc");
                while (!objListCab.NoFim())
                {
                    if (objListCab.Valor("Entidade").Equals(cliente))
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
                    }

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

        public static List<Model.FornecedorProductCounter> ListTopProducts()
        {
            StdBELista objList;
            List<Model.FornecedorProductCounter> listTopProducts = new List<Model.FornecedorProductCounter>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Artigo, Descricao, UnidadeEntrada FROM ARTIGO"); //ORDER BY UnidadeEntrada DESC


                while (!objList.NoFim())
                {
                    listTopProducts.Add(new Model.FornecedorProductCounter
                    {
                        CodArtigo = objList.Valor("Artigo"),
                        DescArtigo = objList.Valor("Descricao"),
                        UnidadeEntrada = objList.Valor("UnidadeEntrada"),
                    });
                    objList.Seguinte();

                }

                return listTopProducts;
            }
            else
                return null;
        }


        public static List<Model.Inventario> InventaryValue()
        {
            StdBELista objList;
            List<Model.Inventario> InventaryValue = new List<Model.Inventario>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT SUM(PCPadrao) AS Soma FROM  ARTIGO");

                InventaryValue.Add(new Model.Inventario
                {
                    Valor = objList.Valor("Soma"),
                });


                return InventaryValue;
            }
            else
                return null;
        }

        #endregion Supply


        # region HumanResources

        public static List<Model.Empregado> ListOfEmployees()
        {
            StdBELista objList;
            List<Model.Empregado> listOfEmployees = new List<Model.Empregado>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT COUNT(*) AS Numero FROM  FUNCIONARIOS");


                listOfEmployees.Add(new Model.Empregado
                {
                    numeroEmpregados = objList.Valor("Numero"),

                });



                return listOfEmployees;
            }
            else
                return null;
        }


        public static List<Model.Empregado> ListOfFemaleEmployees()
        {
            StdBELista objList;
            List<Model.Empregado> listOfFemaleEmployees = new List<Model.Empregado>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT COUNT(Sexo) AS Numero FROM  FUNCIONARIOS WHERE Sexo=1");


                listOfFemaleEmployees.Add(new Model.Empregado
                {
                    numeroRaparigasEmpregadas = objList.Valor("Numero"),

                });



                return listOfFemaleEmployees;
            }
            else
                return null;
        }

        public static List<Model.Empregado> ListOfAgeOfEmployees()
        {
            StdBELista objList;
            List<Model.Empregado> listOfAgeOfEmployees = new List<Model.Empregado>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT DataNascimento FROM FUNCIONARIOS");
                while (!objList.NoFim())
                {
                    listOfAgeOfEmployees.Add(new Model.Empregado
                    {
                        dataNascimento = objList.Valor("DataNascimento"),

                    });
                    objList.Seguinte();
                }

                return listOfAgeOfEmployees;

                // objList = PriEngine.Engine.Consulta("SELECT AVG (SELECT )")
            }
            else
                return null;
        }


        // Get Salarios
        public static List<Model.Empregado> ListOfEmployeeSalary()
        {
            StdBELista objList;
            List<Model.Empregado> listofEmployeeSalary = new List<Model.Empregado>();
            if (PriEngine.InitializeCompany(Dashboard360.Properties.Settings.Default.Company.Trim(), Dashboard360.Properties.Settings.Default.User.Trim(), Dashboard360.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT VencimentoMensal FROM FUNCIONARIOS");
                while (!objList.NoFim())
                {
                    listofEmployeeSalary.Add(new Model.Empregado
                    {
                        salario = objList.Valor("VencimentoMensal"),

                    });
                    objList.Seguinte();
                }

                return listofEmployeeSalary;

                // objList = PriEngine.Engine.Consulta("SELECT AVG (SELECT )")
            }
            else
                return null;
        }




        #endregion HumanResources

    }
}

