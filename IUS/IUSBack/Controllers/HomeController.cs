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
            
        #endregion

        #region "Vistas"
            public ActionResult Registro()
            {
                return View();
            }
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
                        //ViewBag.menus = this.homeModel.sp_sec_getMenu(usu._idUsuario);
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
