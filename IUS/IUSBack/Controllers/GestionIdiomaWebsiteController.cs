using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionIdiomaWebsite.Acciones;    
// librerias externas 
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers
{
    public class GestionIdiomaWebsiteController : PadreController
    {
        //
        // GET: /GestionIdiomaWebsite/
        #region "propiedades"
            public int _idPagina = (int)paginas.gestionIdiomaWebsite;
            public GestionIdiomaWebsiteModel _model;
        #endregion
        #region "constructores"
            public GestionIdiomaWebsiteController()
            {
                this._model = new GestionIdiomaWebsiteModel();
            } 
        #endregion
        #region "URL"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
                return View();
            }
        #endregion
    }
}
