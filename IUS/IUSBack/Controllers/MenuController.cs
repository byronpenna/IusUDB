using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Menu.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    
namespace IUSBack.Controllers
{
    public class MenuController : PadreController
    {
        #region "constructores"
            public MenuController()
            {
                this._model = new MenuModel();
            }
        #endregion
        #region "propiedades"
        //public int _idPagina =
            public MenuModel _model;
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                try
                {
                    Usuario usuarioSession  = this.getUsuarioSesion();
                    ViewBag.titleModulo     = "Detalle menu";
                    //ViewBag.subMenus        = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.submenuss = this._model.sp_sec_getSubmenu(id, usuarioSession._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }

            }
        #endregion
    }
}
