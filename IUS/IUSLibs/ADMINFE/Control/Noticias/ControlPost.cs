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
                public List<NotiEvento> sp_adminfe_aprobarnoticia_getNoticiasAprobar(int idUsuarioEjecutor,int idPagina)
                {
                    NotiEvento noticiaEvento;
                    List<NotiEvento> noticiasEventos = null;
                    SPIUS sp = new SPIUS("sp_adminfe_aprobarnoticia_getNoticiasAprobar");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            //[0]
                                if (tb[0].Rows.Count > 0)
                                {
                                    noticiasEventos = new List<NotiEvento>();
                                    foreach(DataRow row in tb[0].Rows){
                                        noticiaEvento = new NotiEvento((int)row["id"],row["titulo"].ToString(),row["descripcion"].ToString(),(int)row["tipoEntrada"]);
                                        noticiaEvento._fecha = (DateTime)row["fecha"];
                                        noticiaEvento._institucion = row["institucion"].ToString();
                                        noticiasEventos.Add(noticiaEvento);
                                    }
                                }
                        }
                        return noticiasEventos;
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
                public NotiEvento sp_adminfe_aprobarNoticia_cambiarEstado(NotiEvento notiEventoCambio,int idUsuarioEjecutor, int idPagina)
                {

                    SPIUS sp = new SPIUS("sp_adminfe_aprobarNoticia_cambiarEstado");
                    NotiEvento noticiaEvento = null;
                    /*
                        @				date,
		                @					int,
		                @	int,
		                -- seguridad 
		                @idUsuarioEjecutor	int,
		                @idPagina			int
                     */
                    sp.agregarParametro("caducidad", notiEventoCambio._fechaCaducidad);
                    sp.agregarParametro("idPost", notiEventoCambio._id);

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
                                noticiaEvento = new NotiEvento((int)row["id_post_fk"]);
                            }
                        }
                        return noticiaEvento;
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
                public Post sp_adminfe_noticias_cambiarEstadoPost(int idPost,int idUsuarioEjecutor,int idPagina,int estado=-1)
                {
                    SPIUS sp = new SPIUS("sp_adminfe_noticias_cambiarEstadoPost");
                    Post post = null;
                    sp.agregarParametro("idPost", idPost);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    sp.agregarParametro("estado", estado);
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
                public Dictionary<object,object> sp_adminfe_front_getNoticiasPagina(int pagina,int cn,string idioma,string ip,int idPagina)
                {
                    Dictionary<object, object> retorno = new Dictionary<object,object>();
                    List<Post> posts = null; Post post; int cnPagina =0;
                    SPIUS sp = new SPIUS("sp_adminfe_front_getNoticiasPagina");
                    sp.agregarParametro("pagina", pagina);
                    sp.agregarParametro("cn", cn);
                    sp.agregarParametro("idioma", idioma);

                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                posts = new List<Post>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    post                    = new Post((int)row["idPost"], (DateTime)row["fecha_creacion"], (DateTime)row["ultima_modificacion"], row["titulo"].ToString(), "", true, (int)row["id_usuario_fk"]);
                                    post._usuario._usuario  = row["usuario"].ToString();
                                    post._descripcion       = row["breve_descripcion"].ToString();
                                    if (row["miniatura"] != System.DBNull.Value)
                                    {
                                        post._miniatura = (byte[])row["miniatura"];
                                    }
                                    posts.Add(post);
                                }
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                cnPagina = (int)row["numPage"];
                            }
                        }
                        retorno.Add("posts", posts);
                        retorno.Add("cnPagina", cnPagina);
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
                                post = new Post((int)row["idPost"], row["titulo"].ToString(),"");
                                //post._contenido = post._contenido.Replace("&nbsp;", " ");
                                post._descripcion = row["breve_descripcion"].ToString();
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
                public Post sp_adminfe_front_getPicNoticiaFromId(int idPost)
                {
                    Post postRetorno = null;
                    SPIUS sp = new SPIUS("sp_adminfe_front_getPicNoticiaFromId");
                    sp.agregarParametro("idPost", idPost);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[0].Rows[0];
                                postRetorno = new Post((int)row["idPost"]);
                                if(row["miniatura"] != DBNull.Value){
                                    postRetorno._miniatura = (byte[])row["miniatura"];
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
                    return postRetorno;
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
                            post._usuario._persona = new Persona((int)row["id_persona_fk"]);
                            post._usuario._persona._adicionales = new RRHH.Entidades.InformacionPersona((int)row["idInformacionPersona"]);
                            post._usuario._persona._adicionales._institucion = new FrontUI.Entidades.Institucion((int)row["idInstitucion"], row["nombre"].ToString());
                            // para las tags
                            int idNextPost = -1;
                            if (tb[2].Rows.Count > 0)
                            {
                                tags = new List<Tag>();
                                foreach(DataRow rowTag in tb[2].Rows){
                                    tag = new Tag((int)rowTag["id_tags_fk"], rowTag["tag"].ToString());
                                    tags.Add(tag);
                                }
                            }
                            if (tb[4].Rows.Count > 0)
                            {
                                row = tb[4].Rows[0];
                                idNextPost = (int)row["idPost"];

                            }
                            retorno = new Dictionary<object, object>();
                            retorno.Add("post", post);
                            retorno.Add("tags", tags);
                            retorno.Add("nextPost", idNextPost);
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
                public Dictionary<object,object> sp_adminfe_front_buscarNoticias(Post postBuscar,int pagina,int cn,string idioma,string ip,int idPagina)
                {

                    Dictionary<object, object> retorno = new Dictionary<object, object>();
                    List<Post> posts = null; Post post;
                    SPIUS sp = new SPIUS("sp_adminfe_front_buscarNoticias");
                    sp.agregarParametro("pagina", pagina);
                    sp.agregarParametro("cn", cn);
                    sp.agregarParametro("idioma",idioma);

                    sp.agregarParametro("fechaIni", postBuscar._fechaInicioBusqueda);
                    sp.agregarParametro("fechaFin", postBuscar._fechaFinBusqueda);
                    sp.agregarParametro("titulo", postBuscar._titulo);
                    
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        int i = 0;
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                posts = new List<Post>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    post = new Post((int)row["idPost"], row["titulo"].ToString(), "");
                                    //post._contenido = post._contenido.Replace("&nbsp;", " ");
                                    post._descripcion = row["breve_descripcion"].ToString();
                                    /*if (row["miniatura"] != System.DBNull.Value)
                                    {
                                        post._miniatura = (byte[])row["miniatura"];
                                    }*/
                                    posts.Add(post);
                                }
                            }
                            if(tb[1].Rows.Count > 0){
                                DataRow row = tb[1].Rows[0];
                                i = (int)row["numPage"];
                            }
                            retorno.Add("posts", posts);
                            retorno.Add("numPage", i);
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
                    return retorno;
                }
            #endregion
        #endregion
    }
}
