using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// liberias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.REPO.Entidades.Compartido;
namespace IUSBack.Controllers
{
    public class RepositorioCompartidoController : PadreController
    {
        #region "propiedades"
            private RepositorioCompartidoModel  _model;
            private int                         _idPagina       = (int)paginas.Repositorio;
            private string                      _nombreClass    = "RepositorioCompartidoController"; 
        #endregion
        #region "constructores"
            public RepositorioCompartidoController()
            {
                this._model = new RepositorioCompartidoModel();
            }
        #endregion

        #region "acciones url"
            public ActionResult UserShare(int id=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ViewBag.titleModulo = "Repositorio Compartido";
                    ViewBag.usuario = usuarioSession;// usuario actual y que mostrara nombre en la parte superior
                    List<Usuario> usuarios = this._model.sp_repo_getUsuariosArchivosCompartidos(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.usuariosCompartidos = usuarios;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);

                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "UserShare-"+this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "UserShare-"+this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                return View();
            }
            public ActionResult Index(int id = -1, int id2 = -1)
            {
                /*
                 * id: representa la carpeta actual
                 * id2: representa la vista que aparecera
                 */

                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Dictionary<object, object> archivos;
                    Carpeta carpeta;
                    if (id != -1)
                    {
                        carpeta = new Carpeta(id);
                        archivos = this._model.sp_repo_entrarCarpeta(carpeta, usuarioSession._idUsuario, this._idPagina);
                    }
                    else
                    {
                        archivos = this._model.sp_repo_getRootFolder(usuarioSession._idUsuario, this._idPagina);
                    }
                    ViewBag.selectedMenu = 4; // menu seleccionado 
                    ViewBag.idCarpetaActual     = id;
                    ViewBag.titleModulo         = "Repositorio Compartido";
                    ViewBag.vista               = id2;
                    ViewBag.carpetas            = archivos["carpetas"];
                    ViewBag.archivos            = archivos["archivos"];
                    ViewBag.carpetaActual       = archivos["carpetaPadre"];
                    ViewBag.usuarios            = this._model.sp_sec_getAllUsuarios(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.usuario             = usuarioSession;// usuario actual y que mostrara nombre en la parte superior
                    //********
                    ViewBag.idUsuarioSesion     = usuarioSession._idUsuario;
                    //********
                    ViewBag.usuariosCompartidos = this._model.sp_repo_getUsuariosArchivosCompartidos(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.carpetaActual       = archivos["carpetaPadre"];
                    ViewBag.menus               = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    // metricas para funciones generales
                        ViewBag.nombreControlador   = this._nombreClass.Replace("Controller", "");
                        ViewBag.nombreMetodo        = "Index";
                    // Tab seleccionada
                    ViewBag.selectedLi2 = "tabActive";
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-"+this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-"+this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                return View("~/Views/RepositorioCompartido/Indexi.cshtml");
            }
        #endregion
        #region "acciones ajax"
            #region "do"
                //public ActionResult 
                public ActionResult sp_repo_removeShareFile()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        bool estado = this._model.sp_repo_removeShareFile(this.convertObjAjaxToInt(frm["idArchivo"]), usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", estado);
                    }
                }
                catch (ErroresIUS x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                    respuesta = this.errorTryControlador(1, error);
                }
                catch (Exception x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                    respuesta = this.errorTryControlador(2, error);
                }
                return Json(respuesta);

            }
                public ActionResult sp_repo_compartirArchivo()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            ArchivoCompartido archivoAgregar = new ArchivoCompartido(this.convertObjAjaxToInt(frm["idArchivo"]), this.convertObjAjaxToInt(frm["idUsuario"]));
                            ArchivoCompartido archivoAgregado = this._model.sp_repo_compartirArchivo(archivoAgregar, usuarioSession._idUsuario, this._idPagina);
                            if (archivoAgregado != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("archivoCompartido", archivoAgregado);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Ocurrio un error inesperado", ErroresIUS.tipoError.generico, 0);
                                throw x;
                            }
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_repo_dejarDeCompartirTodo()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estado = this._model.sp_repo_dejarDeCompartirTodo(this.convertObjAjaxToInt(frm["idUsuarioCompartido"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                    return Json(respuesta);
                }
            #endregion
            #region "get"
                public ActionResult sp_repo_getUsuariosArchivosCompartidos()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<Usuario> usuarios = this._model.sp_repo_getUsuariosArchivosCompartidos(usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("usuarios", usuarios);
                        }
                        /*else
                        {
                            ErroresIUS x = new ErroresIUS("Ocurrio un error inesperado", ErroresIUS.tipoError.generico, 0);
                            throw x;
                        }*/
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_repo_getFilesFromShareUserId()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<ArchivoCompartido> archivos = this._model.sp_repo_getFilesFromShareUserId(this.convertObjAjaxToInt(frm["idUserFile"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("archivosCompartidos", archivos);

                        }
                        /*else
                        {
                            ErroresIUS x = new ErroresIUS("Ocurrio un error inesperado", ErroresIUS.tipoError.generico, 0);
                            throw x;
                        }*/
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                    return Json(respuesta);
                }
            #endregion
        #endregion

    }
}
