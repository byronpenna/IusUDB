using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;    
namespace IUSLibs.REPO.Control
{
    public class ControlTipoArchivo:PadreLib
    {
        #region "funciones"
            #region "get"
                public List<TipoArchivo> sp_repo_getTipoArchivo(string lang,int idUsuarioEjecutor,int idPagina)
                {
                    /*
                        @				varchar(10),
	                    -- segurdad 
	                    @	int,
	                    @			int
                     */
                    List<TipoArchivo> tiposArchivos = null; TipoArchivo tipoArchivo;
                    SPIUS sp = new SPIUS("sp_repo_getTipoArchivo");
                    sp.agregarParametro("lang", lang);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                tiposArchivos = new List<TipoArchivo>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"],row["tipoArchivo"].ToString(),row["icono"].ToString());
                                    tiposArchivos.Add(tipoArchivo);
                                    
                                }
                            }
                        }
                        return tiposArchivos;
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    
                }
                public List<TipoArchivo> sp_repo_front_getTiposArchivos(string lang,string ip, int idPagina)
                {
                    List<TipoArchivo> tiposArchivos = null; TipoArchivo tipoArchivo;
                    SPIUS sp = new SPIUS("sp_repo_front_getTiposArchivos");
                    sp.agregarParametro("lang", lang);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                tiposArchivos = new List<TipoArchivo>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["traduccion"].ToString(), row["icono"].ToString());
                                    tiposArchivos.Add(tipoArchivo);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
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
                    return tiposArchivos;
                }
            #endregion
            #region "set"
                
            #endregion
        #endregion
    }
}
