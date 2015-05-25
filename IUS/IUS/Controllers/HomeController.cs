using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// modelos
using IUS.Models.page.home.acciones;
using IUSLibs.TRL.Entidades;
using IUSLibs.LOGS;
namespace IUS.Controllers
{
    public class HomeController : PadreController
    {
        #region "propiedades"
            public int idPagina = (int)paginas.home;
            private HomeModel _model;
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                String[] lng = Request.UserLanguages;
                HomeModel modeloHome = new HomeModel();
                List<LlaveIdioma> traducciones;
                try{
                    string lang = lng[0];
                    /*HttpCookie co = new HttpCookie("bla","mundo");
                    Request.Cookies.Add(co);*/
                    HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["bla"];
                    if (cookie != null)
                    {
                        //lang = cookie["idioma"];
                    }
                    traducciones = modeloHome.getTraduccion(lang);
                    ViewBag.idiomas = modeloHome.getIdiomas();
                }catch(ErroresIUS x){
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    //Response.Redirect(vista);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                }
                if (traducciones != null)
                {
                    foreach (LlaveIdioma traduccion in traducciones)
                    {
                        ViewData[traduccion._llave._llave] = traduccion._traduccion;
                    }
                }
                return View();

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
                        //Response.Cookies.Clear();
                        HttpCookie cookie = Request.Cookies["idioma"];
                        if (cookie == null)
                        {
                            cookie = new HttpCookie("idioma",idioma._lang);
                        }
                        //cookie["idioma"] = idioma._lang;
                        cookie.Expires = DateTime.Now.AddYears(1);
                        this.ControllerContext.HttpContext.Request.Cookies.Set(cookie);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
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
