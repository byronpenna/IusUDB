using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUSBack.Controllers
{
    public class RepositorioPublicoController : PadreController
    {
        #region "propiedades"
            public int                      _idPagina   = (int)paginas.RepositorioPublico;
            public RepositorioPublicoModel  _model;
        #endregion
        #region "constructor"
            public RepositorioPublicoController()
            {
                this._model = new RepositorioPublicoModel();
            }
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    // viewback 
                    ViewBag.titleModulo = "Repositorio publico";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.permisos = permisos;
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    ViewBag.carpetas = this._model.sp_repo_getRootFolderPublico(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.idCarpetaActual = id;
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error  = new ErrorsController();
                    return error.redirectToError(x,true);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
        #endregion
        #region "resultados ajax"
            public ActionResult sp_repo_deleteCarpetaPublica()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        bool estado = this._model.sp_repo_deleteCarpetaPublica(this.convertObjAjaxToInt(frm["idCarpeta"]), usuarioSession._idUsuario, this._idPagina);
                        if (estado)
                        {
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", estado);
                        }
                        else
                        {
                            ErroresIUS error = new ErroresIUS("Error inesperado", ErroresIUS.tipoError.generico, 0);
                            throw error;
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
            public ActionResult sp_repo_updateCarpetaPublica()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        CarpetaPublica carpetaUpdate = new CarpetaPublica(this.convertObjAjaxToInt(frm["txtHdIdCarpeta"]), frm["nombre"].ToString());
                        CarpetaPublica carpetaActualizada = this._model.sp_repo_updateCarpetaPublica(carpetaUpdate,usuarioSession._idUsuario,this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("carpeta", carpetaActualizada);

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
            public ActionResult sp_repo_insertCarpetaPublica()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        CarpetaPublica carpetaIngresar = new CarpetaPublica(frm["nombre"].ToString(), this.convertObjAjaxToInt(frm["idCarpetaPadre"]));
                        CarpetaPublica carpetaIngresada = this._model.sp_repo_insertCarpetaPublica(carpetaIngresar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("carpeta",carpetaIngresada);
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
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

    }
}
