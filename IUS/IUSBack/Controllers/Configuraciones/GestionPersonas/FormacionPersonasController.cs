using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers.Configuraciones.GestionPersonas
{
    public class FormacionPersonasController : PadreController
    {
        //
        // GET: /FormacionPersonas/
        #region "propiedades"
            public FormacionPersonasModel _model;
            private int _idPagina = (int)paginas.gestionPersonas;
        #endregion
        #region "acciones url"
            public ActionResult Index(int id)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    ViewBag.selectedMenu        = 2; // menu seleccionado 
                    Usuario usuarioSession      = this.getUsuarioSesion();
                    // viewbag
                    ViewBag.titleModulo         = "Información adicional personas";
                    ViewBag.menus               = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    Dictionary<object, object> 
                        informacionIni          = this._model.sp_rrhh_getInfoInicialFormacion(id, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.infoIni             = informacionIni;
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/GestionPersonas/FormacionPersonas.cshtml");
            }
        #endregion
        #region "constructores"
            public FormacionPersonasController()
            {
                this._model = new FormacionPersonasModel();
            }
        #endregion

    }
}
