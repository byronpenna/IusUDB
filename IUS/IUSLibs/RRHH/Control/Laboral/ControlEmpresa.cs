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
    // --------
        using IUSLibs.RRHH.Entidades.Laboral;
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlEmpresa:PadreLib
    {
        #region "funciones"
            #region "do"
            #endregion
            #region "get"
                public List<Empresa> sp_rrhh_getEmpresas(int idUsuarioEjecutor, int idPagina)
                {
                    List<Empresa> empresas = null; Empresa empresa;
                    SPIUS sp = new SPIUS("sp_rrhh_getEmpresas");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            empresas = new List<Empresa>();
                            if (tb[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    empresa = new Empresa((int)row["idEmpresa"], row["nombre"].ToString(), row["direccion"].ToString(), (int)row["id_rubro_fk"]);
                                    empresa._rubro._rubro = row["rubro"].ToString();
                                    empresas.Add(empresa);
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
                    return empresas;
                }
            #endregion
        #endregion
    }
}
