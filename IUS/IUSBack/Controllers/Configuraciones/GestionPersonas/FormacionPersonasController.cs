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
    using IUSLibs.RRHH.Entidades.Formacion;
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
        #region "resultados ajax"
            #region "Formacion "
                public ActionResult sp_rrhh_ingresarFormacionPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            FormacionPersona formacionAgregar = new FormacionPersona(this.convertObjAjaxToInt(frm["txtYearInicio"]), this.convertObjAjaxToInt(frm["txtYearFin"]), frm["txtAreaObservaciones"].ToString(), this.convertObjAjaxToInt(frm["idPersona"]), this.convertObjAjaxToInt(frm["cbCarrera"]), this.convertObjAjaxToInt(frm["cbEstadoCarrera"]));
                            FormacionPersona formacionAgregada = this._model.sp_rrhh_ingresarFormacionPersona(formacionAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("formacionAgregada", formacionAgregada);
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
                public ActionResult sp_rrhh_eliminarTituloPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            bool estado = this._model.sp_rrhh_eliminarTituloPersona(this.convertObjAjaxToInt(frm["txtHdIdFormacionPersona"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", estado);
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
            #region "carreras"
                public ActionResult sp_rrhh_ingresarCarrera()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            Carrera carreraAgregar  = new Carrera(frm["txtNombreCarrera"].ToString(), this.convertObjAjaxToInt(frm["cbNivelCarrera"]), this.convertObjAjaxToInt(frm["cbInsticionesParaCarrera"]));
                            Carrera carreraAgregada = this._model.sp_rrhh_ingresarCarrera(carreraAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carreraAgregada", carreraAgregada);
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
            #region "instituciones educativas"
                public ActionResult sp_rrhh_eliminarInstitucionEducativa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            bool estado = this._model.sp_rrhh_eliminarInstitucionEducativa(this.convertObjAjaxToInt(frm[""]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", estado);

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
                public ActionResult sp_rrhh_ingresarInstitucionEducativa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            InstitucionEducativa institucionAgregar = new InstitucionEducativa(frm["txtInstitucionEducativa"].ToString(), this.convertObjAjaxToInt(frm["cbPaisInstitucionEducativa"]));
                            InstitucionEducativa institucionAgregada = this._model.sp_rrhh_ingresarInstitucionEducativa(institucionAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("institucionEducativa", institucionAgregada);
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
                public FormacionPersonasController()
            {
                this._model = new FormacionPersonasModel();
            }
        #endregion

    }
}
