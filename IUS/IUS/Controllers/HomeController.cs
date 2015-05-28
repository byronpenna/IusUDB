using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                String[] lng = Request.UserLanguages;
                HomeModel modeloHome = new HomeModel();
                List<LlaveIdioma> traducciones;
                try{
                    string lang = lng[0];
                    ViewBag.slider = this._model.sp_front_getSliderFromPage(this.idPagina);
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(2);
                    HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["IUSidioma"];
                    if (cookie != null)
                    {
                        lang = cookie.Value;
                    }
                    traducciones = modeloHome.getTraduccion(lang);
                    ViewBag.idiomas = modeloHome.getIdiomas();
                }catch(ErroresIUS x){
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    //Response.Redirect(vista);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                    //return "El error es" + x.Message;
                }
                if (traducciones != null)
                {
                    foreach (LlaveIdioma traduccion in traducciones)
                    {
                        ViewData[traduccion._llave._llave] = traduccion._traduccion;
                    }
                }
                return View("~/Views/Home/Index.cshtml");
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
