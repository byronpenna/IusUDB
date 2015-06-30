using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas 
    using IUSBack.Models.Page.GestionInstituciones.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
namespace IUSBack.Controllers
{
    public class GestionMediosInstitucionesController : PadreController
    {
        #region "propiedades"
            public int                          _idPagina = (int)paginas.Instituciones;
            public MediosInstitucionesModel     _model;
            public GestionInstitucionesModel    _institucionModel;
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
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    Permiso permisos        = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.titleModulo     = "Medios electronicos instituciones";
                    Institucion institucion = this._institucionModel.sp_frontui_getInstitucionById(id, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.institucion = institucion;
                    ViewBag.usuario     = usuarioSession;
                    ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);

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
                return View("~/Views/GestionInstituciones/setMedios.cshtml");
            }
        #endregion
        #region "constructores"
            public GestionMediosInstitucionesController()
            {
                this._model             = new MediosInstitucionesModel();
                this._institucionModel  = new GestionInstitucionesModel();
            }
        #endregion
    }
}
