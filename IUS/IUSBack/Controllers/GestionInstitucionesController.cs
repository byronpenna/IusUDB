using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// net framework
    using System.IO;
// librerias internas 
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
        #endregion
        #region "acciones url"
            public ActionResult SetLogo(int id= -1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    if (id != -1)
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                        // set viewbags
                        ViewBag.rutaLogo = this._RUTASGLOBALES["LOGOS_INSTITUCIONES"];
                        ViewBag.institucion = this._model.sp_frontui_getInstitucionById(id,usuarioSession._idUsuario,this._idPagina);
                        ViewBag.titleModulo = "Logo institución";
                        ViewBag.usuario = usuarioSession;
                        ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    }
                    else
                    {
                        return null;
                    }

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
            public ActionResult Index()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession              = this.getUsuarioSesion();
                    Permiso permisos                    = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> inicial  = this._model.cargaInicialIndex(usuarioSession._idUsuario,this._idPagina);
                    // set viewbags
                        ViewBag.paises          = inicial["paises"];
                        ViewBag.instituciones   = inicial["instituciones"];
                        ViewBag.titleModulo     = "Manejo de instituciones";
                        ViewBag.usuario         = usuarioSession;
                        ViewBag.subMenus        = this._model.getMenuUsuario(usuarioSession._idUsuario);

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
        #endregion 
        #region "acciones ajax"
            public ActionResult sp_frontui_getPaises()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        List<Pais> paises = this._model.sp_frontui_getPaises();
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("paises", paises);
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
            #region "set miniatura"
                public ActionResult setMiniaturaLogo()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            if (Request.Files.Count > 0)
                            {
                                List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                                if (files.Count > 0)
                                {
                                    foreach (HttpPostedFileBase file in files)
                                    {
                                        /*var strExtension    = Path.GetExtension(file.FileName);
                                        string strDireccion = this._RUTASGLOBALES["LOGOS_INSTITUCIONES"] + frm["txtHdIdInstitucion"].ToString() + strExtension;
                                        string path         = Server.MapPath(strDireccion);
                                        file.SaveAs(path);
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("ruta", Url.Content(strDireccion));*/
                                        byte[] fileBytes = this.getBytesFromFile(file);
                                        Institucion institucionActualizar = new Institucion(this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]), fileBytes);
                                        bool estado = this._model.sp_frontui_setLogoInstitucion(institucionActualizar, usuarioSession._idUsuario, this._idPagina);
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado",estado);
                                        
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
            #region "gestion institucion"
                public ActionResult sp_frontui_editInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                            Institucion institucionEditar = new Institucion(this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]), frm["txtNombreInstitucionEdit"].ToString(), frm["txtAreaDireccionEdit"].ToString(), this.convertObjAjaxToInt(frm["cbPaisEdit"]), true);
                            Institucion institucionEditada = this._model.sp_frontui_editInstitucion(institucionEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("institucion", institucionEditada);
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
                public ActionResult sp_frontui_deleteInstitucion()
                {
                     Dictionary<object, object> frm, respuesta = null;
                     try
                     {
                         Usuario usuarioSession = this.getUsuarioSesion();
                         frm = this.getAjaxFrm();
                         if (usuarioSession != null && frm != null)
                         {
                             respuesta = new Dictionary<object, object>();
                             bool estado = this._model.sp_frontui_deleteInstitucion(this.convertObjAjaxToInt(frm["idInstitucion"]), usuarioSession._idUsuario, this._idPagina);
                             respuesta.Add("estado", estado);
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
                public ActionResult sp_frontui_insertInstitucion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && frm != null)
                        {
                        
                            Institucion institucionAgregar,institucionAgregada=null;
                            institucionAgregar = new Institucion(frm["txtNombreInstitucion"].ToString(), frm["txtAreaDireccion"].ToString(), this.convertObjAjaxToInt(frm["cbPais"]));
                            institucionAgregada = this._model.sp_frontui_insertInstitucion(institucionAgregar, usuarioSession._idUsuario, this._idPagina);
                            if (institucionAgregada != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado",true);
                                respuesta.Add("institucion", institucionAgregada);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                respuesta = this.errorTryControlador(3, x);
                            }
                        
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

    }
}
