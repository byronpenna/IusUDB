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
            public ActionResult nombre(){
                return null;
            }
        #endregion 
        #region "constructores"
            public ConfigRepoController(){
                this._model = new ConfigRepoModel();
            }
        #endregion
    }
}
