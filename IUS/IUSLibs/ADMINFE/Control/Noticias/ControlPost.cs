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
    using IUSLibs.TRL.Entidades;
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlPost:PadreLib
    {
        #region "backend"
            #region "Get"
                public List<Post> sp_adminfe_noticias_getPosts(int idUsuarioEjecutor, int idPagina) {
                    List<Post> posts = null; Idioma idioma;
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
                                    post = new Post((int)row["idPost"], (DateTime)row["fecha_creacion"], (DateTime)row["ultima_modificacion"], row["titulo"].ToString(), "", (bool)row["estado"], usuarioCreador);
                                    //post._contenido = post._contenido.Replace("&nbsp;", " ");
                                    post._descripcion = row["breve_descripcion"].ToString();
                                    idioma = new Idioma((int)row["idIdioma"], row["idioma"].ToString());
                                    post._idioma = idioma;
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
                                post._idioma = new Idioma((int)rowResult["id_idioma_fk"]);
                                post._descripcion = rowResult["breve_descripcion"].ToString();
                                if (rowResult["miniatura"] != DBNull.Value)
                                {
                                    post._miniatura = (byte[])rowResult["miniatura"];
                                }
                                
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
                    sp.agregarParametro("idIdioma", postActualizar._idioma._idIdioma);
                    sp.agregarParametro("descripcion", postActualizar._descripcion);
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
                    sp.agregarParametro("idIdioma", postAgregar._idioma._idIdioma);
                    sp.agregarParametro("breveDescripcion", postAgregar._descripcion);
                    
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
                public bool sp_adminfe_noticias_setThumbnailPost(Post thumbnailPost,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_adminfe_noticias_setThumbnailPost");
                    sp.agregarParametro("imagen", thumbnailPost._miniatura);
                    sp.agregarParametro("idPost", thumbnailPost._idPost);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
            #endregion
        #endregion
        #region "front end"
            #region "get"
                public List<Post> sp_adminfe_front_getTopNoticias(int n,string lang="")
                {
                    SPIUS sp = new SPIUS("sp_adminfe_front_getTopNoticias");
                    List<Post> posts = null; Post post;
                    sp.agregarParametro("n", n);
                    if (lang != "")
                    {
                        sp.agregarParametro("idioma", lang);
                    }
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb) && tb[1].Rows.Count > 0)
                        {
                            posts = new List<Post>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                post = new Post((int)row["idPost"], row["titulo"].ToString(), row["contenido"].ToString());
                                post._contenido = post._contenido.Replace("&nbsp;", " ");
                                if (row["miniatura"] != System.DBNull.Value)
                                {
                                    post._miniatura = (byte[])row["miniatura"];
                                }
                                posts.Add(post);
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
                public Dictionary<object,object> sp_adminfe_front_getNoticiaFromId(int idPost)
                {
                    Post post = null; /*PostCategoria cat = null;*/ List<Tag> tags = null; Tag tag;
                    Dictionary<object,object> retorno = null;

                    SPIUS sp = new SPIUS("sp_adminfe_front_getNoticiaFromId");
                    sp.agregarParametro("idPost", idPost);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb) && tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            post = new Post((int)row["idPost"], (DateTime)row["fecha_creacion"], (DateTime)row["ultima_modificacion"], row["titulo"].ToString(), row["contenido"].ToString(),(bool)row["estado"],(int)row["id_usuario_fk"]);
                            // para las tags
                            if (tb[2].Rows.Count > 0)
                            {
                                tags = new List<Tag>();
                                foreach(DataRow rowTag in tb[2].Rows){
                                    tag = new Tag((int)rowTag["id_tags_fk"], rowTag["tag"].ToString());
                                    tags.Add(tag);
                                }
                            }
                            retorno = new Dictionary<object, object>();
                            retorno.Add("post", post);
                            retorno.Add("tags", tags);
                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.sql, 0);
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
                    return retorno;
                }
            #endregion
        #endregion
    }
}
