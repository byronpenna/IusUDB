using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.ConfigRepo.Acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;

    using IUSLibs.REPO.Entidades;
namespace IUSBack.Controllers.Configuraciones
{
    public class ConfigRepoController : PadreController
    {
        #region "propiedades" 
            private int _idPagina = (int)paginas.ConfigRepo;
            public ConfigRepoModel  _model;
            public string           _nombreClass = "ConfigRepoController";
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                ActionResult    seguridadInicial    = this.seguridadInicial(this._idPagina);
                Usuario         usuarioSession      = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    ViewBag.selectedMenu    = 2; // menu seleccionado 
                    ViewBag.titleModulo     = "Repo admin";
                    string lang             = "es"; // de momento fijo en español ya que no estan contemplados multiples idiomas 
                    ViewBag.iniciales       = this._model.sp_repo_inicialesConfigRepo(lang, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
        #endregion 
        #region "acciones ajax"
            public ActionResult sp_repo_getTipoArchivo()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        List<TipoArchivo> tiposArchivos = this._model.sp_repo_getTipoArchivo("es", usuarioSession._idUsuario, this._idPagina);
                        respuesta.Add("tiposArchivos", tiposArchivos);
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
        #region "constructores"
            public ConfigRepoController(){
                this._model = new ConfigRepoModel();
            }
        #endregion
    }
}
