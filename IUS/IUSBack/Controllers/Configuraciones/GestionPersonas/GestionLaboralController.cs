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
    public class GestionLaboralController : PadreController
    {
        //
        // GET: /GestionLaboral/
        #region "propiedades"
            private int                 _idPagina = (int)paginas.gestionPersonas;
            private GestionLaboralModel _model;
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
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.titleModulo     = "Información laboral personas";
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.iniciales       = this._model.sp_rrhh_getInfoInicialLaboralPersona(id, usuarioSession._idUsuario, this._idPagina);
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
                return View("~/Views/GestionPersonas/GestionLaboral.cshtml");
            }
        #endregion
        #region "resultados ajax"

        #endregion
        #region "constructores"
            public GestionLaboralController()
            {
                this._model = new GestionLaboralModel();
            }
        #endregion
    }
}
