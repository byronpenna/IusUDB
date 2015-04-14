using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// internas
    using IUSLibs.LOGS;
    using IUSLibs.BaseDatos;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.GENERALS;
namespace IUSLibs.TRL.Control
{
    public class ControlPagina:PadreLib
    {
        #region "constructores"
            public ControlPagina()
            {

            }
        #endregion
        #region "propiedades"

        #endregion
        #region "funciones publicas"
            public List<Pagina> sp_trl_getAllPaginas(int idUsuarioEjecutor, int idPagina)
            {
                List<Pagina> paginas = null;
                Pagina pagina;
                SPIUS sp = new SPIUS("sp_trl_getAllPaginas");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            paginas = new List<Pagina>();
                            foreach(DataRow row in ds.Tables[0].Rows){
                                pagina = new Pagina((int)row["idPagina"],row["pagina"].ToString(),(bool)row["estado"]);
                                paginas.Add(pagina);
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
                return paginas;
            }
        #endregion
    }
}
