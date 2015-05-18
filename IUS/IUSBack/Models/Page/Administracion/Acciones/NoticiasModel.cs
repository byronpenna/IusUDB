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
        #endregion 
        #region "constructores"
            public NoticiasModel()
            {
                this._controlPostCategoria  = new ControlPostCategoria();
                this._controlPost           = new ControlPost();
                this._controlPostTag = new ControlPostTag();
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
        #endregion
        #region "acciones"
            public Post sp_adminfe_noticias_publicarPost(Post postAgregar,int idUsuarioEjecutor,int idPagina)
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
            
            public Dictionary<object,object> sp_adminfe_noticias_agregarTag(int idPost, string[] tags, int idUsuarioEjecutor, int idPagina)
            {
                /*
                 individual false = no todos se agregaron; global true = por lo menos uno se agrego
                 */
                Dictionary<object, object> toReturn = new Dictionary<object,object>();
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
                if(postsTags.Count > 0){
                    estadoGlobal = true;
                }
                toReturn.Add("estadoGlobal", estadoGlobal);
                toReturn.Add("estadoIndividual", estadoIndividual);
                toReturn.Add("postTags", postsTags);
                return toReturn;
            }
        #endregion
    }
}