using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// modelos
    using IUSBack.Models.Page.Login.Acciones;
    using IUSBack.Models.Page.Login.Forms;
// libs externas
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class LoginController : Controller
    {
        #region "propiedades"
            public LoginModel modelLogin;
        #endregion
        #region "Actions"
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Index(User usu)
            {
                bool login = false;
                try
                {
                    login = this.modelLogin.login(usu);
                }
                catch (ErroresIUS x) {
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                if (usu.usuario != null && usu.pass != null && login)
                {
                    Session["usuario"] = this.modelLogin.getUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (usu.usuario != null && usu.pass != null)
                    {
                        ViewBag.errorLogin = "Usuario y/o contraseña incorrecta";
                    }
                    return View(usu);
                }
            }
            
        #endregion
        #region "Views"
            public ActionResult Index()
            {
                return View();
            }
            public ActionResult LogOut(){
                Session.Contents.RemoveAll();
                return RedirectToAction("Index", "Login");
            }
        #endregion
        #region "Constructores"
            public LoginController()
            {
                
                modelLogin = new LoginModel();
            }
            
        #endregion



    }
}
