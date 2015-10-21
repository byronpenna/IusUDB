using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.RecursosHumanos.Acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers.Website
{
    public class RecursosHumanosController : PadreController
    {
        #region "propiedades"
            private int                 _idPagina = (int)paginas.RecursosHumanos;
            public RecursosHumanosModel _model;
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.selectedMenu    = 3; // menu seleccionado 
                    ViewBag.titleModulo     = "Recursos humanos";
                    // objetos para pagina
                    //ViewBag.nivelCarrera    = this._model.sp_rrhh_getNivelesCarreras(usuarioSession._idUsuario,this._idPagina);
                    //ViewBag.paises          = this._model.
                    ViewBag.objInicial      = this._model.cargaInicial(usuarioSession._idUsuario,this._idPagina);
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
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
                return View();
            }
        #endregion
        #region "acciones ajax"
            public ActionResult sp_rrhh_buscarPersonas()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        if (frm.Keys.Contains("cbNiveles"))
                        {
                            int[] actividades = this.convertArrAjaxToInt(frm["cbNiveles"]);
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
        #endregion
        #region "constructores"
            public RecursosHumanosController()
            {
                this._model = new RecursosHumanosModel();

            }
        #endregion
    }
}
