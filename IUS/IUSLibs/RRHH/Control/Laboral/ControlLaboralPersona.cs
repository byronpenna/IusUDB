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
    //-------------------
        using IUSLibs.RRHH.Entidades.Laboral;
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlLaboralPersona:PadreLib
    {
        #region "funciones"
            #region "do"
                public Dictionary<object, object> sp_rrhh_getInfoInicialLaboralPersona(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    // variables 
                        List<Empresa> empresas = null; Empresa empresa;
                    // do 
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                        SPIUS sp = new SPIUS("sp_rrhh_getInfoInicialLaboralPersona");
                        sp.agregarParametro("idPersona", idPersona);
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    //foreach(DataRow row in tb[])
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
                        return retorno;
                    
                }
            #endregion
            #region "get"
            #endregion
        #endregion
    }
}
