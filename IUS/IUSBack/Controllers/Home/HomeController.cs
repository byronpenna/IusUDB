using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            public HomeModel homeModel;
            
        #endregion
        #region "Constructores"
            public HomeController()
            {
                this.homeModel = new HomeModel();
            }
        #endregion
        #region "acciones"
            public ActionResult sp_secpu_addUsuario() {
                Dictionary<object, object> frm, respuesta = null;
                try
                { 
                    frm = this.getAjaxFrm();
                    DateTime fechaNac = DateTime.Parse(frm["txtFechaNac"].ToString());
                    UsuarioPublico usuarioAgregar = new UsuarioPublico(frm["txtNombre"].ToString(), frm["txtApellidos"].ToString(), frm["txtEmail"].ToString(), fechaNac, frm["txtPass"].ToString());
                    UsuarioPublico usuarioAgregado = this.homeModel.sp_secpu_addUsuario(usuarioAgregar);
                    if (usuarioAgregado != null)
                    {
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("usuarioPublico", usuarioAgregado);
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
                    return View();
                }
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
                if (Session["usuario"] != null)
                {
                    try
                    {
                        ViewBag.titleModulo = "Sistema administrativo IUS";
                        ViewBag.selectedMenu = 1;
                        Usuario usu = (Usuario)Session["usuario"];
                        ViewBag.usuario = usu;
                        ViewBag.eventosHoy = this.homeModel.sp_adminfe_front_getTodayEvents("127.0.0.1",1);
                        ViewBag.menus = this.homeModel.sp_sec_getMenu(usu._idUsuario);
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
