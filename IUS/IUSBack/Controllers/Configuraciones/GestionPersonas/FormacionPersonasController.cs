﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Entidades;
    using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSBack.Controllers.Configuraciones.GestionPersonas
{
    public class FormacionPersonasController : PadreController
    {
        //
        // GET: /FormacionPersonas/
        #region "propiedades"
            public FormacionPersonasModel _model;
            private int _idPagina = (int)paginas.formacionAcademica;
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                Usuario usuarioSession = this.getUsuarioSesion();
                if (id != -1)
                {
                    try
                    {
                        ViewBag.selectedMenu = 2; // menu seleccionado 
                        // viewbag
                        ViewBag.titleModulo = "Información adicional personas";
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                        Dictionary<object, object>
                            informacionIni = this._model.sp_rrhh_getInfoInicialFormacion(id, usuarioSession._idUsuario, this._idPagina);
                        InformacionPersona info = (InformacionPersona)informacionIni["informacionPersona"];
                        if (info != null && System.IO.File.Exists(info._fotoRuta))
                        {
                            info._tieneFoto = true;
                            //informarcionPersona._fotoRuta = informarcionPersona._fotoRuta.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
                            info._fotoRuta = this.getRelativePathFromAbsolute(info._fotoRuta);
                        }
                        ViewBag.informacionPersona = info;
                        ViewBag.infoIni = informacionIni;
                    }
                    catch (ErroresIUS x)
                    {
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, true, "Index-FormacionPersonasController", usuarioSession._idUsuario, this._idPagina);
                    }
                    catch (Exception x)
                    {
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, "Index-FormacionPersonasController", usuarioSession._idUsuario, this._idPagina);
                    }
                    return View("~/Views/GestionPersonas/FormacionPersonas.cshtml");
                }
                else
                {
                    return RedirectToAction("Index", "GestionPersonas");
                }
                
            }
        #endregion
        #region "resultados ajax"
            #region "otros"
                public ActionResult getEditTitulos()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = this._model.getEditTitulos(usuarioSession._idUsuario, this._idPagina);
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
                public ActionResult getEditCarreras()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = this._model.getEditCarrera(usuarioSession._idUsuario, this._idPagina);
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
            #endregion
            #region "Formacion"
                public ActionResult sp_rrhh_editarFormacionPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            // -1 para id persona porq no es necesario 
                            FormacionPersona formacionEditar = new FormacionPersona(
                                        this.convertObjAjaxToInt(frm["txtHdIdFormacionPersona"]),   this.convertObjAjaxToInt(frm["txtYearFin"]), 
                                        0,                                                          frm["txtCarrera"].ToString(),                               
                                        this.convertObjAjaxToInt(frm["cbNivelCarrera"]),            this.convertObjAjaxToInt(frm["cbAreaCarrera"]),             
                                        frm["txtInstitucionEducativa"].ToString(),                  this.convertObjAjaxToInt(frm["cbPaisInstitucionEducativa"])
                                    );
                            //cbAreaCarrera,cbNivelCarrera
                            FormacionPersona formacionEditada = this._model.sp_rrhh_editarFormacionPersona(formacionEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("formacionEditada", formacionEditada);
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
                public ActionResult sp_rrhh_ingresarFormacionPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            FormacionPersona formacionAgregar = new FormacionPersona(   this.convertObjAjaxToInt(frm["txtYearFin"]),                    this.convertObjAjaxToInt(frm["idPersona"]),                     frm["txtCarrera"].ToString(),                                   
                                                                                        this.convertObjAjaxToInt(frm["cbNivelCarrera"]),                this.convertObjAjaxToInt(frm["cbAreaCarrera"]),                 
                                                                                        frm["txtInstitucionEducativa"].ToString(),                      this.convertObjAjaxToInt(frm["cbPaisInstitucionEducativa"])
                                                                                    );
                            FormacionPersona formacionAgregada = this._model.sp_rrhh_ingresarFormacionPersona(formacionAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = this._model.getInstanciaRespuestaAjax(usuarioSession, this._idPagina);
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
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
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
                public ActionResult sp_rrhh_editarCarrera()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Carrera carreraEditar = new Carrera(this.convertObjAjaxToInt(frm["txtHdIdCarrera"]), frm["txtNombreCarrera"].ToString(), this.convertObjAjaxToInt(frm["cbNivelCarrera"]), this.convertObjAjaxToInt(frm["cbInsticionesParaCarrera"]), this.convertObjAjaxToInt(frm["cbAreaCarreras"]));
                            Carrera carreraEditada = this._model.sp_rrhh_editarCarrera(carreraEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carreraEditada", carreraEditada);
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
                public ActionResult sp_rrhh_eliminarCarrera()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estado = this._model.sp_rrhh_eliminarCarrera(this.convertObjAjaxToInt(frm["txtHdIdCarrera"]), usuarioSession._idUsuario, this._idPagina);
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
                public ActionResult sp_rrhh_ingresarCarrera()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Carrera carreraAgregar = new Carrera(frm["txtNombreCarrera"].ToString(), this.convertObjAjaxToInt(frm["cbNivelCarrera"]), this.convertObjAjaxToInt(frm["cbInsticionesParaCarrera"]), this.convertObjAjaxToInt(frm["cbAreaCarreras"]));
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
                public ActionResult sp_rrhh_editarInstitucionEducativa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            InstitucionEducativa institucionEditar = new InstitucionEducativa(this.convertObjAjaxToInt(frm["txtHdIdInstitucionEducativa"]), frm["txtInstitucionEducativa"].ToString(), this.convertObjAjaxToInt(frm["cbPaisInstitucionEducativa"]));
                            InstitucionEducativa institucionEditada = this._model.sp_rrhh_editarInstitucionEducativa(institucionEditar,usuarioSession._idUsuario,this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("institucionEditada", institucionEditada);
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
                public ActionResult sp_rrhh_eliminarInstitucionEducativa()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estado = this._model.sp_rrhh_eliminarInstitucionEducativa(this.convertObjAjaxToInt(frm["txtHdIdInstitucionEducativa"]), usuarioSession._idUsuario, this._idPagina);
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
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
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
