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
    using IUSLibs.TRL.Entidades;
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
            public ActionResult setMiniatura(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        try
                        {
                            ViewBag.titleModulo = "Escoger miniatura foto";
                            ViewBag.usuario = usuarioSession;
                            ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                            ViewBag.permiso = permisos;
                            ViewBag.post = this._model.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina)["post"];
                            return View();
                        }
                        catch (ErroresIUS x)
                        {
                            return RedirectToAction("Unhandled", "Erros");
                        }
                        catch (Exception x)
                        {
                            return RedirectToAction("Unhandled", "Erros");
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
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null) { 
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.titleModulo = "Noticias";
                        ViewBag.usuario     = usuarioSession;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.permiso     = permisos;
                        List<Post> posts    = this._model.sp_adminfe_noticias_getPosts(usuarioSession._idUsuario, this._idPagina);
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
                    if (permisos != null && permisos._ver && permisos._editar)
                    {
                        try
                        {
                            List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                            Dictionary<object, object> datosPost = this._model.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina);
                            ViewBag.permiso = permisos;
                            ViewBag.categorias = categorias;
                            ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                            ViewBag.editMode = true;
                            ViewBag.idiomas = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                            #region "Labels"
                                ViewBag.titleModulo = "Modificar noticia";
                                ViewBag.botonAccion = "Modificar";
                                ViewBag.usuario     = usuarioSession;
                                ViewBag.accion      = 0;
                            #endregion
                            #region "Valores"
                                ViewBag.post        = datosPost["post"];
                                ViewBag.tags        = this._model.getComaTags((List<Tag>)datosPost["tags"]);
                            #endregion
                            return View("~/Views/Administracion/Noticias.cshtml");
                        }catch(ErroresIUS){
                            return RedirectToAction("Unhandled", "Errors");
                        }
                        catch (Exception x) {
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
                    if (permisos != null && permisos._ver && permisos._crear)
                    {
                        List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                        ViewBag.permiso     = permisos;
                        ViewBag.categorias  = categorias;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.editMode    = false;
                        ViewBag.idiomas     = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                        #region "Labels"
                            ViewBag.titleModulo     = "Ingresar noticia";
                            ViewBag.usuario         = usuarioSession;
                            ViewBag.botonAccion     = "Guardar";
                            ViewBag.accion          = 1;
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
            public ActionResult sp_adminfe_noticias_setThumbnailPost()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    var form = this._jss.Deserialize<Dictionary<object,object>>(Request.Form["form"]);
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (Request.Files.Count > 0)
                    {
                        List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                        if(files != null){
                            foreach(HttpPostedFileBase file in files ){
                                byte[] fileBytes = this.getBytesFromFile(file);
                                Post postAgregar = new Post( this.convertObjAjaxToInt(form["txtHdIdPost"]) );
                                postAgregar._miniatura = fileBytes;
                                bool estado = this._model.sp_adminfe_noticias_setThumbnailPost(postAgregar, usuarioSession._idUsuario, this._idPagina);
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", estado);
                            }
                        }else{
                            ErroresIUS x = new ErroresIUS("No hay imagenes",ErroresIUS.tipoError.generico,0);
                            throw x;
                        }
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                }
                catch (ErroresIUS x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                    respuesta = this.errorTryControlador(1, error);
                }
                catch (Exception x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                    respuesta = this.errorTryControlador(2, error);
                }
                return Json(respuesta);
            }
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
                Dictionary<object, object> frm,respuestaTag,respuestaCate, respuesta = null;
                frm = this.getAjaxFrmWithOutValidate();
                Usuario usuarioSession  = this.getUsuarioSesion();
                // do it
                    if (usuarioSession != null && frm != null)
                    {
                        try
                        {
                            Idioma idioma = new Idioma(this.convertObjAjaxToInt(frm["cbIdioma"]));
                            Post postAgregar = new Post(frm["txtTitulo"].ToString(), frm["contenido"].ToString(), usuarioSession,idioma);
                            Post postAgregado = this._model.sp_adminfe_noticias_publicarPost(postAgregar, usuarioSession._idUsuario, this._idPagina);
                            //Post postAgregado = null;
                            if (postAgregado != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("post", postAgregado);
                                string[] tags = this.converArrAajaxToString((object[])frm["tags"]);
                                int[] categorias = this.convertArrAjaxToInt((object[])frm["cbCategorias"]);
                                if (tags != null)
                                {
                                    respuestaTag = this._model.sp_adminfe_noticias_agregarTag(postAgregado._idPost, tags, usuarioSession._idUsuario, this._idPagina);
                                    respuesta.Add("respuestaTag", respuestaTag);
                                }
                                else
                                {
                                    respuesta.Add("respuestaTag", null);
                                }

                                if (categorias != null)
                                {
                                    respuestaCate = this._model.sp_adminfe_noticias_insertCategoriasPosts(postAgregado._idPost, categorias, usuarioSession._idUsuario, this._idPagina);
                                    respuesta.Add("respuestaCate", respuestaCate);
                                }
                                else
                                {
                                    respuesta.Add("respuestaCate", null);
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
            #region "Modificar"
                public ActionResult sp_adminfe_noticias_modificarPost()
            {
                Dictionary<object, object> frm, respuesta = null;
                frm = this.getAjaxFrmWithOutValidate();
                Usuario usuarioSession = this.getUsuarioSesion();
                if (frm != null && usuarioSession != null)
                {
                    try
                    {
                        int idPost          = this.convertObjAjaxToInt(frm["txtHdIdPost"]);
                        int[] idCategorias  = this.convertArrAjaxToInt( (object[]) frm["cbCategorias"]);
                        Post postActualizar = new Post(idPost, frm["txtTitulo"].ToString(), frm["contenido"].ToString());
                        string tags         = frm["tags"].ToString();
                        bool actualizo      = this._model.sp_adminfe_noticias_modificarPost(postActualizar, usuarioSession._idUsuario, this._idPagina);
                        List<Tag> tagList   = this._model.sp_adminfe_noticias_updateTag(tags, idPost, usuarioSession._idUsuario, this._idPagina);
                        List<CategoriaPost> categorias = this._model.sp_adminfe_noticias_updateCategoriaPost(idCategorias, idPost, usuarioSession._idUsuario, _idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", actualizo);
                        respuesta.Add("tags", tagList);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
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
                return Json(respuesta);
            }
            #endregion
        #endregion
    }
}
