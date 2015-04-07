using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            private int _idPagina = (int)paginas.gestionPersonas;
            private JavaScriptSerializer _jss;
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
        [HttpPost]
        public ActionResult actualizarPersona()
        {
            string frmText = Request.Form["form"];
            Dictionary<Object, Object> frm, toReturn;
            if (frmText != null && Session["usuario"] != null)
            {
                frm = _jss.Deserialize<Dictionary<Object, Object>>(frmText);
                Usuario usuarioSession = (Usuario)Session["usuario"];
                Persona personaActualizar = new Persona(Convert.ToInt32(frm["txtHdIdPersona"].ToString()), frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), Convert.ToDateTime(frm["dtFechaNacimiento"].ToString()));
                toReturn = this._model.actualizarPersona(personaActualizar, usuarioSession._idUsuario, this._idPagina);
                
            }
            else
            {
                toReturn = this.errorEnvioFrmJSON();
            }
            return Json(toReturn);
        }
        #endregion
        #region "constructores"
            public GestionPersonasController()
            {
                this._model = new GestionPersonaModel();
                this._jss = new JavaScriptSerializer();
            }
        #endregion
        
    }
}
