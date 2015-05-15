using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas 
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlPost:PadreLib
    {
        #region "acciones"
            
        #endregion
        #region "get"
            public Post sp_adminfe_noticias_publicarPost(Post postAgregar,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_publicarPost");
                Post postRegresar = null;
                sp.agregarParametro("titulo", postAgregar._titulo);
                sp.agregarParametro("contenido", postAgregar._contenido);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            postAgregar = new Post(1);
                            postRegresar = new Post((int)row["idPost"]);

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
                return postRegresar;
            }
        #endregion
    }
}
