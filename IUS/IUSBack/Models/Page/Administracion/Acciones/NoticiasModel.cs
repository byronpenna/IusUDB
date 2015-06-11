using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    // entidades
        using IUSLibs.ADMINFE.Entidades;
        using IUSLibs.ADMINFE.Entidades.Noticias;
    // control
        using IUSLibs.ADMINFE.Control;
        using IUSLibs.ADMINFE.Control.Noticias;
        
        using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class NoticiasModel:PadreModel
    {
        #region "propiedades"
            public ControlPostCategoria _controlPostCategoria;
            public ControlPost _controlPost;
            public ControlPostTag _controlPostTag;
            public ControlCategoriaPost _controlCategoriaPost;
        #endregion 
        #region "constructores"
            public NoticiasModel()
            {
                this._controlPostCategoria  = new ControlPostCategoria();
                this._controlPost           = new ControlPost();
                this._controlPostTag        = new ControlPostTag();
                this._controlCategoriaPost = new ControlCategoriaPost();
            }
        #endregion
        #region "generics"
            public string getComaTags(List<Tag> tags)
            {
                string toReturn = "";
                if (tags != null && tags.Count > 0)
                {
                    int cn = 0;
                    foreach (Tag tag in tags)
                    {
                        if (cn == 0)
                        {
                            toReturn += tag._strTag;
                        }
                        else
                        {
                            toReturn += ","+tag._strTag;
                        }
                        cn++;
                    }
                }
                return toReturn;
            }
        #endregion
        #region "get"
            public List<Post> sp_adminfe_noticias_getPosts(int idUsuarioEjecutor,int idPagina)
            {
                List<Post> posts = null;
                try
                {
                    posts = this._controlPost.sp_adminfe_noticias_getPosts(idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return posts;
            }
            public List<PostCategoria> sp_adminfe_noticias_getCategorias(int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    return this._controlPostCategoria.sp_adminfe_noticias_getCategorias(idUsuarioEjecutor, idPagina);
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
            public Dictionary<object, object> sp_adminfe_noticias_getPostsFromId(int idPost,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlPost.sp_adminfe_noticias_getPostsFromId(idPost, idUsuarioEjecutor, idPagina);
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
            public List<IUSLibs.TRL.Entidades.Idioma> sp_trl_getAllIdiomas(int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    IUSLibs.TRL.Control.ControlIdioma control = new IUSLibs.TRL.Control.ControlIdioma();
                    return control.sp_trl_getAllIdiomas(idUsuarioEjecutor, idPagina);
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
        #region "acciones"
            #region "ingresar"
                public Post sp_adminfe_noticias_publicarPost(Post postAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_noticias_publicarPost(postAgregar, idUsuarioEjecutor, idPagina);
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
            
            #region "update"
                public bool sp_adminfe_noticias_setThumbnailPost(Post thumbnailPost, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_noticias_setThumbnailPost(thumbnailPost, idUsuarioEjecutor, idPagina);
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
                public Post sp_adminfe_noticias_cambiarEstadoPost(int idPost, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_noticias_cambiarEstadoPost(idPost, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> sp_adminfe_noticias_agregarTag(int idPost, string[] tags, int idUsuarioEjecutor, int idPagina)
                {
                    /*
                     individual false = no todos se agregaron; global true = por lo menos uno se agrego
                     */
                    Dictionary<object, object> toReturn = new Dictionary<object, object>();
                    List<PostTag> postsTags = new List<PostTag>();
                    PostTag postTag;
                    bool estadoGlobal = false, estadoIndividual = true;
                    foreach (string tag in tags)
                    {
                        try
                        {
                            postTag = this._controlPostTag.sp_adminfe_noticias_agregarTag(idPost, tag, idUsuarioEjecutor, idPagina);
                        }
                        catch (Exception)
                        {
                            postTag = null;
                        }
                        if (postTag != null)
                        {
                            postsTags.Add(postTag);
                        }
                        else
                        {
                            estadoIndividual = false;
                        }
                    }
                    if (postsTags.Count > 0)
                    {
                        estadoGlobal = true;
                    }
                    toReturn.Add("estadoGlobal", estadoGlobal);
                    toReturn.Add("estadoIndividual", estadoIndividual);
                    toReturn.Add("postTags", postsTags);
                    return toReturn;
                }
                public Dictionary<object, object> sp_adminfe_noticias_insertCategoriasPosts(int idPost,int[] categorias,int idUsuarioEjecutor,int idPagina)
                {
                    Dictionary<object, object> toReturn = new Dictionary<object, object>();
                    List<CategoriaPost> categoriasPosts = new List<CategoriaPost>();
                    CategoriaPost categoriaPost;
                    bool estadoGlobal = false, estadoIndividual = true;
                    foreach (int idCategoria in categorias)
                    {
                        try
                        {
                            categoriaPost = this._controlCategoriaPost.sp_adminfe_noticias_insertCategoriasPosts(idPost,idCategoria,idUsuarioEjecutor,idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            categoriaPost = null;
                        }
                        catch (Exception x)
                        {
                            categoriaPost = null;
                        }
                        if (categoriaPost != null)
                        {
                            categoriasPosts.Add(categoriaPost);
                        }
                        else
                        {
                            estadoIndividual = false;
                        }
                    }
                    if (categoriasPosts.Count > 0)
                    {
                        estadoGlobal = true;
                    }
                    toReturn.Add("estadoGlobal", estadoGlobal);
                    toReturn.Add("estadoIndividual", estadoIndividual);
                    toReturn.Add("categorias", categoriasPosts);
                    return toReturn;
                }
                public List<Tag> sp_adminfe_noticias_updateTag(string tags, int idPost, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlPostTag.sp_adminfe_noticias_updateTag(tags, idPost,idUsuarioEjecutor, idPagina);
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
                public bool sp_adminfe_noticias_modificarPost(Post postActualizar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_noticias_modificarPost(postActualizar, idUsuarioEjecutor, idPagina);
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
        #endregion
    }
}