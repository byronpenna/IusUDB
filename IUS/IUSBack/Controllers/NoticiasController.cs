using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUSBack.Controllers
{
    public class NoticiasController : PadreController
    {
        #region "propiedades"
            private int _idPagina = (int)paginas.Noticias;
            private NoticiasModel _model;
        #endregion 
        #region "constructores"
            public NoticiasController()
            {
                this._model = new NoticiasModel();
            }
        #endregion
        #region "url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null) { 
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        List<Post> posts = this._model.sp_adminfe_noticias_getPosts(usuarioSession._idUsuario, this._idPagina);
                        ViewBag.posts = posts;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
            public ActionResult ModificarNoticia(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        try
                        {
                            List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                            Dictionary<object, object> datosPost = this._model.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina);
                            ViewBag.permiso = permisos;
                            ViewBag.categorias = categorias;
                            ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                            ViewBag.editMode = true;
                            #region "Labels"
                                ViewBag.titlePage = "Modificar noticia";
                                ViewBag.botonAccion = "Modificar";
                            #endregion
                            #region "Valores"
                                ViewBag.post        = datosPost["post"];
                                ViewBag.tags        = this._model.getComaTags((List<Tag>)datosPost["tags"]);
                                ViewBag.categorias  = datosPost["categorias"];
                            #endregion
                            return View("~/Views/Administracion/Noticias.cshtml");
                        }catch(ErroresIUS){
                            return RedirectToAction("Unhandled", "Errors");
                        }
                        catch (Exception) {
                            return RedirectToAction("Unhandled", "Errors");
                        }
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
            public ActionResult IngresarNoticia()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                        ViewBag.permiso     = permisos;
                        ViewBag.categorias  = categorias;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.editMode    = false;
                        #region "Labels"
                            ViewBag.titlePage = "Ingresar noticia";
                            ViewBag.botonAccion = "Guardar";
                        #endregion
                        ViewBag.post = new Post();
                        return View("~/Views/Administracion/Noticias.cshtml");
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
        #endregion
        #region "generics"
            
        #endregion
        #region "acciones ajax"
            public ActionResult sp_adminfe_noticias_cambiarEstadoPost()
            {
                Dictionary<object, object> frm, respuesta ;
                frm = this.getAjaxFrm();
                Usuario usuarioSession = this.getUsuarioSesion();
                if (frm != null && usuarioSession != null)
                {
                    try
                    {
                        Post post = this._model.sp_adminfe_noticias_cambiarEstadoPost(this.convertObjAjaxToInt(frm["idPost"]), usuarioSession._idUsuario, this._idPagina);
                        if (post != null)
                        {
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("post", post);

                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                            respuesta = this.errorTryControlador(3, x);
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
            [ValidateInput(false)]
            public ActionResult sp_adminfe_noticias_publicarPost()
            {
                // vars 
                Dictionary<object, object> frm,respuestaTag, respuesta = null;
                frm = this.getAjaxFrmWithOutValidate();
                Usuario usuarioSession  = this.getUsuarioSesion();
                // do it
                    if (usuarioSession != null && frm != null)
                    {
                        try
                        {
                            Post postAgregar = new Post(frm["txtTitulo"].ToString(), frm["contenido"].ToString(), usuarioSession);
                            Post postAgregado = this._model.sp_adminfe_noticias_publicarPost(postAgregar, usuarioSession._idUsuario, this._idPagina);
                            //Post postAgregado = null;
                            if (postAgregado != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("post", postAgregado);
                                string[] tags = this.converArrAajaxToString((object[])frm["tags"]);
                                if (tags != null)
                                {
                                    respuestaTag = this._model.sp_adminfe_noticias_agregarTag(postAgregado._idPost, tags, usuarioSession._idUsuario, this._idPagina);
                                    respuesta.Add("respuestaTag", respuestaTag);
                                }
                                else
                                {
                                    respuesta.Add("respuestaTag", null);
                                }
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                respuesta = errorTryControlador(3, x);
                            }
                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                            respuesta = errorTryControlador(1, error);

                        }
                        catch (Exception x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                    

                    
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                // return 
                return Json(respuesta);
            }
        #endregion
    }
}
