using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    // --------------
        using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlEstadoCarrera:PadreLib
    {
        #region "funciones"
            #region "do"
                
            #endregion
            #region "get"
                public List<EstadoCarrera> sp_rrhh_getEstadosCarreras(int idUsuarioEjecutor, int idPagina)
                {
                    List<EstadoCarrera> estadosCarreras = null; EstadoCarrera estadoCarrera;
                    SPIUS sp = new SPIUS("sp_rrhh_getEstadosCarreras");

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                estadosCarreras = new List<EstadoCarrera>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    estadoCarrera = new EstadoCarrera((int)row["idEstadoCarrera"], row["estado"].ToString());
                                    estadosCarreras.Add(estadoCarrera);
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
                    return estadosCarreras;
                }
            #endregion
        #endregion
    }
}
