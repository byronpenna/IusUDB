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

    }
}
