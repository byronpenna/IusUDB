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
namespace IUSBack.Controllers
{
    public class GestionInstitucionesController : PadreController
    {
        //
        // GET: /GestionInstituciones/
        #region "constructores"
            public GestionInstitucionesController()
            {
                this._model = new GestionInstitucionesModel();
            }
        #endregion
        #region "propiedades"
            public int                          _idPagina = (int)paginas.Instituciones;
            public GestionInstitucionesModel    _model;
        #endregion
        public ActionResult Index()
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
                Dictionary<object, object> inicial = this._model.cargaInicialIndex();
                // set viewbags
                    ViewBag.paises = inicial["paises"];
                    ViewBag.titleModulo = "Manejo de instituciones";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);

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

    }
}
