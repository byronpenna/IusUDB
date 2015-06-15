using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// liberias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class RepositorioController : PadreController
    {
        #region "constructores"
            public RepositorioController()
            {
                this._model = new RepositorioModel();
            }
        #endregion
        #region "propiedades"
            public int _idPagina = (int)paginas.Repositorio;
            public RepositorioModel _model;
        #endregion 
        #region "url"
            public ActionResult Index()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> archivos = this._model.sp_repo_getRootFolder(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.titleModulo = "Repositorio Digital";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.permisos = permisos;
                    ViewBag.carpetas = archivos["carpetas"];
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                
            }
        #endregion
        #region "acciones"
            
        #endregion 

    }
}
