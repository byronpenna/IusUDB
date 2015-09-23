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
    //-------------------
        using IUSLibs.RRHH.Entidades.Laboral;   
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlCargos:PadreLib
    {
        #region "funciones"
            #region "do"
                
            #endregion
            #region "get"
                public List<CargoEmpresa> sp_rrhh_getCargos(int idUsuarioEjecutor,int idPagina)
                {
                    List<CargoEmpresa> cargosEmpresas = null; CargoEmpresa cargoEmpresa = null;
                    try
                    {
                        SPIUS sp = new SPIUS("sp_rrhh_getCargos");
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                cargosEmpresas = new List<CargoEmpresa>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    cargoEmpresa = new CargoEmpresa((int)row["idCargoEmpresa"],row["cargo"].ToString());

                                    cargosEmpresas.Add(cargoEmpresa);
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
                    return cargosEmpresas;
                }
            #endregion
        #endregion
    }
}
