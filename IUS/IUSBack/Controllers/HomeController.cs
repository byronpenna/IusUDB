using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// propias
    using IUSBack.Models.Page.Home.Acciones;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class HomeController:PadreController
    {
        #region "propiedades"
            public HomeModel homeModel;
        #endregion
        #region "Constructores"
            public HomeController()
            {
                this.homeModel = new HomeModel();
            }
        #endregion
        #region
            
        #endregion

        #region "Vistas"

        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                try
                {
                    ViewBag.titleModulo = "Sistema administrativo IUS";
                    Usuario usu = (Usuario)Session["usuario"];
                    ViewBag.usuario = usu;
                    ViewBag.subMenus = this.homeModel.getMenuUsuario(usu._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    return RedirectToAction(obj["controlador"], obj["accion"]);
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "login");
                }
            }
            else
            {
                return RedirectToAction("index", "login");
            }
        }
        #endregion
        
    }
}
