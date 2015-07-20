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
            private RepositorioCompartidoModel _model;
            private int _idPagina = (int)paginas.Repositorio;
        #endregion
        #region "constructores"
            public RepositorioCompartidoController()
            {
                this._model = new RepositorioCompartidoModel();
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
                    ViewBag.titleModulo         = "Repositorio Compartido";
                    ViewBag.carpetas            = archivos["carpetas"];
                    ViewBag.archivos            = archivos["archivos"];
                    ViewBag.carpetaActual       = archivos["carpetaPadre"];
                    ViewBag.usuarios            = this._model.sp_sec_getAllUsuarios(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.idUsuarioSesion     = usuarioSession._idUsuario;
                    ViewBag.usuariosCompartidos = this._model.sp_repo_getUsuariosArchivosCompartidos(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.carpetaActual       = archivos["carpetaPadre"];
                    ViewBag.menus               = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
        #endregion
        #region "acciones ajax"
            public ActionResult sp_repo_getFilesFromShareUserId()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        List<Archivo> archivos = this._model.sp_repo_getFilesFromShareUserId(this.convertObjAjaxToInt(frm["idUserFile"]), usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("archivos", archivos);
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
                    if (usuarioSession != null && frm != null)
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
