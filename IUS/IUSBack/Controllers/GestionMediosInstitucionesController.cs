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
                    
                    ViewBag.enlaces     = this._model.sp_frontui_getEnlacesByInstitucion(institucion._idInstitucion,usuarioSession._idUsuario,this._idPagina);
                    ViewBag.institucion = institucion;
                    ViewBag.usuario     = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);

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
        #region "acciones ajax"
            public ActionResult sp_frontui_editEnlaceInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            EnlaceInstitucion enlaceEditar = new EnlaceInstitucion(this.convertObjAjaxToInt(frm["txtHdIdEnlace"]), frm["txtEnlace"].ToString(), frm["txtTextoEnlaceEdit"].ToString());
                            EnlaceInstitucion enlaceEditado = this._model.sp_frontui_editEnlaceInstitucion(enlaceEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("enlace", enlaceEditado);

                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                            respuesta = this.errorTryControlador(3, x);
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
            public ActionResult sp_frontui_deleteEnlaceInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        bool estado = this._model.sp_frontui_deleteEnlaceInstitucion(this.convertObjAjaxToInt(frm["txtHdIdEnlace"]), usuarioSession._idUsuario, this._idPagina);
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
            public ActionResult sp_frontui_insertEnlaceInstituciones()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {

                        EnlaceInstitucion enlaceAgregar = new EnlaceInstitucion(frm["txtEnlace"].ToString(), frm["txtTextoEnlace"].ToString(), this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]));
                        EnlaceInstitucion enlaceAgregado = this._model.sp_frontui_insertEnlaceInstituciones(enlaceAgregar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("enlace", enlaceAgregado);
                    }
                    else
                    {
                        ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(3, x);
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
            public GestionMediosInstitucionesController()
            {
                this._model             = new MediosInstitucionesModel();
                this._institucionModel  = new GestionInstitucionesModel();
            }
        #endregion
    }
}
