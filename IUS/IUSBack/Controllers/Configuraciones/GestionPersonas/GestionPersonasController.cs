using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
    using System.IO;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
    using System.Data.ProviderBase;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Entidades;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
            public ActionResult Extras(int id)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    
                    ViewBag.selectedMenu                    = 2; // menu seleccionado 
                    //Permiso permisos                        = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> data         = this._model.sp_rrhh_getInformacionPersonas(id, usuarioSession._idUsuario, this._idPagina);
                    InformacionPersona informarcionPersona  = (InformacionPersona)data["informacionPersona"];
                    
                    if (informarcionPersona != null && System.IO.File.Exists(informarcionPersona._fotoRuta))
                    {
                        informarcionPersona._tieneFoto = true;
                        //informarcionPersona._fotoRuta = informarcionPersona._fotoRuta.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
                        informarcionPersona._fotoRuta = this.getRelativePathFromAbsolute(informarcionPersona._fotoRuta);
                    }
                    ViewBag.paises                          = data["paises"];
                    ViewBag.estadosCiviles                  = data["estadosCiviles"];
                    ViewBag.informacionPersona              = informarcionPersona;

                    ViewBag.emails                  = data["emails"];
                    ViewBag.telefonos               = data["telefonos"];
                    ViewBag.persona                 = data["persona"];
                    ViewBag.personas                = data["personas"];
                    ViewBag.idPersona               = id;
                    // viewbag
                        ViewBag.titleModulo = "Información adicional personas";
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Extras-FormacionPersonasController", usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Extras-GestionPersonasController", usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult Index()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 2);
                Usuario usuarioSession = this.getUsuarioSesion();//*
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    List<Persona> personas = this._model.getPersonas();
                    // ----------------
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.personas = personas;
                    ViewBag.titleModulo = "Gestion de personas";
                    ViewBag.usuario = usuarioSession;
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-GestionPersonasController", usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-GestionPersonasController", usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult Detalle(int id=-1,int id2= 1) // este -1 es temporal 
            {
                /*
                 Para id2
                 * 1: Usuarios
                 * 2: Personas
                 */
                Usuario usuarioSesion = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 2);                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    //Dictionary<object,object> detalle = this._model.
                    Dictionary<object, object> detalle  = this._model.sp_rrhh_detallePesona(id, usuarioSesion._idUsuario, this._idPagina);
                    InformacionPersona informacion = (InformacionPersona)detalle["infoPersona"];
                    if (informacion != null)
                    {
                        if (informacion._fotoRuta != null && informacion._fotoRuta != "")
                        {
                            informacion._fotoRuta = this.getRelativePathFromAbsolute(informacion._fotoRuta);
                            informacion._tieneFoto = true;
                        }
                    }
                    else
                    {
                        informacion = new InformacionPersona();
                        
                    }
                    detalle["infoPersona"] = informacion;
                    ViewBag.idFrom = id2;
                    switch (id2)
                    {
                        case 1:
                            {
                                ViewBag.textoFrom = "Usuarios";
                                ViewBag.urlFrom = Url.Action("Index", "GestionUsuarios");
                                break;
                            }
                        case 2:
                            {
                                ViewBag.textoFrom = "Personas";
                                ViewBag.urlFrom = Url.Action("Index", "GestionPersonas");
                                break;
                            }
                    }
                    Dictionary<object, object> medios   = this._model.sp_rrhh_getMediosPersonas(id, usuarioSesion._idUsuario,this._idPagina);
                    ViewBag.titleModulo                 = "Detalle persona";
                    ViewBag.detalle                     = detalle;
                    ViewBag.medios                      = medios;
                    ViewBag.menus                       = this._model.sp_sec_getMenu(usuarioSesion._idUsuario);
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-FormacionPersonasController", usuarioSesion._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-FormacionPersonasController", usuarioSesion._idUsuario, this._idPagina);
                }
                return View();
            }
            public ActionResult FichaPdf(int id,int id2)
            {
                /*Stream stream = this._model.getFichaPdf(Path.Combine(Server.MapPath("~/Reportes"), "CrystalReport1.rpt"),id);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                return File(stream, "application/pdf");*/

                //ReportDocument rd = this._model.getFicha(Path.Combine(Server.MapPath("~/Reportes"), "CrystalReport1.rpt"), id,System.Web.HttpContext.Current.Response);

                this._model.getFicha(Path.Combine(Server.MapPath("~/Reportes"), "CrystalReport1.rpt"), id, System.Web.HttpContext.Current.Response,id2);
                return null;
                /*Stream retorno = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                retorno.Seek(0, SeekOrigin.Begin);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                return File(retorno, "application/pdf");*/
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
                public ActionResult actualizarTodo()// no se puede estandarizar para sesion por una forma rara de traer frm
                {
                    List<Dictionary<object, object>> frm;
                    Dictionary<object, object> respuesta = null;
                    frm = this.getListAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null){
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
                    Dictionary<object, object> frm, respuesta = null;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        try
                        {
                            //Usuario usuarioSession = (Usuario)Session["usuario"];
                            Persona personaActualizar = new Persona(Convert.ToInt32(frm["txtHdIdPersona"].ToString()), frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), Convert.ToDateTime(frm["dtFechaNacimiento"].ToString()));
                            Sexo sexo = new Sexo(this.convertObjAjaxToInt(frm["cbSexo"]));
                            personaActualizar._sexo = sexo;
                            Persona personaActualizada = this._model.actualizarPersona(personaActualizar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("persona", personaActualizada);
                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult sp_hm_agregarPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        try
                        {
                            Persona aAgregar = new Persona(frm["txtNombrePersona"].ToString(), frm["txtApellidoPersona"].ToString(), 
                            DateTime.Parse(frm["dtFechaNacimiento"].ToString()));
                            Sexo sexo = new Sexo(this.convertObjAjaxToInt(frm["cbSexo"]));
                            aAgregar._sexo = sexo;
                            Dictionary<object, object> respuestaPeticion = this._model.sp_hm_agregarPersona(aAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", true);
                            respuesta.Add("persona",respuestaPeticion["persona"]);
                            respuesta.Add("permisos", respuestaPeticion["permisos"]);

                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql,x._mostrar);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
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
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
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
                    return Json(respuesta);
                }
            #endregion
        #endregion
        #region "constructores"
            public GestionPersonasController()
            {
                this._model = new GestionPersonaModel();
            }
        #endregion
        
    }
}
