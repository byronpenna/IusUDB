using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionIdiomaWebsite.Acciones;    
// librerias externas 
    using IUSLibs.TRL.Entidades;
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
                    List<Idioma> idiomas = this._model.sp_tra_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                    List<Pagina> paginas = this._model.sp_trl_getAllPaginas(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    ViewBag.idiomas = idiomas;
                    ViewBag.paginas = paginas;

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
