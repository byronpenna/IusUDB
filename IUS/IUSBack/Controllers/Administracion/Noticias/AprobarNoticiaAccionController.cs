using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

    using System.Net.Mail;
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
            private string _nombreClass = "AprobarNoticiaController";
        #endregion
        #region "constructores"
            public AprobarNoticiaAccionController()
            {
                this._model = new AprobarNoticiasModel();
            }
        #endregion 
        #region "url"
            public ActionResult preview(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    NoticiasModel modelo = new NoticiasModel();
                    Dictionary<object, object> cuerpoPagina = modelo.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina);
                    Post post = (Post)cuerpoPagina["post"];
                    ViewBag.post = post;
                    ViewBag.origen = 2; //origen aprobar
                    return View("~/Views/Noticias/preview.cshtml");
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
                public ActionResult sp_adminfe_eliminarSolicitudPublicacion()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);

                        if (respuesta == null)
                        {
                            NotiEvento notiEventoEliminar = new NotiEvento(this.convertObjAjaxToInt(frm["txtHdIdNotiEvento"]));
                            notiEventoEliminar._idTipoEntrada = this.convertObjAjaxToInt(frm["txtHdTipoEvento"]);
                            bool estado = this._model.sp_adminfe_eliminarSolicitudPublicacion(notiEventoEliminar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", estado);
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
                #region "genericas"
                    public void enviarCorreo(string email,int op,string mensaje)
                    {
                        //usuarioAgregado._email,codigo._numero
                        string ruta = Request.Url.AbsoluteUri;
                        ruta = ruta.Substring(0, this.CustomIndexOf(ruta, '/', 3));
                        MailMessage m = new MailMessage();
                        m.To.Add(email);
                        m.Subject = this.getSubject(op); //"Por favor confirmar cuenta IUS";
                        m.Body = "Su noticia no fue publicada debido a que presenta algunos inconvenientes por favor prestar atencion y corregir: <br>" +
                            mensaje;
                        m.IsBodyHtml = true;
                        m.Priority = MailPriority.Normal;
                        SmtpClient cliente = new SmtpClient();
                        cliente.Send(m);
                    }
                    public string getSubject(int op)
                    {
                        switch (op)
                        {
                            case 1:
                                {
                                    return "Solicitud de cambio noticias - IUS ";
                                }
                            case 2:
                                {
                                    return "Noticia no fue aceptada - IUS";
                                }
                            default:
                                {
                                    return "";
                                }
                        }
                    }
                #endregion

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
                                        string motivo = frm["txtAreaMotivos"].ToString();
                                        int Accion = this.convertObjAjaxToInt(frm["txtHdIdAccion"]);
                                        bool eliminado = false;
                                        if (Accion == 2)
                                        {
                                            eliminado = true;
                                        }
                                        Post post = modeloNoticia.sp_adminfe_noticias_cambiarEstadoPost(id, usuarioSession._idUsuario, this._idPagina, 0,eliminado);
                                        string email = post._usuario._persona.emailsContacto[0]._email;
                                        if (email != "")
                                        {
                                            this.enviarCorreo(email, this.convertObjAjaxToInt(frm["txtHdIdAccion"]), motivo);
                                        }
                                        
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
                            respuesta = new Dictionary<object, object>();
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
