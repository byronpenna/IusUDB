using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
// librerias externas
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
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                        ViewBag.personas = personas;
                        ViewBag.titleModulo = "Gestion de personas";
                        ViewBag.usuario = usuarioSession;
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
        #region "privadas"
            private Persona getPersonaFromForm(Dictionary<object,object> frm){ 
                Persona persona = new Persona(Convert.ToInt32(frm["txtHdIdPersona"].ToString()), frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), Convert.ToDateTime(frm["dtFechaNacimiento"].ToString()));
                return persona;
            }
            private List<Persona> getPersonaFromForm(List<Dictionary<object,object>> frms){
                List<Persona> personas = new List<Persona>();
                Persona persona;
                foreach (Dictionary<object, object> frm in frms)
                {
                    persona = this.getPersonaFromForm(frm);
                    personas.Add(persona);
                }
                return personas;
            }
        #endregion
        #region "ajax action"
            #region "gets"
                [HttpPost]
                public ActionResult getJSONPersonas()
            {
                List<Persona> personas = this._model.getPersonas();
                return Json(personas);
            }
            #endregion
            #region "acciones"
            [HttpPost]
                public ActionResult actualizarTodo()
                {
                    List<Dictionary<object, object>> frm;
                    Dictionary<object, object> respuesta = null;
                    frm = this.getListAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null)
                    {
                        List<Persona> personasActualizar = this.getPersonaFromForm(frm);
                        respuesta = this._model.actualizarPersona(personasActualizar, usuarioSession._idUsuario, this._idPagina);

                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
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
                            Persona aAgregar = new Persona(frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), /*Convert.ToDateTime(frm["dtFechaNacimiento"].ToString())*/ DateTime.Parse(frm["dtFechaNacimiento"].ToString()));
                            Dictionary<object, object> respuestaPeticion = this._model.sp_hm_agregarPersona(aAgregar, usuarioSession._idUsuario, this._idPagina);
                            //Persona persona = this._model.sp_hm_agregarPersona(aAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", true);
                            respuesta.Add("persona",respuestaPeticion["persona"]);
                            respuesta.Add("permisos", respuestaPeticion["permisos"]);

                        }
                        catch (ErroresIUS x)
                        {
                            /*respuesta.Add("estado", false);
                            respuesta.Add("errorType", 1);
                            respuesta.Add("error", x);*/
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql,x._mostrar);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
                            /*respuesta.Add("estado", false);
                            respuesta.Add("errorType", 2);
                            respuesta.Add("error", x);*/
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }

                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult sp_hm_eliminarPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    bool estado;
                    if (frm != null && usuarioSession != null)
                    {
                        int idPersona = this.convertObjAjaxToInt(frm["txtHdIdPersona"]);
                        try{
                            estado = this._model.sp_hm_eliminarPersona(idPersona,usuarioSession._idUsuario,this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            if (estado)
                            {
                                respuesta.Add("estado",true);
                            }
                            else
                            {
                                ErroresIUS error = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                respuesta = this.errorTryControlador(3, error);
                            }
                        }catch(ErroresIUS x){
                            ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                            respuesta = this.errorTryControlador(1,error);
                        }catch(Exception x){
                            ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                            respuesta = this.errorTryControlador(2,error);
                        }
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
            #endregion
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
