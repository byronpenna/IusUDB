using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.ConfiguracionWebsite.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class ConfiguracionWebsiteController : PadreController
    {
        //
        // GET: /ConfiguracionWebsite/
        #region "propiedades"
            public ConfiguracionWebsiteModel _model;
            private int _idPagina = (int)paginas.configuracionFront;
        #endregion
        #region "url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.permiso = permisos;
                        ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                    
                }
                else
                {
                    return RedirectToAction("index", "login");
                }

            }
        #endregion
        #region "constructores"
            public ConfiguracionWebsiteController()
            {
                this._model = new ConfiguracionWebsiteModel();
            }
        #endregion

    }
}
