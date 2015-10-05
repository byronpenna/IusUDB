using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// internas 
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    //----------
        using IUSLibs.RRHH.Entidades;
namespace IUSLibs.RRHH.Control
{
    public class ControlEstadoCivil:PadreLib
    {
        #region "funciones"
            #region "do"
            #endregion 
            #region "get"
                public List<EstadoCivil> sp_rrhh_getEstadosCiviles()
                {
                    List<EstadoCivil> estadosCiviles = null; EstadoCivil estadoCivil;
                    SPIUS sp = new SPIUS("sp_rrhh_getEstadosCiviles");
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                estadosCiviles = new List<EstadoCivil>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    estadoCivil = new EstadoCivil((int)row["idEstadoCivil"], row["estado_civil"].ToString());
                                    estadosCiviles.Add(estadoCivil);
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
                    catch(Exception x)
                    {
                        throw x;
                    }
                    return estadosCiviles;
                }
            #endregion
        #endregion
    }
}
