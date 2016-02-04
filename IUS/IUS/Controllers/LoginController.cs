using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// internas 
    using System.Net.Mail;
// modelos
    using IUS.Models.page.Login.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.SECPU.Entidades;
namespace IUS.Controllers
{
    public class LoginController : PadreController
    {
        //
        // GET: /Login/
        #region "propiedades"
            public int idPagina = (int)paginas.login;
            private LoginModel _model;
        #endregion
        #region "URL"
            public ActionResult cambiarPass(int id,int id2)
            {
                /*
                 id: representa al id del usuario
                 id2: representa a el numero aleatorio
                 */
                ViewBag.accion              = 2;
                ViewBag.idUsuarioPublico    = id;
                ViewBag.numVal = id2;
                return View("~/Views/Login/index.cshtml");
            }
            public ActionResult LostPass()
            {
                ViewBag.accion = 1;
                return View();

            }
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    return View();
                }
                catch (ErroresIUS x)
                {
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
                
            }
        #endregion
        #region "ajax"
            #region "cambio de contraseña"
                public ActionResult sp_secpu_cambiarPassPublico()
                {
                    Dictionary<object, object> frm, respuesta;
                    frm = this.getAjaxFrm();
                    if (frm != null)
                    {
                        try
                        {
                            string ip = Request.UserHostAddress;
                            bool cambio = this._model.sp_secpu_cambiarPassPublico(this.convertObjAjaxToInt(frm["txtHdIdUsuario"]), this.convertObjAjaxToInt(frm["txtHdNumVal"]), frm["txtPass"].ToString(),ip,1); // por el momento la pagina sera 1

                        }catch (ErroresIUS x)
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
                public ActionResult sp_secpu_solicitarCambio()
                {
                    Dictionary<object, object> frm, respuesta;
                    frm = this.getAjaxFrm();
                    if (frm != null)
                    {
                        try
                        {
                            string email = frm["txtEmail"].ToString();
                            ValidadorPassPublico validador = this._model.sp_secpu_solicitarCambio(email);
                            respuesta = new Dictionary<object, object>();
                            MailMessage m = new MailMessage();
                            m.To.Add(email);
                            m.Body = "Se a solicitado un cambio de contraseña por favor ingresar al siguiente enlace: ";
                            m.Subject = "Solicitud cambio de contraseña";
                            m.IsBodyHtml = true;
                            m.Priority = MailPriority.Normal;
                            SmtpClient cliente = new SmtpClient();
                            cliente.Send(m);
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
            #region "login"
                public ActionResult sp_adminfe_front_getLogin()
                {
                    Dictionary<object, object> frm, respuesta;
                    frm = this.getAjaxFrm();
                    if (frm != null)
                    {
                        try
                        {
                            string lang = this.getUserLang();
                            string ip = Request.UserHostAddress;
                            respuesta = new Dictionary<object, object>();
                            UsuarioPublico usuario = this._model.sp_adminfe_front_getLogin(frm["txtEmail"].ToString(), frm["txtPass"].ToString(), ip, this.idPagina);
                            Session["usuarioPublico"] = usuario;
                        
                            respuesta.Add("estado", true);
                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql,x._mostrar);
                        
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
                public ActionResult LogOut()
                {
                    Dictionary<object, object> respuesta = new Dictionary<object, object>();
                    try{
                        Session.Contents.RemoveAll();
                        respuesta.Add("estado", true);
                    }
                    catch (Exception )
                    {

                    }

                    return Json(respuesta);
                }
            #endregion
        #endregion
        #region "Constructores"
            public LoginController()
            {
                this._model = new LoginModel();
            }
        #endregion
    }
}
