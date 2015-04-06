using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers
{
    public class GestionPersonasController : PadreController
    {
        //
        // GET: /GestionPersonas/
        #region "propiedades"
            public GestionPersonaModel _model;
            private int _idPagina = 4;
        #endregion
        public ActionResult Index()
        {
            // mandar a traer personas
            List<Persona> personas = this._model.getPersonas();
            Usuario usuarioSession = (Usuario)Session["usuario"];
            if (Session["usuario"] != null)
            {
                ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                ViewBag.personas = personas;
                return View();
            }
            else
            {
                return RedirectToAction("index", "login");
            }
        }
        #region "ajax action"
        [HttpPost]
        public ActionResult getJSONPersonas()
        {
            List<Persona> personas = this._model.getPersonas();
            return Json(personas);
        }
        #endregion
        public GestionPersonasController()
        {
            this._model = new GestionPersonaModel();
        }
    }
}
