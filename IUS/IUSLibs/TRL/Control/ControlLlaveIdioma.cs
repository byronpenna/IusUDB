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
namespace IUSLibs.TRL.Control
{
    public class ControlLlaveIdioma
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
                sp.agregarParametro("idPagina", this.pagina.idPagina);
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
                    llav.idLlave    = Convert.ToInt32(row["idPagina"].ToString());
                    llav.llave      = row["llave"].ToString();   
                    // generando idioma 
                    pag.idPagina    = Convert.ToInt32(row["idPagina"].ToString());
                    // objeto final 
                    llavIdioma.llave        = llav;
                    llavIdioma.idioma       = idioma;
                    llavIdioma.traduccion   = row["traduccion"].ToString();
                    traduccion.Add(llavIdioma);
                }
                return traduccion;
            }
        #endregion 
        #region "Constructores"
            // mandando unicamente los identificadores
            public ControlLlaveIdioma(int pPagina,string pIdioma) // la p viene de "parametro"
            {
                Pagina pag = new Pagina();
                pag.idPagina = pPagina;
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
