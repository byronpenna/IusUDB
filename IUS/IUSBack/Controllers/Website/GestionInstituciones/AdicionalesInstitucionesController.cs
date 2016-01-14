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
namespace IUSBack.Controllers.Website.GestionInstituciones
{
    public class AdicionalesInstitucionesController : PadreController
    {
        //
        // GET: /AdicionalesInstituciones/
        #region "propiedades"
            public AdicionalInstitucionesModel _model;
            public int _idPagina = (int)paginas.Instituciones;
        #endregion
        #region "constructores"
            public AdicionalesInstitucionesController()
            {
                this._model = new AdicionalInstitucionesModel();
            }
        #endregion
        #region "url"
            public ActionResult Index(int id)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 3);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.titleModulo     = "Manejo de instituciones";
                    ViewBag.iniciales       = this._model.getInfoInicialAdicionalInstituciones(usuarioSession._idUsuario, this._idPagina,id);
                    ViewBag.idInstitucion   = id;
                    return View();
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
            }
        #endregion
        #region "acciones ajax"
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
            }
        #endregion
    }
}
