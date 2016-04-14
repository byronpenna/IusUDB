using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// 
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
namespace IUSLibs.ADMINFE.Pantalla
{
    public class PantallaHome:PadreLib
    {
        public DataRowCollection sp_adminfe_front_pantallaHome(int n, string ip, int idPagina)
        {
            SPIUS sp = new SPIUS("sp_adminfe_front_pantallaHome");
            DataRowCollection rows = null;
            sp.agregarParametro("n", n);
            sp.agregarParametro("ip", ip);
            sp.agregarParametro("idPagina", idPagina);
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrectoGet(tb))
                {
                    return tb[0].Rows;
                }
                return rows;
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
    }
}
