using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// net framework
    using System.IO;
    using System.Drawing;
// librerias internas 
    using IUSBack.Models.General;
    using IUSBack.Models.Page.GestionInstituciones.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
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
            public string                       _nombreClass = "GestionInstitucionesController";
        #endregion
        #region "acciones url"
            public ActionResult SetLogo(int id= -1)
            {
                ActionResult    seguridadInicial    = this.seguridadInicial(this._idPagina);
                Usuario         usuarioSession      = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    if (id != -1)
                    {
                        ViewBag.selectedMenu = 3; // menu seleccionado 
                        Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                        // set viewbags
                        ViewBag.rutaLogo = this._RUTASGLOBALES["LOGOS_INSTITUCIONES"];
                        ViewBag.institucion = this._model.sp_frontui_getInstitucionById(id,usuarioSession._idUsuario,this._idPagina);
                        ViewBag.titleModulo = "Logo institución";
                        ViewBag.usuario = usuarioSession;
                        //ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "SetLogo-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "SetLogo-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                return View();
            }
            public ActionResult Index()
            {
                ActionResult    seguridadInicial    = this.seguridadInicial(this._idPagina,3);
                Usuario         usuarioSession      = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    
                    
                    //Permiso permisos                    = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> inicial  = this._model.cargaInicialIndex(usuarioSession._idUsuario,this._idPagina);
                    // set viewbags
                        ViewBag.paises          = inicial["paises"];
                        ViewBag.instituciones   = inicial["instituciones"];
                        ViewBag.titleModulo     = "Manejo de instituciones";
                        ViewBag.usuario         = usuarioSession;
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
                return View();
            }
        #endregion 
        #region "acciones ajax"
            public ActionResult sp_frontui_getPaises()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        List<Pais> paises = this._model.sp_frontui_getPaises();
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("paises", paises);
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
            #region "set miniatura"
                public ActionResult getImageThumbLogo(int id)
                {
                    try
                    {
                        Institucion institucion = this._model.sp_frontui_getLogoInstitucion(id);
                        if (institucion._logo != null)
                        {
                            Stream stream = new MemoryStream(institucion._logo);
                            return new FileStreamResult(stream, "image/jpeg");
                        }
                        else
                        {
                            string path = Server.MapPath("/Content/themes/iusback_theme/img/general/image.png");
                            return base.File(path, "image/jpeg");
                        }
                    }
                    catch (ErroresIUS)
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                    catch (Exception x)
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                }
                public ActionResult setMiniaturaLogo()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            if (Request.Files.Count > 0)
                            {
                                List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                                if (files.Count > 0)
                                {
                                    foreach (HttpPostedFileBase file in files)
                                    {
                                        Coordenadas coordenadas = new Coordenadas(this.convertObjAjaxToDecimal(frm["x"]), this.convertObjAjaxToDecimal(frm["y"]), this.convertObjAjaxToDecimal(frm["imgAncho"]), this.convertObjAjaxToDecimal(frm["imgAlto"]));
                                        byte[] fileBytes = this.getBytesRecortadosFromFile(file, coordenadas); 
                                        Institucion institucionActualizar = new Institucion(this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]), fileBytes);
                                        bool estado = this._model.sp_frontui_setLogoInstitucion(institucionActualizar, usuarioSession._idUsuario, this._idPagina);
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado",estado);
                                        respuesta.Add("id", institucionActualizar._idInstitucion);
                                    }
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("No se encontro ningun fichero", ErroresIUS.tipoError.generico, 0);
                                    throw x;
                                }
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("No se enviaron archivos multimedia", ErroresIUS.tipoError.generico, 0);
                                throw x;
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
            #region "gestion institucion"
                public ActionResult sp_frontui_editInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Institucion institucionEditar = new Institucion(this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]), frm["txtNombreInstitucionEdit"].ToString(), frm["txtAreaDireccionEdit"].ToString(), this.convertObjAjaxToInt(frm["cbPaisEdit"]), true);
                            institucionEditar._ciudad = frm["txtCiudad"].ToString();
                            Institucion institucionEditada = this._model.sp_frontui_editInstitucion(institucionEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("institucion", institucionEditada);
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
                public ActionResult sp_frontui_deleteInstitucion()
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
                             bool estado = this._model.sp_frontui_deleteInstitucion(this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina);
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
                public ActionResult sp_frontui_insertInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Institucion institucionAgregar,institucionAgregada=null;
                            institucionAgregar = new Institucion(frm["txtNombreInstitucion"].ToString(), frm["txtAreaDireccion"].ToString(), this.convertObjAjaxToInt(frm["cbPais"]));
                            institucionAgregar._ciudad = frm["txtCiudad"].ToString();
                            institucionAgregada = this._model.sp_frontui_insertInstitucion(institucionAgregar, usuarioSession._idUsuario, this._idPagina);
                            if (institucionAgregada != null)
                            {
                                respuesta = this._model.getInstanciaRespuestaAjax(usuarioSession, this._idPagina);
                                respuesta.Add("estado",true);
                                respuesta.Add("institucion", institucionAgregada);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                respuesta = this.errorTryControlador(3, x);
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

    }
}
