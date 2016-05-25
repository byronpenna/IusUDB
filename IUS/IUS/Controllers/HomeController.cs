using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
// modelos
    using IUS.Models.page.home.acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUS.Controllers
{
    public class HomeController : PadreController
    {
        #region "propiedades"
            public int idPagina = (int)paginas.home;
            private HomeModel _model;
        #endregion
        #region "acciones url"
            /*public string Index()
            {
                return "Hola mundo :D ";
            }*/
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try{
                    //FormsAuthentication 
                    /*if (this.HttpContext.User.Identity.IsAuthenticated)
                    {
                        string str = "";
                        var x = this.HttpContext.User.Identity.Name.ToString();
                    }*/
                    ViewBag.usuarioSession  = this.getUsuarioSession();
                    ViewBag.slider          = this._model.sp_front_getSliderFromPage(this.idPagina);
                    
                    string lang             = this.getUserLang();
                    ViewBag.noticias        = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones            = this._model.getTraduccion(lang,this.idPagina);
                    string  ip              = Request.UserHostAddress;
                    //ViewBag.eventos         = this._model.sp_adminfe_front_getMonthEvents(ip, this.idPagina);
                    
                    this.setTraduccion(traducciones);
                    ViewBag.menu11 = this.activeClass;
                    ViewBag.notiEvento      = this._model.sp_adminfe_front_pantallaHome(3, ip, this.idPagina);
                    ViewBag.urlBack         = IUSLibs.GENERALS.Rutas.IUSBACK;
                }catch(ErroresIUS x){
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    //Response.Redirect(vista);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                    //return "El error es" + x.Message;
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/Home/Indexi.cshtml");
                //return "todo bien";
            }
        #endregion
        #region "ajax actions"
            public ActionResult sp_trl_getIdiomaFromIds()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        Idioma idioma = this._model.sp_trl_getIdiomaFromIds(this.convertObjAjaxToInt(frm["idIdioma"]));
                        HttpCookie cookie = Request.Cookies["idioma"];
                        if (cookie == null)
                        {
                            cookie = new HttpCookie("idioma",idioma._lang);
                        }
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("lang", idioma._lang);
                        respuesta.Add("idIdioma", idioma._idIdioma);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                        respuesta = this.errorTryControlador(1,error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,0);
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
            public HomeController()
            {
                this._model = new HomeModel();
            }
        #endregion
    }
}
