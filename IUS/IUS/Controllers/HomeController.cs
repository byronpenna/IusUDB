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
    public class HomeController : Controller
    {
        #region "Acciones temporales"
            #region "propuesta1"
                public ActionResult propuesta1()
                {
                    return View("~/Views/propuestas/propuesta1.cshtml");
                }
                public ActionResult propuesta1menu()
                {
                    return View("~/Views/propuestas/propuesta1menu.cshtml");
                }
            #endregion
            #region "propuesta2"
                public ActionResult propuesta2()
                {
                    return View("~/Views/propuestas/propuesta2.cshtml");
                }
            #endregion
                public ActionResult propuesta3()
                {
                    return View("~/Views/propuestas/propuesta3.cshtml");
                }
                public ActionResult propuesta4()
                {
                    return View("~/Views/propuestas/propuesta4.cshtml");
                }
        #endregion
        public ActionResult Index()
        {

            String[] lng = Request.UserLanguages;
            HomeModel modeloHome = new HomeModel(lng[0]);
            List<LlaveIdioma> traducciones;
            try{
                traducciones = modeloHome.getTraduccion();
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
                    ViewData[traduccion.llave.llave] = traduccion.traduccion;
                }
            }
            return View();
            
        }
    }
}
