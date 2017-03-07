using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
// otras 
    using System.Net.Mail;
// modelos
    using IUSBack.Models.Page.Login.Acciones;
    using IUSBack.Models.Page.Login.Forms;
// libs externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class LoginController : PadreController
    {
        #region "propiedades"
            public LoginModel modelLogin;
        #endregion
        #region "Actions"
            public ActionResult Loguear()
            {
                Dictionary<object, object> respuesta;
                ActionResult retorno = null;
                string self = "";
                try
                {

                    Usuario usu     = new Usuario();
                    usu._usuario    = Request.Form["txtUsuario"].ToString();
                    usu._pass       = Request.Form["txtPass"].ToString();
                    self            = Request.Form["txtHdSelf"].ToString();
                    respuesta       = this.modelLogin.loguearAjax(usu);
                    if ((bool)respuesta["login"])
                    {
                        Usuario usuario = (Usuario)respuesta["usuario"];
                        FormsAuthentication.SetAuthCookie(usuario._idUsuario.ToString(), false);
                        if ((bool)respuesta["changePass"])
                        {
                            Session["idUsuario"] = usuario._idUsuario;

                            retorno = RedirectToAction("changePasswordA", "Home");
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
                        if (self != "")
                        {
                            return Redirect(self+"/"+x.Message);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    /*ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true);*/
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return retorno;
            }
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
                        FormsAuthentication.SetAuthCookie(usuario._idUsuario.ToString(), false);
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
                catch (Exception)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return retorno;
            }
            
        #endregion
        #region "funciones ajax"
            public ActionResult sp_usu_cambiarPassUsuario()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(frm);
                    if (respuesta == null)
                    {
                        bool cambio = this.modelLogin.sp_usu_cambiarPassUsuario(this.convertObjAjaxToInt(frm["txtHdIdUsuario"]), frm["txtPass"].ToString(), this.convertObjAjaxToInt(frm["txtHdIdUsuario"]), (int)paginas.forgetPass);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", cambio);
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
            public ActionResult sp_usu_solicitarCambioPass()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(frm);
                    if (respuesta == null)
                    {
                        respuesta = this.modelLogin.sp_usu_solicitarCambioPass(frm["usuario"].ToString(), 4);
                        ValidadorPass val = (ValidadorPass)respuesta["validadorPass"];
                        MailMessage m = new MailMessage();
                        m.To.Add(respuesta["email"].ToString());
                        m.Subject   =   "Solicitud de cambio en contraseña";
                        string ruta =   Request.Url.AbsoluteUri;
                        ruta        =   ruta.Substring(0, this.CustomIndexOf(ruta, '/', 3));
                        m.Body = "Se ha solicitado un cambio de contraseña, si usted lo hizo por favor haga clic en el siguiente enlace<br>" +
                                        ruta + Url.Action("ForgetPass", "Login") + "/"+val._codigo+"/"+val._usuario._idUsuario; //+ val._usuario._idUsuario;
                        m.IsBodyHtml = true;
                        m.Priority = MailPriority.Normal;
                        SmtpClient cliente = new SmtpClient();
                        cliente.Send(m);
                        respuesta.Add("estado", true);
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
        #region "Views"
            public ActionResult ForgetPass(int id=-1,int id2=-1)
            {
                /*
                 id:    codigo del usuario
                 id2:   idUsuario
                 */
                try
                    {
                        Usuario usuario = this.modelLogin.sp_usu_getUsuarioById(id2);    
                        if (id != -1 && id2 != -1 && usuario != null)
                        {
                            ViewBag.codigo = id;
                            ViewBag.usuario = usuario;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }
                        
                    }
                catch (ErroresIUS)
                {
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Login");
                }
                
                
            }
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession == null)
                {
                    return View();
                }
                else
                {
                    Session["backControl"] = "";
                    Session["fowardControl"] = "";
                    Session["neutroControl"] = "";
                    Session["flagNav"] = false;
                    return RedirectToAction("Index", "Home");
                }
                
            }
            public ActionResult LogOut(){
                Session.Contents.RemoveAll();
                return Redirect(IUSLibs.GENERALS.Rutas.IUS+"Login/Index");
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
