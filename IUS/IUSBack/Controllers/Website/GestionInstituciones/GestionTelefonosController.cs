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
    public class GestionTelefonosController : PadreController
    {
        #region "propiedades"
            public int                          _idPagina           = (int)paginas.Instituciones;
            public GestionTelefonoModel         _model;
            public GestionInstitucionesModel    _institucionModel;
            public string                       _nombreClass        = "GestionTelefonosController";
        #endregion
        #region "resultados url"
            public ActionResult Index(int id = -1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina,3);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Institucion institucion = this._institucionModel.sp_frontui_getInstitucionById(id, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.institucion = institucion;
                    ViewBag.telefonos = this._model.sp_frontui_getTelInstitucionByInstitucion(institucion._idInstitucion,usuarioSession._idUsuario,this._idPagina);
                    ViewBag.titleModulo = "Telefonos de Instituciones";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);

                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                return View("~/Views/GestionInstituciones/setTel.cshtml");
            }
        #endregion
        #region "acciones ajax"
            public ActionResult sp_frontui_editTelInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        TelefonoInstitucion telefonoEditar = new TelefonoInstitucion(this.convertObjAjaxToInt(frm["txtHdIdTel"]), frm["txtTelefonoEdit"].ToString(), frm["txtEtiquetaEdit"].ToString());
                        TelefonoInstitucion telefonoEditado = this._model.sp_frontui_editTelInstitucion(telefonoEditar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("telefono", telefonoEditado);
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
            public ActionResult sp_frontui_deleteTelInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        bool agrego = this._model.sp_frontui_deleteTelInstitucion(this.convertObjAjaxToInt(frm["txtHdIdTel"]), usuarioSession._idUsuario, this._idPagina);
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
            public ActionResult sp_frontui_insertTelInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        TelefonoInstitucion telefonoIngresar = new TelefonoInstitucion(frm["txtTelefono"].ToString(), frm["txtEtiqueta"].ToString(), this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]));
                        TelefonoInstitucion telefonoAgregado = this._model.sp_frontui_insertTelInstitucion(telefonoIngresar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = this._model.getInstanciaRespuestaAjax(usuarioSession, this._idPagina);
                        respuesta.Add("estado", true);
                        respuesta.Add("telefono", telefonoAgregado);

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
            public GestionTelefonosController()
            {
                this._model = new GestionTelefonoModel();
                this._institucionModel = new GestionInstitucionesModel();
            }
        #endregion

    }
}
