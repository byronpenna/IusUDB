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
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                String[] lng = Request.UserLanguages;
                HomeModel modeloHome = new HomeModel(lng[0]);
                List<LlaveIdioma> traducciones;
                try{
                    traducciones = modeloHome.getTraduccion();
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
    }
}
