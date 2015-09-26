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
    using IUSLibs.RRHH.Entidades.Laboral;
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
                    ViewBag.selectedMenu = 2; // menu seleccionado 
                    Persona persona = new Persona(id);
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.titleModulo     = "Información laboral personas";
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.iniciales       = this._model.sp_rrhh_getInfoInicialLaboralPersona(id, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.idPersona       = id;
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
            public ActionResult sp_rrhh_getActividadesEmpresa()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        List<ActividadEmpresa> actividadesEmpresa = this._model.sp_rrhh_getActividadesEmpresa(usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("actividadesEmpresa", actividadesEmpresa);
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
            public ActionResult sp_rrhh_getEditModeLaboralPersona()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        respuesta = this._model.sp_rrhh_getEditModeLaboralPersona(usuarioSession._idUsuario, this._idPagina);
                        respuesta.Add("estado", true);
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
            
            #region "laboral"
                public ActionResult sp_rrhh_editarLaboralPersonas()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            LaboralPersona laboralEditar = new LaboralPersona(this.convertObjAjaxToInt(frm["txtHdIdLaboralPersona"]), this.convertObjAjaxToInt(frm["cbEmpresa"]), this.convertObjAjaxToInt(frm["txtInicio"]), this.convertObjAjaxToInt(frm["txtFin"]), -1,frm["txtAreaObservacion"].ToString(), this.convertObjAjaxToInt(frm["cbCargo"]));
                            LaboralPersona laboralEditado = this._model.sp_rrhh_editarLaboralPersonas(laboralEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("laboralEditado", laboralEditado);
                            
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
                public ActionResult sp_rrhh_eliminarLaboralPersonas()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            bool elimino = this._model.sp_rrhh_eliminarLaboralPersonas(this.convertObjAjaxToInt(frm["txtHdIdLaboralPersona"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", elimino); 
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
                public ActionResult sp_rrhh_insertLaboralPersonas()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            LaboralPersona laboral = new LaboralPersona(this.convertObjAjaxToInt(frm["cbEmpresa"]), this.convertObjAjaxToInt(frm["txtInicio"]), this.convertObjAjaxToInt(frm["txtFin"]), this.convertObjAjaxToInt(frm["idPersona"]), frm["txtAreaObservacion"].ToString(), this.convertObjAjaxToInt(frm["cbCargo"]));
                            LaboralPersona laboralAgregada = this._model.sp_rrhh_insertLaboralPersonas(laboral, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("laboralAgregada", laboralAgregada);
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
        #endregion
            #region "constructores"
            public GestionLaboralController()
            {
                this._model = new GestionLaboralModel();
            }
        #endregion
    }
}
