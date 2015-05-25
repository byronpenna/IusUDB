using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// internas
    using IUSLibs.LOGS;
    using IUSLibs.BaseDatos;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.GENERALS;
namespace IUSLibs.TRL.Control
{
    public class ControlIdioma:PadreLib
    {
        #region "constructores"
            public ControlIdioma()
            {

            }
        #endregion
        #region "propiedades"

        #endregion
        #region "funciones publicas"
            #region "backend"
                public List<Idioma> sp_trl_getAllIdiomas(int idUsuarioEjecutor,int idPagina)
            {
                List<Idioma> idiomas = null;
                Idioma idioma; // clase generica para la lista
                SPIUS sp = new SPIUS("sp_trl_getAllIdiomas");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if(!this.DataSetDontHaveTable(ds))
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        idiomas = new List<Idioma>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            idioma = new Idioma((int)row["idIdioma"],row["idioma"].ToString(),row["lang"].ToString(),row["charset"].ToString());
                            idiomas.Add(idioma);
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
                return idiomas;
            }
            #endregion
            #region "front end"
                public Idioma sp_trl_getIdiomaFromIds(int idIdioma)
                {
                    Idioma idioma = null;
                    SPIUS sp = new SPIUS("sp_trl_getIdiomaFromIds");
                    sp.agregarParametro("idIdioma",idIdioma);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb) && tb[1].Rows.Count > 0)
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            idioma = new Idioma((int)rowResultado["idIdioma"], rowResultado["idioma"].ToString(), rowResultado["lang"].ToString(), rowResultado["charset"].ToString());
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
                    return idioma;
                }
            #endregion
        #endregion
    }
}
