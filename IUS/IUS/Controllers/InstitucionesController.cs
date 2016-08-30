using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUS.Models.page.Instituciones.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Control;
    using IUSLibs.FrontUI.Entidades;
namespace IUS.Controllers
{
    public class InstitucionesController : PadreController
    {
        //
        // GET: /Instituciones/
        #region "propiedades"
            public int idPagina                 = (int)paginas.Instituciones;
            public int idPaginaFichaInstitucion = (int)paginasInternas.FichaInstitucion;
            private InstitucionesModel _model;
        #endregion
        #region "acciones url"
            public ActionResult VerInstituciones(int id)
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.usuarioSession = this.getUsuarioSession();
                    string lang = this.getUserLang();
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    string ip = Request.UserHostAddress;
                    ViewBag.objIniciales = this._model.sp_frontui_getInstitucionesByContinente(id,lang, ip, this.idPagina);
                    //ViewBag.paises = this._model.sp_frontui_getPaisesFromContinente(id, ip, this.idPagina);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    ViewBag.menu22 = this.activeClass;
                    this.setTraduccion(traducciones);
                    
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
            public ActionResult FichaInstitucion(int id=-1)
            {
                
                if (id == -1)
                {
                    return RedirectToAction("Index", "Instituciones");
                }
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.usuarioSession = this.getUsuarioSession();
                    string lang = this.getUserLang();
                    //Institucion institucion = this._model.sp
                    string ip = Request.UserHostAddress;
                    Dictionary<object, object> data = this._model.sp_frontui_front_getInstitucionById(id, ip, this.idPaginaFichaInstitucion,lang);
                    ViewBag.data = data;
                    ViewBag.menu13 = this.activeClass;
                    traducciones = this._model.getTraduccion(lang, this.idPaginaFichaInstitucion);
                    this.setTraduccion(traducciones);
                    ViewBag.menu22 = this.activeClass;
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/Instituciones/FichaInstitucioni.cshtml");
            }
            public ActionResult Index(int id=-1)
            {
                List<LlaveIdioma> traducciones;
                /***/
                /*int     cn = 0;
                bool    contar = false;
                int salario = 0;
                do {
                    // no recuerdo como obtener el texto del string reader asi que usaer .getString();
                    
                    string textoLector = reader.getString();
                    if(textoLector.Contains("Mes:")){
                        contar = true;
                        cn = 0;
                    }
                    if(textoLector == ""){
                        contar = false;
                        cn = 0;
                    }
                    string[] arr;
                    if(cn > 2){ // porque no cuentan las lineas antes de los salarios
                        arr = textoLector.Split(" ");
                        salario += arr[11];
                    }
                    if(contar){
                        cn++;
                    }else if(contar && cn >6){}
                }
                while(reader.read())*/
                /***/
                try
                {
                    string ip               = Request.UserHostAddress;
                    ViewBag.usuarioSession  = this.getUsuarioSession();
                    string lang             = this.getUserLang();
                    //ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones            = this._model.getTraduccion(lang, this.idPagina);
                    //ViewBag.menu22          = this.activeClass;
                    ViewBag.idContinente    = id;
                    this.setTraduccion(traducciones);
                    ViewBag.menu13          = this.activeClass;
                    ViewBag.notiEvento      = this._model.sp_adminfe_front_pantallaHome(3,1, ip, this.idPagina);
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x) {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/Instituciones/Indexi.cshtml");

            }
        #endregion
        #region "acciones ajax"
            public ActionResult sp_frontui_getInstitucionesByContinente()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        string ip = Request.UserHostAddress;
                        string lang = this.getUserLang();
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("instituciones",this._model.sp_frontui_getInstitucionesByContinente(this.convertObjAjaxToInt(frm["id"]), lang, ip, this.idPagina));
                        respuesta.Add("estado", true);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
        #endregion
        #region "constructores"
            public InstitucionesController()
            {
                this._model = new InstitucionesModel();
            }
        #endregion
    }
}
