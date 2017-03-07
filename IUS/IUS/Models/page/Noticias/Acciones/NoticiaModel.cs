using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas 
    using IUS.Models.general;
// externas 
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Control.Noticias;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.FrontUI.Noticias.Entidades;
    using IUSLibs.FrontUI.Noticias.Control;
namespace IUS.Models.page.Noticias.Acciones
{
    
        public class NoticiaModel:ModeloPadre
        {
            #region "propiedades"
                private ControlPost         _controlPost;
                private ControlComentario   _controlComentario;
            #endregion
            #region "acciones"
                #region "get"
                    public Post sp_adminfe_front_getPicNoticiaFromId(int idPost)
                    {
                        try
                        {
                            return this._controlPost.sp_adminfe_front_getPicNoticiaFromId(idPost);
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
                    public Dictionary<object, object> sp_adminfe_front_getNoticiaFromId(int idPost)
                    {
                        try
                        {
                            return this._controlPost.sp_adminfe_front_getNoticiaFromId(idPost);
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
                    public List<Comentario> sp_frontUi_noticias_getComentariosPost(int idPost, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlComentario.sp_frontUi_noticias_getComentariosPost(idPost, ip, idPagina);
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
                    
                    public Dictionary<object,object> sp_adminfe_front_getNoticiasPagina(int pagina, int cn,string idioma, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlPost.sp_adminfe_front_getNoticiasPagina(pagina, cn, idioma,ip, idPagina);
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
                    public Dictionary<object,object> sp_adminfe_front_buscarNoticias(Post postBuscar, int pagina, int cn, string idioma, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlPost.sp_adminfe_front_buscarNoticias(postBuscar, pagina, cn, idioma, ip, idPagina);
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
                #region "do it"
                    public Comentario sp_frontUi_noticias_ponerComentario(Comentario comentarioAgregar,int idUsuario,int idPagina)
                    {
                        try
                        {
                            return this._controlComentario.sp_frontUi_noticias_ponerComentario(comentarioAgregar,idUsuario, idPagina);
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
            #region "constructores"
                public NoticiaModel()
                {
                    this._controlPost = new ControlPost();
                    this._controlComentario = new ControlComentario();
                }
            #endregion
        }
   
}