using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Controllers
{
    public class AdministracionController : PadreController
    {

        #region "propiedades"
            private int _idPaginaEventos = (int)paginas.Eventos;
            private int _idPaginaNoticias = (int)paginas.Noticias;
            private AdministracionModel _model;
            
        #endregion
        #region "url"
            public ActionResult Eventos()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPaginaEventos);
                    if (permisos != null && permisos._ver)
                    {
                        List<Evento> eventos = this._model.sp_adminfe_eventosPropios(usuarioSession._idUsuario, this._idPaginaEventos);
                        ViewBag.eventos     = eventos;
                        ViewBag.permiso     = permisos;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
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
            public ActionResult Noticias()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPaginaNoticias);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.permiso     = permisos;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
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
        #region "acciones"
            #region "Eventos"
                #region "acciones"
                    #region "agregar"
                        public ActionResult sp_adminfe_agregarPermisoUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            if (frm != null && usuarioSession != null)
                            {
                                try
                                {
                                    int[] idPermisos = this.convertArrAjaxToInt((object[])frm["idPermisos"]);
                                    int idUsuarioEvento = this.convertObjAjaxToInt((object)frm["idUsuarioEvento"]);
                                    Dictionary<string, object> respuestaModel = this._model.sp_adminfe_agregarPermisoUsuarioEvento(idPermisos, idUsuarioEvento, usuarioSession._idUsuario, this._idPaginaEventos);
                                    List<PermisoUsuarioEvento> PermisosUsuariosEventos = (List<PermisoUsuarioEvento>)respuestaModel["permisosUsuariosEventos"];
                                    List<PermisoEvento> permisosFaltantes = this._model.sp_adminfe_getPermisosFaltantesEvento(idUsuarioEvento, usuarioSession._idUsuario, this._idPaginaEventos);
                                    respuesta = new Dictionary<object, object>();
                                    bool estado = (bool)respuestaModel["estadoGeneral"];
                                    if (estado)
                                    {
                                        respuesta.Add("estado", true);
                                        respuesta.Add("estadoIndividual", (bool)respuestaModel["estadoIndividual"]);
                                        respuesta.Add("PermisosUsuariosEventos", PermisosUsuariosEventos);
                                        respuesta.Add("permisosFaltantes", permisosFaltantes);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                        respuesta = this.errorTryControlador(3, x);
                                    }
                                    
                                }
                                catch (ErroresIUS x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
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
                        public ActionResult sp_adminfe_publicarEventoWebsite()
                        {
                            Dictionary<object, object> frm, respuesta = null;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            EventoWebsite eventoPublicado;
                            if (frm != null && usuarioSession != null)
                            {
                                try
                                {
                                    Evento eventoAgregar = new Evento(this.convertObjAjaxToInt(frm["txtHdIdEvento"]));
                                    eventoPublicado = this._model.sp_adminfe_publicarEventoWebsite(eventoAgregar, usuarioSession._idUsuario, this._idPaginaEventos);
                                    if (eventoPublicado != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("eventoPublicado", eventoPublicado);
                                    }
                                    else
                                    {
                                        respuesta = this.errorTryControlador(3, "Ocurrio un error inesperado");
                                    }
                                }
                                catch (ErroresIUS x)
                                {
                                    respuesta = this.errorTryControlador(1, x);
                                }
                                catch (Exception x)
                                {
                                    ErroresIUS errorIUS = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                    respuesta = this.errorTryControlador(2, errorIUS);
                                }
                            }
                            else
                            {

                                respuesta = this.errorEnvioFrmJSON();
                            }

                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_crearEvento()
                        {
                            Dictionary<object, object> frm,respuesta;
                            Evento eventoAgregado;
                            try
                            {
                                Usuario usuarioSession = this.getUsuarioSesion();
                                frm = this.getAjaxFrm();
                                if (usuarioSession != null && usuarioSession != null)
                                {
                                    respuesta               = new Dictionary<object, object>();
                                    DateTime fechaInicio    = this.convertObjAjaxToDateTime(frm["txtFechaInicio"].ToString(),frm["txtHoraInicio"].ToString());
                                    DateTime fechaFin       = this.convertObjAjaxToDateTime(frm["txtFechaFin"].ToString(), frm["txtHoraFin"].ToString());
                                    Evento eventoAgregar    = new Evento(frm["txtEvento"].ToString(), fechaInicio, fechaFin, usuarioSession, frm["txtAreaDescripcion"].ToString());
                                    eventoAgregado          = this._model.sp_adminfe_crearEvento(eventoAgregar, usuarioSession._idUsuario, this._idPaginaEventos);
                                    if (eventoAgregado != null)
                                    {
                                        respuesta.Add("estado", true);
                                        respuesta.Add("evento", eventoAgregado);
                                    }
                                    else
                                    {
                                        respuesta = this.errorTryControlador(3, "Ocurrio un error no controlado");
                                    }
                                }
                                else
                                {
                                    respuesta = this.errorEnvioFrmJSON();
                                }
                            }
                            catch (ErroresIUS x)
                            {
                                respuesta = this.errorTryControlador(1, x);
                            }
                            catch (Exception x)
                            {
                                respuesta = this.errorTryControlador(2, x);
                            }
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_compartirEventoUsuario()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            if (frm != null && usuarioSession != null)
                            {
                                try
                                {
                                    Evento evento = new Evento(this.convertObjAjaxToInt(frm["idEvento"]));
                                    Usuario usuario = new Usuario(this.convertObjAjaxToInt(frm["cbUsuarioCompartir"]));
                                    UsuarioEvento agregar = new UsuarioEvento(evento, usuario);
                                    UsuarioEvento usuarioAgregado = this._model.sp_adminfe_compartirEventoUsuario(agregar, usuarioSession._idUsuario, this._idPaginaEventos);
                                    List<Usuario> usuariosFaltantes = this._model.sp_adminfe_getUsuariosFaltantesEvento(usuarioAgregado._evento._idEvento, usuarioSession._idUsuario, this._idPaginaEventos);
                                    if (usuarioAgregado != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("usuarioEventoAgregado", usuarioAgregado);
                                        respuesta.Add("usuariosFaltantes", usuariosFaltantes);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Ocurrio un error inesperado", ErroresIUS.tipoError.generico, 0);
                                        respuesta = this.errorTryControlador(3, x);
                                    }
                                }
                                catch (ErroresIUS x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                                    respuesta = this.errorTryControlador(1, x);
                                }
                                catch (Exception x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                    respuesta = this.errorTryControlador(2, x);
                                }
                            }
                            else
                            {
                                respuesta = this.errorEnvioFrmJSON();
                            }
                            return Json(respuesta);
                        }
                    #endregion
                    #region "eliminar"
                        public ActionResult sp_adminfe_eliminarPermisoUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            if (frm != null && usuarioSession != null)
                            {
                                try
                                {
                                    bool estado = this._model.sp_adminfe_eliminarPermisoUsuarioEvento(this.convertObjAjaxToInt((object)frm["txtHdIdPermisoUsuarioEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                                    if (estado)
                                    {
                                        List<PermisoEvento> permisosFaltantes = this._model.sp_adminfe_getPermisosFaltantesEvento(this.convertObjAjaxToInt((object)frm["txtHdIdUsuarioEvento"]),usuarioSession._idUsuario,this._idPaginaEventos);
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", estado);
                                        respuesta.Add("permisosFaltantes", permisosFaltantes);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                        respuesta = this.errorTryControlador(3, x);
                                    }
                                }
                                catch (ErroresIUS x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                                    respuesta = errorTryControlador(1, error);
                                }
                                catch (Exception x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                                    respuesta = errorTryControlador(2, error);
                                }
                            }
                            else
                            {
                                respuesta = this.errorEnvioFrmJSON();
                            }
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_editarEventos()
                        {
                            Dictionary<object, object> frm, respuesta;
                            Usuario usuarioSession = this.getUsuarioSesion();
                            frm = this.getAjaxFrm(); Evento eventoEditado;
                            if (usuarioSession != null && frm != null)
                            {
                                try
                                {
                                    DateTime fechaInicio = this.convertObjAjaxToDateTime(frm["txtFechaInicio"].ToString(), frm["txtHoraInicio"].ToString());
                                    DateTime fechaFin = this.convertObjAjaxToDateTime(frm["txtFechaFin"].ToString(), frm["txtHoraFin"].ToString());
                                    Evento eventoEditar = new Evento(this.convertObjAjaxToInt(frm["txtHdIdEvento"]), frm["txtEvento2"].ToString(), fechaInicio, fechaFin, usuarioSession, frm["txtAreaDescripcion"].ToString());
                                    eventoEditado = this._model.sp_adminfe_editarEventos(eventoEditar, usuarioSession._idUsuario, this._idPaginaEventos);
                                    if (eventoEditado != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("eventoEditado", eventoEditado);
                                    }
                                    else
                                    {
                                        ErroresIUS errorIus = new ErroresIUS("Ocurrio un error no controlado", ErroresIUS.tipoError.generico, -1);
                                        respuesta = this.errorTryControlador(3, "Ocurrio un error no controlado");
                                    }
                                }
                                catch (ErroresIUS x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.HResult, x._errorSql);
                                    respuesta = this.errorTryControlador(1, error);
                                }
                                catch (Exception x)
                                {
                                    ErroresIUS errorIus = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult, "");
                                    respuesta = this.errorTryControlador(2, errorIus);
                                }
                            }
                            else
                            {
                                respuesta = this.errorEnvioFrmJSON();
                            }
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_removeUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            if (frm != null && usuarioSession != null)
                            {
                                try
                                {
                                    bool resultadoDelete = this._model.sp_adminfe_removeUsuarioEvento(this.convertObjAjaxToInt(frm["txtHdIdUsuarioEvento"]), usuarioSession._idUsuario);
                                    if (resultadoDelete)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        List<Usuario> usuarios = this._model.sp_adminfe_getUsuariosFaltantesEvento(this.convertObjAjaxToInt((object)frm["idEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                                        respuesta.Add("estado", resultadoDelete);
                                        respuesta.Add("usuariosFaltantes", usuarios);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                        respuesta = this.errorTryControlador(3, x);
                                    }
                                }
                                catch (ErroresIUS x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                                    respuesta = this.errorTryControlador(1, x);
                                }
                                catch (Exception x)
                                {
                                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.sql, x.HResult);
                                    respuesta = this.errorTryControlador(2, x);
                                }
                            }
                            else
                            {
                                respuesta = this.errorEnvioFrmJSON();
                            }
                            return Json(respuesta);
                        }
                    #endregion
                    public ActionResult sp_adminfe_quitarEventoWebsite()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        Usuario usuarioSesion = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        EventoWebsite eventoWebsite;
                        if (usuarioSesion != null && frm != null)
                        {
                            try
                            {
                                eventoWebsite = this._model.sp_adminfe_quitarEventoWebsite(this.convertObjAjaxToInt(frm["txtHdIdEvento"]), frm["txtAreaMotivoQuitar"].ToString(), usuarioSesion._idUsuario, this._idPaginaEventos);
                                respuesta = new Dictionary<object, object>();
                                if (eventoWebsite != null)
                                {
                                    respuesta.Add("estado", true);
                                    respuesta.Add("eventoWebsite", eventoWebsite);
                                    
                                }
                                else
                                {
                                    respuesta = this.errorTryControlador(3, "Ocurrio un error inesperado");
                                }
                            }
                            catch (ErroresIUS x)
                            {
                                respuesta = this.errorTryControlador(1, x);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS errorIus = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                                respuesta = this.errorTryControlador(2, errorIus);
                            }
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                #endregion
                #region "gets"
                    
                    public ActionResult sp_adminfe_getPermisosUsuarioEvento()
                    {
                        Dictionary<object, object> frm, respuesta=null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        if (usuarioSession != null && frm != null)
                        {
                            Dictionary<object, object> permisos = this._model.sp_adminfe_getPermisosUsuarioEvento(this.convertObjAjaxToInt(frm["idUsuarioEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                            List<PermisoEvento> permisosFaltantes = (List<PermisoEvento>)permisos["permisosFaltantes"];
                            List<PermisoUsuarioEvento> permisosActuales = (List<PermisoUsuarioEvento>)permisos["permisosActuales"];
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("permisosFaltantes", permisosFaltantes);
                            respuesta.Add("permisosActuales", permisosActuales);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_adminfe_loadCompartirEventos()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        if (frm != null && usuarioSession != null)
                        {
                            try
                            {
                                Dictionary<string,object> usuarios = this._model.sp_adminfe_loadCompartirEventos(this.convertObjAjaxToInt(frm["idEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                                List<UsuarioEvento> usuariosCompartidos = (List<UsuarioEvento>)usuarios["usuariosCompartido"];
                                List<Usuario> usuariosNoCompartidos = (List<Usuario>)usuarios["usuariosNoCompartido"];
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("usuariosCompartidos", usuariosCompartidos);
                                respuesta.Add("usuariosNoCompartidos", usuariosNoCompartidos);
                            }
                            catch (ErroresIUS x)
                            {
                                ErroresIUS errorIus = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                                respuesta = this.errorTryControlador(1, x);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS errorIus = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                respuesta = this.errorTryControlador(2, x);
                            }                            
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_adminfe_getEventosPrincipales()
                    {
                        Dictionary<object, object> respuesta = new Dictionary<object, object>();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        List<Evento> eventos = this._model.sp_adminfe_eventosPropios(usuarioSession._idUsuario, this._idPaginaEventos);
                        respuesta.Add("estado", true);
                        respuesta.Add("eventos", eventos);
                        return Json(respuesta);
                    }
                #endregion
            #endregion
            #region "Noticias"

            #endregion
        #endregion
        #region "constructores"
            public AdministracionController()
            {
                this._model = new AdministracionModel();
            }
        #endregion
    }
}
