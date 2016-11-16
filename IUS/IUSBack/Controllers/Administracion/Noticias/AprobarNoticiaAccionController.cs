using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas 
    using IUSBack.Models.General;
    using IUSBack.Models.Page.Administracion.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Controllers.Administracion.Noticias
{
    public class AprobarNoticiaAccionController : PadreController
    {
        //
        // GET: /AprobarNoticiaAccion/

        #region "propiedades"
            public AprobarNoticiasModel _model;
            private int _idPagina = (int)paginas.Noticias;
        #endregion
        #region "constructores"
            public AprobarNoticiaAccionController()
            {
                this._model = new AprobarNoticiasModel();
            }
        #endregion 
        #region "metodos"
            #region "get"
                public ActionResult sp_adminfe_aprobarNoticia_getNoticiasRevision()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        List<NotiEvento> noticiasEventos = this._model.sp_adminfe_aprobarNoticia_getNoticiasRevision(usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("noticiasEventos", noticiasEventos);

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
                public ActionResult sp_adminfe_aprobarnoticia_getNoticiasAprobar()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<NotiEvento> noticiasEventos = this._model.sp_adminfe_aprobarnoticia_getNoticiasAprobar(usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("noticiasEventos",noticiasEventos);

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
            #endregion
            #region "set"
                public ActionResult ajax_rechazar() { 
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);

                        if (respuesta == null)
                        {
                            int id;
                            id = this.convertObjAjaxToInt(frm["txtHdIdNotiEvento"]);
                            switch(this.convertObjAjaxToInt(frm["txtHdTipoEvento"])){
                                case 1:
                                    {
                                        NoticiasModel modeloNoticia = new NoticiasModel();
                                        Post post = modeloNoticia.sp_adminfe_noticias_cambiarEstadoPost(id, usuarioSession._idUsuario, this._idPagina, 0);
                                        if (post != null)
                                        {
                                            respuesta = new Dictionary<object, object>();
                                            respuesta.Add("estado", true);
                                            respuesta.Add("post", post);
                                        }
                                        else
                                        {
                                            ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                            respuesta = this.errorTryControlador(3, x);
                                        }
                                        break;
                                    }
                                case 2:{
                                    AdministracionModel modeloAdministracion = new AdministracionModel(); 
                                    EventoWebsite ew = modeloAdministracion.sp_adminfe_quitarEventoWebsite(id,"Rechazado por parte de coordinador ius",usuarioSession._idUsuario,this._idPagina);
                                    if (ew != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("post", ew);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                        respuesta = this.errorTryControlador(3, x);
                                    }
                                    break;
                                }
                            }
                            
                            
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
                public ActionResult ajax_revision()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);

                        if (respuesta == null)
                        {
                            NotiEvento noti = new NotiEvento(this.convertObjAjaxToInt(frm["txtHdIdNotiEvento"]));
                            noti._fechaCaducidad = this.convertObjAjaxToDateTime(frm["txtFechaCaducidad"].ToString(), "");
                            noti._idTipoEntrada = this.convertObjAjaxToInt(frm["txtHdTipoEvento"]);
                            NotiEvento notiEventoActualizado = this._model.sp_adminfe_cambiarEstadoPublicacion(noti, usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", true);
                            respuesta.Add("notiEvento", notiEventoActualizado);
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
                public ActionResult sp_adminfe_aprobarNoticia_cambiarEstado()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);

                        if (respuesta == null)
                        {
                            // txtHdIdNotiEvento,txtHdTipoEvento,txtFechaCaducidad
                            NotiEvento noti = new NotiEvento(this.convertObjAjaxToInt(frm["txtHdIdNotiEvento"]));
                            noti._fechaCaducidad = this.convertObjAjaxToDateTime(frm["txtFechaCaducidad"].ToString(), "");
                            noti._idTipoEntrada = this.convertObjAjaxToInt(frm["txtHdTipoEvento"]);
                            NotiEvento notiEventoActualizado = this._model.sp_adminfe_cambiarEstadoPublicacion(noti, usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", true);
                            respuesta.Add("notiEvento", notiEventoActualizado);
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
                
            #endregion
           
        #endregion
        
    }
}
