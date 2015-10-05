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
    //---------
        using IUSLibs.RRHH.Entidades.Laboral;
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlRubroEmpresa:PadreLib
    {
        #region "funciones"
            #region "do"

            #endregion
            #region "get"
                public List<RubroEmpresa> sp_rrhh_getRubrosEmpresas()
                {
                    List<RubroEmpresa> rubros = null; RubroEmpresa rubro;
                    SPIUS sp = new SPIUS("sp_rrhh_getRubrosEmpresas");
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                rubros = new List<RubroEmpresa>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    rubro = new RubroEmpresa((int)row["idRubro"], row["rubro"].ToString());
                                    rubros.Add(rubro);
                                }
                            }
                        }
                        return rubros;
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
            #endregion
        #endregion
    }
}
