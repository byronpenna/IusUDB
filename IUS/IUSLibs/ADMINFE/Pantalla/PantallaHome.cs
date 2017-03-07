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
        public Dictionary<object,object> sp_adminfe_front_pantallaHome(int n,int pagina,string ip, int idPagina,int op = 1)
        {
            Dictionary<object, object> retorno = new Dictionary<object, object>();
            SPIUS sp = new SPIUS("sp_adminfe_front_pantallaHome");
            DataRowCollection rows = null;
            sp.agregarParametro("n", n);
            sp.agregarParametro("pagina", pagina);
            sp.agregarParametro("ip", ip);
            sp.agregarParametro("idPagina", idPagina);
            sp.agregarParametro("op", op);
            int cn = 0;
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrectoGet(tb))
                {
                    if (tb[0].Rows.Count > 0)
                    {
                        rows = tb[0].Rows;
                    }
                    if (tb[1].Rows.Count > 0)
                    {
                        DataRow row = tb[1].Rows[0];
                        cn = (int)row["cnNotiEvento"];
                    }
                    retorno.Add("notiEvento",rows);
                    retorno.Add("total", cn);
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
    }
}
