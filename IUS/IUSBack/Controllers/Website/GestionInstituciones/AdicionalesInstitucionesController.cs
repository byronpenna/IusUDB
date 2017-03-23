using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// modelos
    using IUSBack.Models.Page.GestionInstituciones.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;

namespace IUSBack.Controllers.Website.GestionInstituciones
{
    public class AdicionalesInstitucionesController : PadreController
    {
        //
        // GET: /AdicionalesInstituciones/
        #region "propiedades"
            public AdicionalInstitucionesModel  _model;
            public int                          _idPagina       = (int)paginas.Instituciones;
            public string                       _nombreClass    = "AdicionalesInstitucionesController";
        #endregion
        #region "constructores"
            public AdicionalesInstitucionesController()
            {
                this._model = new AdicionalInstitucionesModel();
            }
        #endregion
        #region "url"
            public ActionResult Index(int id=-1,int id2=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 3);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    /*
                     id: idInstitucion
                     id2: id tab
                     */
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.titleModulo     = "Manejo de instituciones";
                    ViewBag.iniciales       = this._model.getInfoInicialAdicionalInstituciones(usuarioSession._idUsuario, this._idPagina,id);
                    GestionInstitucionesModel modeloInstitucion = new GestionInstitucionesModel();
                    ViewBag.institucionActual = modeloInstitucion.sp_frontui_getInstitucionById(id,usuarioSession._idUsuario,this._idPagina);
                    ViewBag.idInstitucion   = id;
                    // variables de navegación
                        ViewBag.idTab = id2;
                        ViewBag.nombreClass = this._nombreClass.Replace("Controller", "");
                        ViewBag.nombreFuncion = "Index";
                    return View();
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
            }
        #endregion
        #region "acciones ajax"
            public ActionResult guardarOtrosInstituciones()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        Institucion institucionEditar = new Institucion(this.convertObjAjaxToInt(frm["idInstitucion"]));
                        institucionEditar._rector = frm["txtRector"].ToString();
                        institucionEditar._direccion = frm["txtDireccionInstitucion"].ToString();
                        institucionEditar._ciudad = frm["txtCiudad"].ToString();
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("institucionActualizada",this._model.actualizarOtrosInstitucion(institucionEditar, usuarioSession._idUsuario, this._idPagina));
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
            #region "revista"
                public ActionResult sp_frontui_updateRevistaInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            RevistaInstitucion revistaActualizar = new RevistaInstitucion(this.convertObjAjaxToInt(frm["txtHdIdRevista"]));
                            revistaActualizar._revista = frm["txtNombreRevista"].ToString();
                            revistaActualizar._categoria = frm["txtCategoria"].ToString();
                            revistaActualizar._anioPublicacion = this.convertObjAjaxToInt(frm["txtAnioPublicacion"]);

                            RevistaInstitucion revistaActualizada = this._model.sp_frontui_updateRevistaInstitucion(revistaActualizar,usuarioSession._idUsuario,this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("revistaActualizada", revistaActualizar);
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
                public ActionResult sp_frontui_getRevistasInstitucion()
                {

                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<RevistaInstitucion> revistasInstituciones = this._model.sp_frontui_getRevistasInstitucion(this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("revistasInstitucion",revistasInstituciones);

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
                public ActionResult sp_frontui_deleteRevistaInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = new Dictionary<object, object>();
                            bool retorno = this._model.sp_frontui_deleteRevistaInstitucion(this.convertObjAjaxToInt(frm["idRevista"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado",retorno );
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
                public ActionResult sp_frontui_addRevistaInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            RevistaInstitucion revistaAgregar = new RevistaInstitucion(-1);
                            RevistaInstitucion revistaAgregada;
                            revistaAgregar._revista = frm["txtNombreRevista"].ToString(); revistaAgregar._categoria = frm["txtCategoria"].ToString();
                            revistaAgregar._anioPublicacion = this.convertObjAjaxToInt(frm["txtAnioPublicacion"]); revistaAgregar._institucion = new Institucion(this.convertObjAjaxToInt(frm["idInstitucion"]));
                            revistaAgregada = this._model.sp_frontui_addRevistaInstitucion(revistaAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("revistaAgregada", revistaAgregada);
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
            
            public ActionResult sp_frontui_insertAreaConocimientoInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("areasConocimiento", this._model.sp_frontui_insertAreaConocimientoInstitucion(frm["strAreaCarrera"].ToString(), this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina));
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
            
            public ActionResult sp_frontui_insertNivelInstituciones()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();

                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("nivelesEducacion", this._model.sp_frontui_insertNivelInstituciones(frm["strEstadoNivel"].ToString(), frm["strNumAlumno"].ToString(), this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina));
                        respuesta.Add("estado", true);
                        //, , , 
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
        /*public ActionResult sp_frontui_insertNivelInstituciones()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();

                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("nivelesEducacion", this._model.sp_frontui_insertNivelInstituciones(frm["strEstadoNivel"].ToString(), this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina));
                        respuesta.Add("estado", true);
                        //, , , 
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
            }*/
        #endregion
    }
}
