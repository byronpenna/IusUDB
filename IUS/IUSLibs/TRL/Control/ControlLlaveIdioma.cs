using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos 
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.LOGS;
    using IUSLibs.GENERALS;
namespace IUSLibs.TRL.Control
{
    public class ControlLlaveIdioma:PadreLib
    {
        #region "Propiedades"
            public Pagina pagina;
            public Idioma idioma;
        #endregion
        #region "Funciones"
            public List<LlaveIdioma> getLlavesSitio()
            {   
                List<LlaveIdioma> traduccion = new List<LlaveIdioma>();
                LlaveIdioma llavIdioma;
                Llave llav;
                Pagina pag;
                DataSet ds;
                SPIUS   sp = new SPIUS("sp_trl_getLlavesTraducidasPaginas");
                sp.agregarParametro("idPagina", this.pagina._idPagina);
                sp.agregarParametro("idioma", this.idioma._lang);
                try
                {
                    ds = sp.EjecutarProcedimiento();
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw new Exception("Error no controlado");
                }
                DataTable tb = ds.Tables[0];
                foreach (DataRow row in tb.Rows)
                {
                    llavIdioma  = new LlaveIdioma();
                    llav        = new Llave();
                    pag         = new Pagina();
                    // generando llave
                    llav._idLlave    = Convert.ToInt32(row["idPagina"].ToString());
                    llav._llave      = row["llave"].ToString();   
                    // generando idioma 
                    pag._idPagina    = Convert.ToInt32(row["idPagina"].ToString());
                    // objeto final 
                    llavIdioma._llave        = llav;
                    llavIdioma._idioma       = idioma;
                    llavIdioma._traduccion   = row["traduccion"].ToString();
                    traduccion.Add(llavIdioma);
                }
                return traduccion;
            }
            // nombres estandarizados
            public List<LlaveIdioma> sp_trl_tablitaGestionTraduccion(int idUsuarioEjecutor,int idPagina)
            {
                List<LlaveIdioma> llavesIdiomas = null;
                LlaveIdioma llaveIdioma; Idioma idioma; Pagina pagina; Llave llave; // clases que van dentro de llaves idiomas
                SPIUS sp = new SPIUS("sp_trl_tablitaGestionTraduccion");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPermiso", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            llavesIdiomas = new List<LlaveIdioma>();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                pagina = new Pagina((int)row["idPagina"], row["pagina"].ToString(), true);
                                idioma = new Idioma((int)row["idIdioma"],row["idioma"].ToString());
                                llave = new Llave((int)row["idLlave"], row["llave"].ToString(),pagina);
                                llaveIdioma = new LlaveIdioma((int)row["idLlaveIdioma"],idioma,llave,row["traduccion"].ToString());
                                llavesIdiomas.Add(llaveIdioma);
                            }
                        }
                    }
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return llavesIdiomas;
            }
            public bool sp_trl_actualizarLlaveIdioma(int idLlaveIdioma,int idLlave,int idIdioma,string traduccion,int idUsuarioEjecutor, int idPagina)
            {
                bool toReturn = false;
                ErroresIUS errorIUS;
                SPIUS sp = new SPIUS("sp_trl_actualizarLlaveIdioma");
                sp.agregarParametro("idLlaveIdioma",idLlaveIdioma);
                sp.agregarParametro("idLlave", idLlave);
                sp.agregarParametro("idIdioma", idIdioma);
                sp.agregarParametro("traduccion", traduccion);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            
                            DataRow row = ds.Tables[0].Rows[0];
                            if ((int)row["estadoUpdate"] == 1)
                            {
                                toReturn = true;
                            }
                            else
                            {
                                row = ds.Tables[1].Rows[0];
                                errorIUS = new ErroresIUS("", ErroresIUS.tipoError.sql,(int)row["errorNumber"]);
                                throw errorIUS;
                            }
                        }
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return toReturn;
            }
            public bool sp_trl_eliminarLlaveIdioma(int idLlaveIdioma, int idUsuario, int idPagina)
            {
                bool estado = false;
                SPIUS sp = new SPIUS("sp_trl_eliminarLlaveIdioma");
                sp.agregarParametro("idLlaveIdioma", idLlaveIdioma);
                sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if ((int)ds.Tables[0].Rows[0]["estadoDelete"] == 1)
                            {
                                estado = true;
                            }
                        }
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return estado;
            }
        #endregion 
        #region "Constructores"
            // mandando unicamente los identificadores
            public ControlLlaveIdioma(int pPagina,string pIdioma) // la p viene de "parametro"
            {
                Pagina pag = new Pagina();
                pag._idPagina = pPagina;
                Idioma idi = new Idioma();
                idi._lang = pIdioma;
                this.pagina = pag;
                this.idioma = idi;
            }
            // mandando los objetos 
            public ControlLlaveIdioma(Pagina pPagina,Idioma pIdioma)
            {
                this.pagina = pPagina;
                this.idioma = pIdioma;
            }
            // no mandando nada
            public ControlLlaveIdioma()
            {

            }
        #endregion
    }
}
