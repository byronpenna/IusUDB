using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.RecursosHumanos.Acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    
    using IUSLibs.RRHH.Entidades;
namespace IUSBack.Controllers.Website
{
    public class RecursosHumanosController : PadreController
    {
        #region "propiedades"
            private int                 _idPagina = (int)paginas.RecursosHumanos;
            public RecursosHumanosModel _model;
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
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.selectedMenu    = 3; // menu seleccionado 
                    ViewBag.titleModulo     = "Recursos humanos";
                    // objetos para pagina
                    //ViewBag.nivelCarrera    = this._model.sp_rrhh_getNivelesCarreras(usuarioSession._idUsuario,this._idPagina);
                    //ViewBag.paises          = this._model.
                    ViewBag.objInicial      = this._model.cargaInicial(usuarioSession._idUsuario,this._idPagina);
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
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
                return View();
            }
        #endregion
        #region "genericos"
            public Dictionary<object, object> getArrElementosBusqueda(Dictionary<object,object> frm)
            {
                Dictionary<object, object> retorno = new Dictionary<object, object>();
                // variables arr
                int[] rubros = null; int[] areas = null;
                int[] niveles = null;
                // variables string 
                string strRubros=null; string strAreas=null; string strNiveles= null;
                if (frm.Keys.Contains("cbRubros"))
                {
                    rubros = this.convertArrAjaxToInt(frm["cbRubros"]);
                    strRubros = string.Join(",", rubros);
                }
                if (frm.Keys.Contains("cbAreas"))
                {
                    areas = this.convertArrAjaxToInt(frm["cbAreas"]);
                    strAreas = string.Join(",", areas);
                }
                if (frm.Keys.Contains("cbNiveles"))
                {
                    niveles = this.convertArrAjaxToInt(frm["cbNiveles"]);
                    strNiveles = string.Join(",",niveles);
                }
                retorno.Add("rubros", strRubros);
                retorno.Add("areas", strAreas);
                retorno.Add("niveles", strNiveles);
                return retorno;
            }
        #endregion
        #region "acciones ajax"
            public ActionResult sp_rrhh_detallePesona()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        respuesta = this._model.sp_rrhh_detallePesona(this.convertObjAjaxToInt(frm["txtHdIdPersona"]), usuarioSession._idUsuario, this._idPagina);
                        InformacionPersona informarcionPersona = (InformacionPersona)respuesta["infoPersona"];
                        if (informarcionPersona != null && System.IO.File.Exists(informarcionPersona._fotoRuta))
                        {
                            informarcionPersona._tieneFoto = true;
                            //informarcionPersona._fotoRuta = informarcionPersona._fotoRuta.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
                            informarcionPersona._fotoRuta = this.getRelativePathFromAbsolute(informarcionPersona._fotoRuta);
                            informarcionPersona._fotoRuta = Url.Content(informarcionPersona._fotoRuta);
                        }
                        respuesta["infoPersona"] = informarcionPersona;
                        respuesta.Add("estado", true);

                    }
                    else
                    {
                        respuesta = errorEnvioFrmJSON();
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
            public ActionResult sp_rrhh_buscarPersonas()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        Dictionary<object, object> objetos = new Dictionary<object, object>();
                        objetos = this.getArrElementosBusqueda(frm);
                        List<Persona> personas = this._model.sp_rrhh_buscarPersonas(objetos, frm["txtCarrera"].ToString(), null, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("personas", personas);
                    }
                    else
                    {
                        respuesta = errorEnvioFrmJSON();
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
            public RecursosHumanosController()
            {
                this._model = new RecursosHumanosModel();

            }
        #endregion
    }
}
