using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
// internas
    using IUSBack.Models.Page.Home.Acciones;
// externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.SECPU.Entidades;

namespace IUSBack.Controllers
{
    public class HomeController:PadreController
    {
        #region "propiedades"
            public HomeModel    homeModel;
            public string       _nombreClass = "HomeController";
            public int          _idPagina = (int)paginas.Home;
        #endregion
        #region "Constructores"
            public HomeController()
            {
                this.homeModel = new HomeModel();
            }
        #endregion
        #region "acciones"
            public ActionResult sp_secpu_reenviarCorreo()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    frm         = this.getAjaxFrm();
                    respuesta   = new Dictionary<object, object>();
                    Dictionary<object,object> retorno = this.homeModel.sp_secpu_reenviarCorreo(frm["txtEmail"].ToString());
                    CodigoVerificacion codigo   = (CodigoVerificacion)retorno["codigo"];
                    UsuarioPublico usuario      = (UsuarioPublico)retorno["usuarioPublico"];
                    if (codigo != null)
                    {
                        
                        this.enviarCorreo(usuario._email, codigo._numero, usuario._idUsuarioPublico);
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
            public void enviarCorreo(string email,int codigo,int idUsuario)
            {
                //usuarioAgregado._email,codigo._numero
                string ruta = Request.Url.AbsoluteUri;
                ruta = ruta.Substring(0, this.CustomIndexOf(ruta, '/', 3));
                MailMessage m = new MailMessage();
                m.To.Add(email);
                m.Subject = "Por favor confirmar cuenta IUS";
                m.Body = "para confirmar su cuenta por favor ingrese al siguiente enlace <br>" +
                    ruta + Url.Action("Verificar", "Home", new { id = codigo, id2 = idUsuario });
                m.IsBodyHtml = true;
                m.Priority = MailPriority.Normal;
                SmtpClient cliente = new SmtpClient();
                cliente.Send(m);
            }
            public ActionResult sp_secpu_addUsuario() {
                Dictionary<object, object> frm, respuesta = null;
                try
                { 
                    frm = this.getAjaxFrm();
                    DateTime fechaNac = DateTime.Parse(frm["txtFechaNac"].ToString());
                    UsuarioPublico usuarioAgregar = new UsuarioPublico(frm["txtNombre"].ToString(), frm["txtApellidos"].ToString(), frm["txtEmail"].ToString(), fechaNac, frm["txtPass"].ToString());
                    Dictionary<object,object> resp = this.homeModel.sp_secpu_addUsuario(usuarioAgregar);
                    UsuarioPublico usuarioAgregado = (UsuarioPublico)resp["usuarioAgregado"];
                    CodigoVerificacion codigo = (CodigoVerificacion)resp["codigoVerificacion"];
                    if (usuarioAgregado != null)
                    {
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("usuarioPublico", usuarioAgregado);
                        this.enviarCorreo(usuarioAgregado._email, codigo._numero, usuarioAgregado._idUsuarioPublico);
                        
                    }else{
                        ErroresIUS x = new ErroresIUS("Error inesperado",ErroresIUS.tipoError.generico,0);
                        throw x;
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
            public ActionResult sp_usu_changePass()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    frm = this.getAjaxFrm();
                    string ip       = Request.UserHostAddress;
                    Usuario usuario = this.homeModel.sp_usu_changePass(frm["txtPass"].ToString(), ip, (int)Session["idUsuario"], (int)paginas.Home);
                    if (usuario != null)
                    {
                        Session["usuario"] = usuario;
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        
                    }
                    else
                    {
                        ErroresIUS x = new ErroresIUS("Ocurrio un error no controlado");
                        throw x;
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

        #region "Vistas"
            public ActionResult Registro()
            {
                return View();
            }
            #region "parte del login"
                public ActionResult changePassword()
                {
                    ActionResult seguridadInicial = this.seguridadInicial(-1, -1);
                    Usuario usu = this.getUsuarioSesion();
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    try
                    {
                        //Session["neutroControl"] 
                        ViewBag.titleModulo = "Sistema administrativo IUS";
                        ViewBag.selectedMenu = 1;
                        ViewBag.menus = this.homeModel.sp_sec_getMenu(usu._idUsuario);
                    
                    }
                    catch (ErroresIUS x)
                    {
                        /*ErrorsController error = new ErrorsController();
                        var obj = error.redirectToError(x);
                        return RedirectToAction(obj["controlador"], obj["accion"]);*/
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, true, "Index-" + this._nombreClass, usu._idUsuario, this._idPagina);
                    }
                    catch (Exception x)
                    {
                        ErrorsController error = new ErrorsController();
                        return error.redirectToError(x, "Index-" + this._nombreClass, usu._idUsuario, this._idPagina);
                    }
                    return View("~/Views/Home/changePasswordi.cshtml");
                }

                /*
                    public ActionResult changePassword()
                    {
                        return View();
                    }
                 */
                public ActionResult Verificar(int id=-1,int id2=-1)
                {
                    /*
                     id: numero de verificacion 
                     id2: id del usuario
                     */
                    try
                    {
                        bool verificado = this.homeModel.sp_secpu_verificarCuenta(id, id2);
                        ViewBag.estado = verificado;
                    }
                    catch (ErroresIUS x)
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                    catch (Exception x)
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                    return View();
                }
            #endregion
            public ActionResult Index()
            {
                // estandarizar 
                /*if (Session["usuario"] != null)
                {*/
                ActionResult    seguridadInicial    = this.seguridadInicial(-1,-1);
                Usuario         usu                 = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    //Session["neutroControl"] 
                    ViewBag.titleModulo     = "Sistema administrativo IUS";
                    ViewBag.selectedMenu    = 1;
                    
                    ViewBag.usuario = usu;
                    //ViewBag.eventosHoy      = this.homeModel.sp_adminfe_front_getTodayEvents("127.0.0.1",1);
                    ViewBag.menus                       = this.homeModel.sp_sec_getMenu(usu._idUsuario);
                    
                    Dictionary<object, object> respuesta = this.homeModel.sp_sec_getSubmenu(-1, usu._idUsuario, 1);
                    ViewBag.subMenus        = respuesta["submenus"];
                    //ViewBag.cnEventos       = this.homeModel.sp_adminfe_countTodayEvents(this._idPagina, usu._idUsuario);
                    ViewBag.selectedMenu    = 1;
                    return View("~/Views/Home/Indexi.cshtml");
                }
                catch (ErroresIUS x)
                {
                    /*ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    return RedirectToAction(obj["controlador"], obj["accion"]);*/
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usu._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usu._idUsuario, this._idPagina);
                }
                /*}
                else
                {
                    return RedirectToAction("index", "login");
                }*/
            }
            public ActionResult ControlesNav(int id)
            {
                string url = "";
                try
                {
                    
                    Session["flagNav"] = true;
                    //Session["neutroControl"] = Request.Url.AbsoluteUri;
                    string adelante = Session["fowardControl"].ToString();
                    string neutro = Session["neutroControl"].ToString();
                    string atras = Session["backControl"].ToString();
                    if (id == 0)
                    { // atras 
                        url = Session["backControl"].ToString();
                        if (Session["fowardControl"] != Session["neutroControl"])
                        {
                            Session["fowardControl"] = Session["neutroControl"];
                        }
                    }
                    else
                    {
                        url = Session["neutroControl"].ToString();
                        /*
                        if (Session["backControl"] != Session["neutroControl"])
                        {
                            Session["backControl"] = Session["neutroControl"];
                        }*/
                    }
                    if (url == "")
                    {
                        url = neutro;
                    }

                }
                catch (ErroresIUS)
                {
                    url = Request.UrlReferrer.ToString();
                }
                catch (Exception)
                {
                    url = Request.UrlReferrer.ToString();
                }
                
                return Redirect(url);
            }
        #endregion
        
    }
}
