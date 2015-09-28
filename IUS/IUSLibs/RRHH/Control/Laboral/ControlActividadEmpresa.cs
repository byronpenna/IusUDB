using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias externas
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    // -------------------
        using IUSLibs.RRHH.Entidades.Laboral;   
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlActividadEmpresa:PadreLib
    {
        #region "funciones"
            #region "do"
                public ActividadEmpresa sp_rrhh_insertActividadEmpresa(ActividadEmpresa actividadAgregar,int idUsuarioEjecutor, int idPagina)
                {
                    ActividadEmpresa actividadIngresada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_insertActividadEmpresa");
                    sp.agregarParametro("actividad", actividadAgregar._actividad);
                    sp.agregarParametro("idLaboralPersona", actividadAgregar._laboralPersona._idLaboralPersona);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);

                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            actividadIngresada = new ActividadEmpresa((int)row["idActividadesEmpresa"], (int)row["id_laboralpersona_fk"], row["actividad"].ToString());

                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
                    }
                    return actividadIngresada;
                }
            #endregion
            #region "get"
                public List<ActividadEmpresa> sp_rrhh_getActividadesEmpresa(int idLaboralPersona,int idUsuarioEjecutor, int idPagina)
                {
                    List<ActividadEmpresa> actividadesEmpresa = null; ActividadEmpresa actividadEmpresa;
                    SPIUS sp = new SPIUS("sp_rrhh_getActividadesEmpresa");
                    sp.agregarParametro("idLaboralPersona", idLaboralPersona);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            actividadesEmpresa = new List<ActividadEmpresa>();
                            foreach (DataRow row in tb[0].Rows)
                            {
                                actividadEmpresa = new ActividadEmpresa((int)row["idActividadesEmpresa"],(int)row["id_laboralpersona_fk"],row["actividad"].ToString());
                                actividadesEmpresa.Add(actividadEmpresa);
                            }
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
                    }
                    return actividadesEmpresa;
                }
                
            #endregion
        #endregion
    }
}
