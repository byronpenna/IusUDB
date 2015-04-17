using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class GestionPersonasController : PadreController
    {
        //
        // GET: /GestionPersonas/
        #region "propiedades"
            public GestionPersonaModel _model;
            private int _idPagina = (int)paginas.gestionPersonas;
        #endregion
        #region "url"
            public ActionResult Index()
            {
                // mandar a traer personas
                List<Persona> personas = this._model.getPersonas();
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.permiso = permisos;
                        ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.personas = personas;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed","Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
        #endregion
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
            [HttpPost]
            public ActionResult sp_hm_agregarPersona()
            {
                Dictionary<object, object> frm, respuesta = null;
                Usuario usuarioSession = this.getUsuarioSesion();
                frm = this.getAjaxFrm();
                if (usuarioSession != null && frm != null)
                {
                    respuesta = new Dictionary<object, object>();
                    try
                    {
                        Persona aAgregar = new Persona(frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), Convert.ToDateTime(frm["dtFechaNacimiento"].ToString()));
                        Persona persona = this._model.sp_hm_agregarPersona(aAgregar, usuarioSession._idUsuario, this._idPagina);
                        if (persona != null)
                        {
                            respuesta.Add("estado", true);
                            respuesta.Add("persona", persona);
                        }
                        else
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 3);
                            respuesta.Add("error", "Error al intentar ingresar persona");
                        }
                        
                    }
                    catch (ErroresIUS x)
                    {
                        respuesta.Add("estado", false);
                        respuesta.Add("errorType", 1);
                        respuesta.Add("error", x);
                    }
                    catch (Exception x)
                    {
                        respuesta.Add("estado", false);
                        respuesta.Add("errorType", 2);
                        respuesta.Add("error", x);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
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
