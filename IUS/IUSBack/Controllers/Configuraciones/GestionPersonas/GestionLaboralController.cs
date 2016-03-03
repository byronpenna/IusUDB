using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias net framework
    using System.IO;
// librerias internas   
    using IUSBack.Models.Page.GestionPersonas.acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    // RRHH
        using IUSLibs.RRHH.Entidades.Laboral;
        using IUSLibs.RRHH.Entidades;
namespace IUSBack.Controllers.Configuraciones.GestionPersonas
{
    public class GestionLaboralController : PadreController
    {
        //
        // GET: /GestionLaboral/
        #region "propiedades"
            private int                 _idPagina       = (int)paginas.gestionPersonas;
            private GestionLaboralModel _model;
            public string               _nombreClass    = "GestionLaboralController";
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                ActionResult    seguridadInicial    = this.seguridadInicial(this._idPagina);
                Usuario         usuarioSession      = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                if (id != -1)
                {
                    try
                    {
                        ViewBag.selectedMenu = 2; // menu seleccionado 
                        Persona persona = new Persona(id);
                        Dictionary<object, object> iniciales = this._model.sp_rrhh_getInfoInicialLaboralPersona(id, usuarioSession._idUsuario, this._idPagina);
                        InformacionPersona info = (InformacionPersona)iniciales["infoPersona"];
                        if (info != null && System.IO.File.Exists(info._fotoRuta))
                        {
                            info._tieneFoto = true;
                            //informarcionPersona._fotoRuta = informarcionPersona._fotoRuta.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
                            info._fotoRuta = this.getRelativePathFromAbsolute(info._fotoRuta);
                        }
                        if (info != null && System.IO.File.Exists(info._curriculumn))
                        {
                            info._tieneCurriculumn = true;
                            info._curriculumn = this.getRelativePathFromAbsolute(info._curriculumn);
                        }

                        iniciales["infoPersona"] = info;
                        ViewBag.titleModulo = "Información laboral personas";
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                        ViewBag.iniciales = iniciales;
                        ViewBag.idPersona = id;
                    }
                    catch (ErroresIUS x)
                    {
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    }
                    catch (Exception x)
                    {
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    }
                    return View("~/Views/GestionPersonas/GestionLaboral.cshtml");
                }
                else
                {
                    return RedirectToAction("Index", "GestionPersonas");
                }
                
            }
        #endregion
        #region "resultados ajax"
            #region "actividades"
                public ActionResult sp_rrhh_editarActividadEmpresa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            ActividadEmpresa actividadEditar = new ActividadEmpresa(this.convertObjAjaxToInt(frm["txtHdIdActividadEmpresa"]),-1, frm["txtActividad"].ToString());
                            ActividadEmpresa actividadEditada = this._model.sp_rrhh_editarActividadEmpresa(actividadEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("actividadEditada", actividadEditada);
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
                public ActionResult sp_rrhh_eliminarActividadadesEmpresa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool elimino = this._model.sp_rrhh_eliminarActividadadesEmpresa(this.convertObjAjaxToInt(frm["txtHdIdActividadEmpresa"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", elimino);
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
                public ActionResult sp_rrhh_insertActividadEmpresa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            ActividadEmpresa actividadIngresar = new ActividadEmpresa(this.convertObjAjaxToInt(frm["txtHdIdLaboralPersona"]), frm["txtActividad"].ToString());
                            ActividadEmpresa actividadIngresada = this._model.sp_rrhh_insertActividadEmpresa(actividadIngresar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("actividadIngresada", actividadIngresada);
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
                public ActionResult sp_rrhh_getActividadesEmpresa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<ActividadEmpresa> actividadesEmpresa = this._model.sp_rrhh_getActividadesEmpresa(this.convertObjAjaxToInt(frm["idLaboralPersona"]), usuarioSession._idUsuario, this._idPagina);
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
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = this._model.sp_rrhh_getEditModeLaboralPersona(usuarioSession._idUsuario, this._idPagina);
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
            #region "laboral"
                public ActionResult sp_rrhh_editarLaboralPersonas()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            LaboralPersona laboralEditar = new LaboralPersona(this.convertObjAjaxToInt(frm["txtHdIdLaboralPersona"]), this.convertObjAjaxToInt(frm["cbEmpresa"]), this.convertObjAjaxToInt(frm["txtInicio"]), this.convertObjAjaxToInt(frm["txtFin"]), -1,/*frm["txtAreaObservacion"].ToString(),*/ this.convertObjAjaxToInt(frm["cbCargo"]));
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
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool elimino = this._model.sp_rrhh_eliminarLaboralPersonas(this.convertObjAjaxToInt(frm["txtHdIdLaboralPersona"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", elimino); 
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
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            LaboralPersona laboral = new LaboralPersona(this.convertObjAjaxToInt(frm["cbEmpresa"]), this.convertObjAjaxToInt(frm["txtInicio"]), this.convertObjAjaxToInt(frm["txtFin"]), this.convertObjAjaxToInt(frm["idPersona"]), /*frm["txtAreaObservacion"].ToString(),*/ this.convertObjAjaxToInt(frm["cbCargo"]));
                            LaboralPersona laboralAgregada = this._model.sp_rrhh_insertLaboralPersonas(laboral, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("laboralAgregada", laboralAgregada);
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

                public ActionResult subirCurriculumn()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        string fileName = "",path="";
                        int idPersona = this.convertObjAjaxToInt(frm["txtHdIdPersona"]);
                        if (respuesta == null)
                        {
                            if (Request.Files.Count > 0)
                            {
                                List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                                if (files.Count > 0)
                                {
                                    foreach (HttpPostedFileBase file in files)
                                    {
                                        fileName = Path.GetFileName(file.FileName);
                                        var strExtension = Path.GetExtension(file.FileName);
                                        path = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["FOTOS_PERSONAL"] + idPersona + "/"), "curriculum" + strExtension);

                                        file.SaveAs(path);
                                        ExtraGestionPersonasModel modeloInformacion = new ExtraGestionPersonasModel();
                                        InformacionPersona info = modeloInformacion.sp_rrhh_setCurriculumnPersona(path, idPersona, usuarioSession._idUsuario, this._idPagina);
                                        info._curriculumn = Url.Content(this.getRelativePathFromAbsolute(info._curriculumn));
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("informacionPersona", info);
                                    }
                                }
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
        #endregion
            #region "constructores"
            public GestionLaboralController()
            {
                this._model = new GestionLaboralModel();
            }
        #endregion
    }
}
