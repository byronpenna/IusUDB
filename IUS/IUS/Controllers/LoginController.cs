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
            public ActionResult cambiarPass(int id=-1,int id2=-1)
            {
                /*
                 id: representa al id del usuario
                 id2: representa a el numero aleatorio
                 */
                if (id != -1 && id2 != -1)
                {
                    UsuarioPublico usu = this._model.sp_secpu_getUsuarioPublico(id);
                    if (usu != null)
                    {
                        ViewBag.accion = 2;
                        ViewBag.idUsuarioPublico = id;
                        ViewBag.correo = usu._email;
                        ViewBag.numVal = id2;
                        return View("~/Views/Login/LostPass.cshtml");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                
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
                    return View("~/Views/Login/Indexi.cshtml");
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
                            string lang = this._model.getStandarLang(this.getUserLang());
                            bool cambio = this._model.sp_secpu_cambiarPassPublico(this.convertObjAjaxToInt(frm["txtHdIdUsuario"]), this.convertObjAjaxToInt(frm["txtHdNumVal"]), frm["txtPass"].ToString(),ip,1,lang); // por el momento la pagina sera 1
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", cambio);
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
                            string ruta = Request.Url.AbsoluteUri;
                            //########## TMP #########
                            ruta = ruta.Substring(0, this.CustomIndexOf(ruta, '/', 3) );
                            
                            //###################
                            m.To.Add(email);
                            m.Body = "Se a solicitado un cambio de contraseña por favor ingresar al siguiente enlace: <br>" +
                                        ruta + Url.Action("cambiarPass", "Login", new { id=validador._usuarioPublico._idUsuarioPublico,id2=validador._codigo});
                            m.Subject = "Solicitud cambio de contraseña";
                            //cambiarPass(usuario,numeroAleatorio)
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
