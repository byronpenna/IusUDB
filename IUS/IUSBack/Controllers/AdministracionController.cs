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
                                respuesta = this.errorTryControlador(1,x);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS errorIUS = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico,x.HResult);
                                respuesta = this.errorTryControlador(2,errorIUS);
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
                
                #endregion
                #region "gets"
                    // de momento se traen los eventos solo de usuario pero se deben de traer todos
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
