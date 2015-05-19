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
        #region "Get"
            public List<Post> sp_adminfe_noticias_getPosts(int idUsuarioEjecutor, int idPagina) { 
                List<Post> posts = null;
                Post post;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_getPosts");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    Usuario usuarioCreador;
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            posts = new List<Post>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                usuarioCreador = new Usuario((int)row["id_usuario_fk"],row["usuario"].ToString());
                                post = new Post((int)row["idPost"], (DateTime)row["fecha_creacion"], (DateTime)row["ultima_modificacion"], row["titulo"].ToString(), row["contenido"].ToString(), (bool)row["estado"], usuarioCreador);
                                posts.Add(post);
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
                return posts;
            }
            public Dictionary<object, object> sp_adminfe_noticias_getPostsFromId(int idPost,int idUsuarioEjecutor,int idPagina) {
                //vars
                Dictionary<object, object> retorno = null;
                Post post = new Post(); List<Tag> tags = null; List<PostCategoria> categorias = null;
                Usuario usu; DataRow rowResult; Tag tag; PostCategoria categoria;
                // do it 
                SPIUS sp = new SPIUS("sp_adminfe_noticias_getPostsFromId");
                sp.agregarParametro("idPost", idPost);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if(tb[1].Rows.Count > 0){
                            rowResult = tb[1].Rows[0];
                            usu = new Usuario((int)rowResult["id_usuario_fk"]);
                            post = new Post((int)rowResult["idPost"], (DateTime)rowResult["fecha_creacion"], (DateTime)rowResult["ultima_modificacion"], rowResult["titulo"].ToString(), rowResult["contenido"].ToString(), (bool)rowResult["estado"], usu);
                        }
                        if (tb[2].Rows.Count > 0)
                        {
                            tags = new List<Tag>();
                            foreach (DataRow row in tb[2].Rows)
                            {
                                tag = new Tag((int)row["id_tags_fk"], row["tag"].ToString());
                                tags.Add(tag);
                            }
                        }
                        if (tb[3].Rows.Count > 0) {
                            categorias = new List<PostCategoria>();
                            foreach (DataRow row in tb[3].Rows)
                            {
                                categoria = new PostCategoria((int)row["id_categoria_fk"], row["categoria"].ToString());
                                categorias.Add(categoria);
                            }
                        }
                        retorno = new Dictionary<object, object>();
                        retorno.Add("post", post);
                        retorno.Add("tags", tags);
                        retorno.Add("categorias", categorias);
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
        #region "Acciones"
            public bool sp_adminfe_noticias_modificarPost(Post postActualizar, int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_modificarPost");
                sp.agregarParametro("titulo", postActualizar._titulo);
                sp.agregarParametro("contenido", postActualizar._contenido);
                sp.agregarParametro("idPost", postActualizar._idPost);
                sp.agregarParametro("idUsuarioEjecutor",idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                bool retorno = false;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        retorno = true;
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
            public Post sp_adminfe_noticias_cambiarEstadoPost(int idPost,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_cambiarEstadoPost");
                Post post = null;
                sp.agregarParametro("idPost", idPost);
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
                            post = new Post((int)row["idPost"]);
                            post._estado = (bool)row["estado"];
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
                return post;
            }
        #endregion
    }
}
