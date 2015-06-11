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
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlCategoriaPost:PadreLib
    {
        #region "get"
            
        #endregion
        #region "acciones"
            public CategoriaPost sp_adminfe_noticias_insertCategoriasPosts(int idPost,int idCategoria,int idUsuarioEjecutor, int idPagina)
            {
                bool estado = false;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_insertCategoriasPosts");
                sp.agregarParametro("idCategoria", idCategoria);
                sp.agregarParametro("idPost", idPost);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                CategoriaPost categoriaPost = null;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        DataRow row = tb[1].Rows[0];
                        Post post = new Post((int)row["id_post_fk"]);
                        PostCategoria categoria = new PostCategoria((int)row["id_categoria_fk"]);
                        categoriaPost = new CategoriaPost((int)row["idCategoriaPost"], post, categoria);
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
                if(categoriaPost == null){
                    throw new Exception("No se pudo obtener objeto");
                }
                return categoriaPost;
            }
        #endregion
    }
}
