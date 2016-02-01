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
    using IUSLibs.ADMINFE.Entidades;

namespace IUSLibs.ADMINFE.Control
{
    public class ControlDatosSalesianos:PadreLib
    {
        #region "get"
            public DatosSalesianos sp_adminfe_front_getDatosSalesianos(string ip, int idPagina)
            {
                try
                {
                    DatosSalesianos retorno = null;
                    SPIUS sp = new SPIUS("sp_adminfe_front_getDatosSalesianos");
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            retorno = new DatosSalesianos((int)row["salesianos_mundo"], (int)row["paises_presencia"], (int)row["provincias"], (int)row["grupos_familia"],row["website_salesiano"].ToString());
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                        }
                    }
                    return retorno;
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
    }
}
