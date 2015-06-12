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
            public List<PostCategoria> sp_adminfe_noticias_getCategoriasPostById(int idPost,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_getCategoriasPostById");
                sp.agregarParametro("idPost",idPost);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                List<PostCategoria> postsCategorias = null;
                PostCategoria postCategoria;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            postsCategorias = new List<PostCategoria>();
                            foreach(DataRow row in tb[0].Rows){
                                postCategoria = new PostCategoria((int)row["idPostCategoria"],row["categoria"].ToString(),(bool)row["selected"]);
                                postsCategorias.Add(postCategoria);
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
                return postsCategorias;
            }
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
            public List<CategoriaPost> sp_adminfe_noticias_updateCategoriaPost(string categorias, int idPost, int idUsuarioEjecutor, int idPagina)
            {
                List<CategoriaPost> categoriasPosts = null;
                CategoriaPost categoriaPost;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_updateCategoriaPost");
                bool estado = false;
                sp.agregarParametro("categorias", categorias);
                sp.agregarParametro("idPost", idPost);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        estado = true;
                        if (tb[1].Rows.Count > 0)
                        {
                            categoriasPosts = new List<CategoriaPost>();
                            foreach(DataRow row in tb[1].Rows){

                                categoriaPost = new CategoriaPost((int)row["idCategoriaPost"], (int)row["id_post_fk"], (int)row["id_categoria_fk"]);
                                categoriasPosts.Add(categoriaPost);
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
                if(estado = false && categoriasPosts.Count <= 0){
                    throw new Exception("Error no controlado al querer actualizar categorias");
                }
                return categoriasPosts;
            }
        #endregion
    }
}
