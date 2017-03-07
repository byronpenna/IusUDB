using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// json 
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionUsuarios.Acciones;
    using IUSBack.Models.Page.GestionPersonas.acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class GestionUsuariosController : PadreController
    {
        //
        // GET: /GestionUsuarios/
        #region "Propiedades"
            private GestionUsuarioModel _model;
            private int                 _idPagina       = (int)paginas.usuarios;
            private string              _nombreClass    = "GestionUsuariosController";
        #endregion
        #region "constructores"
            public GestionUsuariosController()
            {
                this._model = new GestionUsuarioModel(this._idPagina);
            }
        #endregion
        #region "Resultados url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 2);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    List<Usuario> usuarios;
                    GestionPersonaModel modelPersona = new GestionPersonaModel();
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    List<Persona> personas = null;
                    try
                    {
                        ViewBag.titleModulo = "Gestion de usuarios";
                        ViewBag.usuario = usuarioSession;
                        usuarios = this._model.getUsuarios(usuarioSession._idUsuario);
                        personas = modelPersona.getPersonas(usuarioSession._idUsuario);
                    }
                    catch (ErroresIUS)
                    {
                        usuarios = null;
                    }
                    catch (Exception)
                    {
                        usuarios = null;
                    }
                    ViewBag.usuarios = usuarios;
                    ViewBag.personas = personas;
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                
            }
        #endregion
        #region "funciones privadas"
            private Usuario getUsuarioFromForm(Dictionary<Object,Object> frm)
            {
                Persona persona = new Persona(Convert.ToInt32((string)frm["cbPersona"]));
                Usuario usu = new Usuario(Convert.ToInt32((String)frm["txtHdIdUser"]), frm["txtEditUsuario"].ToString(), persona);
                return usu;
            }
            public List<Usuario> getUsuarioFromForm(List<Dictionary<Object,Object>> frms)
            {
                List<Usuario> usuarios = new List<Usuario>();
                Usuario usu;
                foreach (Dictionary<Object, Object> frm in frms)
                {
                    usu = this.getUsuarioFromForm(frm);
                    usuarios.Add(usu);
                }
                return usuarios;
            }
        #endregion
        #region "Resultados ajax"
            #region "acciones"
                #region "actualizar"
                    public ActionResult sp_sec_agregarUsuario()
                    {
                        Dictionary<object, object> frm, respuesta;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        Usuario usuarioAgregado,usuarioAgregar;
                        Persona persona; Permiso permiso;
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try
                            {
                                permiso = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario,this._idPagina);
                                persona = new Persona(this.convertObjAjaxToInt(frm["cbPersona"]));
                                usuarioAgregar = new Usuario(frm["txtEditUsuario"].ToString(), DateTime.Now, true,persona, frm["txtEditUsuario"].ToString()); // contraseña por defecto su nombre de usuario
                                usuarioAgregado = this._model.sp_sec_agregarUsuario(usuarioAgregar, usuarioSession._idUsuario, this._idPagina);
                                if (usuarioAgregado != null)
                                {
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                    respuesta.Add("usuarioAgregado", usuarioAgregado);
                                    respuesta.Add("permiso", permiso);
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Error inesperado", ErroresIUS.tipoError.generico, -1);
                                    respuesta = this.errorTryControlador(3, x);
                                }
                            }
                            catch (ErroresIUS x)
                            {
                                ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber,x._errorSql,x._mostrar);
                                respuesta = this.errorTryControlador(1, error);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS errorIus = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                respuesta = this.errorTryControlador(2, x);
                            }
                        }
                        return Json(respuesta);
                    }
                #endregion
                #region "Deshabilitar"
                    [HttpPost]
                    public ActionResult cambiarEstadoUsuario()
                    {
                        Usuario usuarioSession = (Usuario)Session["usuario"];
                        int idUsuario = Convert.ToInt32(Request.Form["usuarioId"]);
                        var resp = (Dictionary<Object,Object>)this._model.cambiarEstadoUsuario(idUsuario,usuarioSession._idUsuario);
                        return Json(resp);
                    }
                #endregion
                #region "actualizar"
                    [HttpPost]
                    public ActionResult actualizarUsuario()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        try
                        {
                            Usuario usuarioSession = this.getUsuarioSesion();
                            frm = this.getAjaxFrm();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                respuesta = this._model.actualizarUsuario(frm, usuarioSession._idUsuario);
                            }
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
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult sp_sec_actualizarUsuariosGeneral()
                    {
                        List<Dictionary<Object, Object>> frm;
                        Dictionary<Object, Object> respuesta = null;
                        frm = this.getListAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        if (usuarioSession != null && frm != null) // manejor error usuario D: 
                        {
                            List<Usuario> usuariosActualizar = this.getUsuarioFromForm(frm);
                            respuesta = this._model.actualizarUsuario(usuariosActualizar, usuarioSession._idUsuario,this._idPagina);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                #endregion
            #endregion
        #endregion

    }
}

