using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// modelos
    using IUSBack.Models.Page.Login.Acciones;
    using IUSBack.Models.Page.Login.Forms;
// libs externas
    using IUSLibs.SEC.Entidades;
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
                Dictionary<object, object> respuesta;
                ActionResult retorno = null;
                try
                {
                    respuesta = this.modelLogin.logueo(usu);
                    if ((bool)respuesta["login"])
                    {
                        Usuario usuario = (Usuario)respuesta["usuario"];
                        if ((bool)respuesta["changePass"])
                        {
                            Session["idUsuario"]        = usuario._idUsuario;
                            
                            retorno =  RedirectToAction("changePassword", "Home");
                        }
                        else
                        {
                            Session["usuario"] = usuario;
                            retorno = RedirectToAction("Index", "Home");
                        }
                        // nav 
                        Session["backControl"] = "";
                        Session["fowardControl"] = "";
                        Session["neutroControl"] = "";
                        Session["flagNav"] = false;
                    }
                    
                }
                catch (ErroresIUS x)
                {
                    if (x._mostrar)
                    {
                        ViewBag.errorLogin = x.Message;
                        return View(usu);
                    }
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return retorno;
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
