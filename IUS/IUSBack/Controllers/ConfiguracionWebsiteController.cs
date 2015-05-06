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
    using IUSLibs.ADMINFE.Entidades;
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
                        List<RedSocial> redesSociales = null;
                        Configuracion config = null;
                        List<Valor> valores = null;
                        try
                        {
                            Dictionary<object, object> dic = this._model.sp_adminfe_getConfiguraciones(usuarioSession._idUsuario, this._idPagina);
                            
                            if (dic != null)
                            {
                                config = (Configuracion)dic["configuracion"];
                                redesSociales = (List<RedSocial>)dic["redesSociales"];
                                valores = (List<Valor>)dic["valores"];
                            }
                            
                        }
                        catch (ErroresIUS)
                        {

                            return RedirectToAction("Unhandled", "Errors");
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("Unhandled", "Errors");
                        }
                        ViewBag.redesSociales = redesSociales;
                        ViewBag.config = config;
                        ViewBag.valores = valores;
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
