using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Drawing;
using System.IO;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Controllers
{
    public class AdministracionController : PadreController
    {

        #region "propiedades"
            private int                 _idPaginaEventos    = (int)paginas.Eventos;
            private int                 _idPaginaNoticias   = (int)paginas.Noticias;
            private AdministracionModel _model;
            private string              _nombreClass        = "AdministracionController";
        #endregion
        #region "url"
            public ActionResult valEntrarEventos()
            {
                Usuario usuarioSesion = this.getUsuarioSesion();
                if (usuarioSesion != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSesion._idUsuario, this._idPaginaEventos);
                    if (permisos != null && permisos._ver)
                    {
                        return RedirectToAction("Eventos", "Administracion");
                    }
                    else
                    {
                        return Redirect(this.URL_IUS + "Evento/Index");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
                //return null;
            }
            public ActionResult Eventos(int id=1,int id2=1,int id3=5)
            {
                /*
                 id: representa la pestaña en la que se encuentra actualmente
                 */
                Usuario usuarioSession = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPaginaEventos, 4);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    string appPath          = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    Dictionary<object, object> respuestaEventos = this._model.sp_adminfe_eventosCalendario(usuarioSession._idUsuario, this._idPaginaEventos, id3, id2);
                    List<Evento> eventos    = (List<Evento>)respuestaEventos["eventos"];
                    ViewBag.titleModulo     = "Eventos";
                    ViewBag.usuario         = usuarioSession;
                    ViewBag.eventos         = eventos;
                    ViewBag.cnEvento        = respuestaEventos["cnEvento"];
                    ViewBag.menus           = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.idTab           = id;
                    ViewBag.numPagina       = id2;
                    ViewBag.num             = id3;
                    // variables de navegación
                        ViewBag.nombreClass     = this._nombreClass.Replace("Controller","");
                        ViewBag.nombreFuncion   = "Eventos";
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Eventos-" + this._nombreClass, usuarioSession._idUsuario, this._idPaginaEventos);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Eventos-" + this._nombreClass, usuarioSession._idUsuario, this._idPaginaEventos);
                }
                return View();
            }
            public ActionResult getImageThumbnailEvent(int id)
            {
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    Evento evento = this._model.sp_adminfe_getEventById(id,usuarioSession._idUsuario,this._idPaginaEventos);
                    string ruta = this.getRelativePathFromAbsolute(evento._miniatura);
                    return base.File(ruta, "image/jpeg");
                }
                catch (ErroresIUS)
                {
                    string path = Server.MapPath("~/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                    return base.File(path, "image/jpeg");
                }
                catch (Exception x)
                {
                    string path = Server.MapPath("~/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                    return base.File(path, "image/jpeg");
                }
            }
            // Al parecer no funciona 
            public ActionResult ImagenEvento(int id)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPaginaEventos);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Evento evento = this._model._controlEvento.sp_adminfe_getEventById(id, usuarioSession._idUsuario, this._idPaginaEventos);
                    string ruta = evento._miniatura;
                    ViewBag.titleModulo = "Establecer imagen evento";
                    ViewBag.usuario     = usuarioSession;
                    ViewBag.menus       = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    if (!System.IO.File.Exists(evento._miniatura))
                    {
                        ruta = "";
                    }
                    else
                    {
                        ruta = Url.Content(this.getRelativePathFromAbsolute(ruta));
                    }
                    ViewBag.ruta = ruta;
                    ViewBag.idEvento    = id;
                    

                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "SetLogo-" + this._nombreClass, usuarioSession._idUsuario, this._idPaginaEventos);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "SetLogo-" + this._nombreClass, usuarioSession._idUsuario, this._idPaginaEventos);
                }
            }
            /*public ActionResult Noticias()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPaginaNoticias);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.selectedMenu = 4; // menu seleccionado 
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
            }*/
        #endregion 
        #region "acciones"
            #region "Eventos"
                #region "acciones"
                    #region "agregar"
                        public ActionResult sp_adminfe_eliminarEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                try
                                {
                                    bool estado = this._model.sp_adminfe_eliminarEvento(this.convertObjAjaxToInt(frm["idEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", estado);
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
                            return Json(respuesta);
                        }                
                        public ActionResult sp_adminfe_agregarPermisoUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                            
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_publicarEventoWebsite()
                        {
                            Dictionary<object, object> frm, respuesta = null;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            EventoWebsite eventoPublicado;
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                                
                                respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                                if (respuesta == null)
                                {
                                    //respuesta               = new Dictionary<object, object>();
                                    respuesta               = this._model.getInstanciaRespuestaAjax(usuarioSession, this._idPaginaEventos);
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
                            }
                            catch (ErroresIUS x)
                            {
                                ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql,x._mostrar);
                                respuesta = this.errorTryControlador(1, error);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                respuesta = this.errorTryControlador(2, error);
                            }
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_compartirEventoUsuario()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                            return Json(respuesta);
                        }
                    #endregion
                    #region "eliminar"
                        public ActionResult sp_adminfe_eliminarPermisoUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_editarEventos()
                        {
                            Dictionary<object, object> frm, respuesta;
                            Usuario usuarioSession = this.getUsuarioSesion();
                            frm = this.getAjaxFrm(); Evento eventoEditado;
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                            return Json(respuesta);
                        }
                        public ActionResult sp_adminfe_removeUsuarioEvento()
                        {
                            Dictionary<object, object> frm, respuesta;
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
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
                            return Json(respuesta);
                        }
                    #endregion
                    public ActionResult sp_adminfe_quitarEventoWebsite()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        Usuario usuarioSesion = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        EventoWebsite eventoWebsite;
                        respuesta = this.seguridadInicialAjax(usuarioSesion, frm);
                        if (respuesta == null)
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
                    public ActionResult sp_adminfe_setMiniaturaEvento() // aun no creado procedimiento almacenado
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        string fileName = ""; string path = "";
                        try
                        {
                            Usuario usuarioSession = this.getUsuarioSesion();
                            frm = this.getAjaxFrm();
                            int idEvento = this.convertObjAjaxToInt(frm["txtHdIdEvento"]);
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                                if (files.Count > 0)
                                {
                                    foreach (HttpPostedFileBase file in files)
                                    {
                                        fileName = Path.GetFileName(file.FileName);
                                        var strExtension = Path.GetExtension(file.FileName);
                                        string path2 = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["IMAGEN_EVENTO"]  + "/"), idEvento.ToString());
                                        path = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["IMAGEN_EVENTO"] + "/"), idEvento.ToString() + strExtension);
                                        file.SaveAs(path);
                                        string rutaRecortada = path;
                                        //decimal xx = this.convertObjAjaxToDecimal(frm["x"]); decimal yy = this.convertObjAjaxToDecimal(frm["y"]);
                                        decimal xancho = this.convertObjAjaxToDecimal(frm["imgAncho"]); decimal yalto = this.convertObjAjaxToDecimal(frm["imgAlto"]);
                                        Coordenadas coordenadas = new Coordenadas(this.convertObjAjaxToDecimal(frm["x"]), this.convertObjAjaxToDecimal(frm["y"]), this.convertObjAjaxToDecimal(frm["imgAncho"]), this.convertObjAjaxToDecimal(frm["imgAlto"]));
                                        
                                        using (Image image = Image.FromFile(path))
                                        {
                                            rutaRecortada = path2 + "_recortada" + strExtension;
                                            if ((image.Width != image.Height || (xancho > 0 && yalto > 0 && xancho > 0)))
                                            {
                                                int x       = decimal.ToInt32(image.Width * coordenadas._x);
                                                int y       = decimal.ToInt32(image.Height * coordenadas._y);
                                                int ancho   = decimal.ToInt32(image.Width * coordenadas._ancho); //decimal.ToInt32(xancho);
                                                int alto    = decimal.ToInt32(image.Height * coordenadas._alto);
                                                Rectangle cropArea = new Rectangle(x, y, ancho, alto);
                                                try
                                                {
                                                    using (Bitmap bitMap = new Bitmap(cropArea.Width, cropArea.Height))
                                                    {
                                                        using (Graphics g = Graphics.FromImage(bitMap))
                                                        {
                                                            g.DrawImage(image, new Rectangle(0, 0, bitMap.Width, bitMap.Height), cropArea, GraphicsUnit.Pixel);
                                                        }
                                                        bitMap.Save(rutaRecortada);
                                                    }

                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }
                                            else
                                            {
                                                //rutaRecortada
                                                file.SaveAs(rutaRecortada);
                                            }
                                        }
                                        /*info._fotoRuta = rutaRecortada;
                                        InformacionPersona infoAgregada = this._model.sp_rrhh_setFotoInformacionPersona(info, usuarioSession._idUsuario, this._idPagina);*/
                                        Evento eventoEditar = new Evento(idEvento);
                                        eventoEditar._miniatura = rutaRecortada;
                                        Evento evento = this._model.sp_adminfe_setMiniaturaEvento(eventoEditar, usuarioSession._idUsuario, this._idPaginaEventos);
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        string ruta = Url.Content(this.getRelativePathFromAbsolute(rutaRecortada));
                                        respuesta.Add("imagen", ruta);
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
                #endregion
                #region "gets"
                    public ActionResult ajax_getEventosCalendario()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        try
                        {
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();

                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                respuesta = new Dictionary<object,object>();
                                Dictionary<object, object> respuestaEventos = this._model.sp_adminfe_eventosCalendario(usuarioSession._idUsuario, this._idPaginaEventos,this.convertObjAjaxToInt(frm["n"]),this.convertObjAjaxToInt(frm["pagina"]));
                                respuesta.Add("estado", true);
                                respuesta.Add("respEventos", respuestaEventos);
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
                    public ActionResult sp_adminfe_buscarAllEventosPersonalesByDate()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        try
                        {
                            frm = this.getAjaxFrm();
                            Usuario usuarioSession = this.getUsuarioSesion();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                respuesta = new Dictionary<object, object>();
                                // ---------------------
                                    DateTime fechaInicio = this.convertObjAjaxToDateTime(frm["txtDeFechaBusqueda"].ToString(), "");
                                    DateTime fechaFin = this.convertObjAjaxToDateTime(frm["txtHastaFecha"].ToString(), "");
                                // ----------------------
                                List<Evento> eventos = this._model.sp_adminfe_buscarAllEventosPersonalesByDate(fechaInicio, fechaFin, usuarioSession._idUsuario, this._idPaginaNoticias);
                                respuesta.Add("estado", true);
                                respuesta.Add("eventos", eventos);
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
                    public ActionResult sp_adminfe_getPermisosUsuarioEvento()
                    {
                        Dictionary<object, object> frm, respuesta=null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Dictionary<object, object> permisos = this._model.sp_adminfe_getPermisosUsuarioEvento(this.convertObjAjaxToInt(frm["idUsuarioEvento"]), usuarioSession._idUsuario, this._idPaginaEventos);
                            List<PermisoEvento> permisosFaltantes = (List<PermisoEvento>)permisos["permisosFaltantes"];
                            List<PermisoUsuarioEvento> permisosActuales = (List<PermisoUsuarioEvento>)permisos["permisosActuales"];
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("permisosFaltantes", permisosFaltantes);
                            respuesta.Add("permisosActuales", permisosActuales);
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_adminfe_loadCompartirEventos()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
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
                        return Json(respuesta);
                    }
                    //#########
                    public ActionResult sp_adminfe_getEventosPrincipales()
                    {
                        Dictionary<object, object> respuesta = new Dictionary<object, object>();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        try
                        {
                            string appPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                            Dictionary<object, object> respuestaEvento = this._model.sp_adminfe_eventosCalendario(usuarioSession._idUsuario, this._idPaginaEventos);
                            List<Evento> eventos = (List<Evento>)respuestaEvento["eventos"];
                            respuesta.Add("estado", true);
                            respuesta.Add("eventos", eventos);
                        }
                        catch (ErroresIUS)
                        {
                            // nose que podria hacer aqui 
                        }
                        catch (Exception)
                        {
                            // algo se pudiera hacer aqui
                        }
                        
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
