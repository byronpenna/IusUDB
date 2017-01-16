using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
// liberias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
    using IUSBack.Models.Page.Repositorio.Entidades;

// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUSBack.Controllers
{
    public class RepositorioController : PadreController
    {
        #region "constructores"
            public RepositorioController()
            {
                this._model = new RepositorioModel();
            }
        #endregion
        #region "propiedades"
            public int              _idPagina       = (int)paginas.Repositorio;
            public RepositorioModel _model;
            private string          _nombreClass = "RepositorioController";
        #endregion 
        #region "url"
            public ActionResult AprobarArchivos()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                //var xx = Session["HistoryRepo"];
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    RepositorioPublicoModel model = new RepositorioPublicoModel();
                    List<ArchivoPublico> archivosPublicos = new List<ArchivoPublico>();
                    ViewBag.archivos = model.sp_repo_getPendienteAprobacion(usuarioSession._idUsuario, this._idPagina);

                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-RepositorioController", usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-RepositorioController", usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult Index(int id = -1, int id2= -1)
            {
                /*
                 * id: representa la carpeta actual
                 * id2: representa la vista que aparecera
                 */
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                //var xx = Session["HistoryRepo"];
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    HistoryRepo history;
                    if (Session["HistoryRepo"] == null)
                    {
                        history = new HistoryRepo(id);
                        Session["HistoryRepo"] = history;
                    }
                    else
                    {
                        history = (HistoryRepo)Session["HistoryRepo"];
                        history.insertHistory(id);
                        Session["HistoryRepo"] = history;
                    }
                    ViewBag.selectedMenu = 4; // menu seleccionado 
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> archivos;
                    Carpeta carpeta;
                    if (id != -1)
                    {
                         carpeta = new Carpeta(id);
                         archivos = this._model.sp_repo_entrarCarpeta(carpeta, usuarioSession._idUsuario, this._idPagina);
                    }
                    else
                    {
                        archivos = this._model.sp_repo_getRootFolder(usuarioSession._idUsuario, this._idPagina);
                    }
                    ViewBag.idCarpetaActual     = id;
                    ViewBag.titleModulo         = "Repositorio Digital";
                    ViewBag.vista               = id2;
                    ViewBag.usuario             = usuarioSession;
                    ViewBag.permisos            = permisos;
                    ViewBag.carpetas            = archivos["carpetas"];
                    ViewBag.archivos            = archivos["archivos"];
                    ViewBag.carpetaActual       = archivos["carpetaPadre"];
                    ViewBag.menus               = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    // metricas para funciones generales
                        ViewBag.nombreControlador   = this._nombreClass.Replace("Controller","");
                        ViewBag.nombreMetodo        = "Index";
                    // Tab seleccionada
                        ViewBag.selectedLi1 = "tabActive";
                    return View("~/Views/Repositorio/Indexi.cshtml");
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-RepositorioController", usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-RepositorioController", usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult DescargarFichero(int id=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                Usuario usuarioSession = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    //Usuario usuarioSession = this.getUsuarioSesion();
                    Archivo archivo = this._model.sp_repo_getDownloadFile(id, usuarioSession._idUsuario, this._idPagina);
                    //string ruta = this._RUTASGLOBALES["REPOSITORIO_DIGITAL"] + "/"+usuarioSession._idUsuario+"/"+archivo._carpeta._idCarpeta+"/"+archivo._idArchivo+archivo._extension._extension+"";
                    
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(ruta));
                    string ruta = archivo._src;
                    byte[] fileBytes = System.IO.File.ReadAllBytes(ruta);
                    string fileName = archivo._nombre+archivo._extension._extension;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "DescargarFichero-RepositorioControler", usuarioSession._idUsuario, this._idPagina);
                    //return new EmptyResult();
                }
                catch (Exception x)
                {
                    // algun mejor error por aqui
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "DescargarFichero-RepositorioController", usuarioSession._idUsuario, this._idPagina);
                    //return new EmptyResult();
                }
            }
            public string       NotFolderFound()
            {
                return "folder no encontrado";
            }
        #endregion
        #region "acciones ajax"
                public ActionResult navHistory()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            HistoryRepo history = (HistoryRepo)Session["HistoryRepo"];
                            Carpeta carpeta;
                            if (this.convertObjAjaxToInt(frm["direccion"]) == 1)
                            {
                                carpeta = history.historyFoward();
                            }
                            else
                            {
                                carpeta = history.historyBack();
                            }
                            Session["HistoryRepo"] = history;
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("url", this.Url.Action("Index", "Repositorio", new { id = carpeta._idCarpeta,id2= this.convertObjAjaxToInt(frm["vistaActual"]) }));
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
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
            #region "controlArchivo"
                public ActionResult sp_repo_deleteFile()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estado = this._model.sp_repo_deleteFile(Server.MapPath(this._RUTASGLOBALES["REPOSITORIO_DIGITAL"]),this.convertObjAjaxToInt(frm["idArchivo"]), usuarioSession._idUsuario, this._idPagina);
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
                //#########
                public ActionResult sp_repo_uploadFile()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    bool guardo = false; bool guardoBase = false;
                    string path = ""; string fileName = "¿?";
                    try
                    {
                        //var form = this._jss.Deserialize<Dictionary<object, object>>(Request.Form["form"]);
                        frm = this.getAjaxFrm();

                        Usuario usuarioSession = this.getUsuarioSesion();
                        int idCarpetaPadre = this.convertObjAjaxToInt(frm["txtHdIdCarpetaPadre"]);
                        if (Request.Files.Count > 0)
                        {
                            List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                            if (files.Count > 0)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {

                                    fileName = Path.GetFileName(file.FileName);
                                    var strExtension = Path.GetExtension(file.FileName);
                                    ExtensionArchivo extension = new ExtensionArchivo(strExtension);
                                    Archivo archivoAgregar = new Archivo(fileName.Substring(0, fileName.IndexOf(strExtension)), idCarpetaPadre, path, extension);
                                    Archivo archivoAgregado = this._model.sp_repo_uploadFile(archivoAgregar, usuarioSession._idUsuario, this._idPagina);
                                    path = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["REPOSITORIO_DIGITAL"] + usuarioSession._idUsuario + "/" + idCarpetaPadre), archivoAgregado._idArchivo.ToString() + strExtension);
                                    file.SaveAs(path);
                                    archivoAgregado._src = path;
                                    archivoAgregado = this._model.sp_repo_refreshSourceFile(archivoAgregado, usuarioSession._idUsuario, this._idPagina);
                                    guardo = true;
                                    if (archivoAgregado != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("archivo", archivoAgregado);
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error inesperado", ErroresIUS.tipoError.generico, 0);
                                        this.errorTryControlador(3, x);
                                    }

                                }
                            }
                            /**/

                        }
                    }
                    catch (ErroresIUS x)
                    {
                        if (guardo && !guardoBase)
                        {
                            System.IO.File.Delete(path);
                        }
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                        Archivo archivo = new Archivo(fileName);
                        respuesta.Add("archivo", archivo);

                    }
                    catch (Exception x)
                    {
                        if (guardo && !guardoBase)
                        {
                            System.IO.File.Delete(path);
                        }
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                        Archivo archivo = new Archivo(fileName);
                        respuesta.Add("archivo", archivo);

                    }

                    return Json(respuesta);
                }
                
                public ActionResult sp_repo_changeFileName()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    { 
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Archivo archivoModificar = new Archivo(this.convertObjAjaxToInt(frm["idArchivo"]), frm["nombreArchivo"].ToString());
                            Archivo archivoModificado = this._model.sp_repo_changeFileName(archivoModificar, usuarioSession._idUsuario, this._idPagina);
                            if (archivoModificado != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("archivo", archivoModificado);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Ocurrio un error inesperado", ErroresIUS.tipoError.generico, 0);
                                throw x;
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
                public ActionResult sp_repo_searchArchivo()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<Archivo> archivos = this._model.sp_repo_searchArchivo(frm["txtBusqueda"].ToString(), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("archivos", archivos);
                            respuesta.Add("urlBase", this.Url.Action("DescargarFichero","Repositorio"));
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
            #region "controlCarpeta"
                public ActionResult sp_repo_byRuta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            string ruta =frm["txtRuta"].ToString();
                            Carpeta carpeta;
                            if (ruta != "/")
                            {
                                carpeta = this._model.sp_repo_byRuta(ruta, usuarioSession._idUsuario, this._idPagina);
                            }
                            else
                            {
                                carpeta = new Carpeta(-1);
                            }
                            if (carpeta == null)
                            {
                                ErroresIUS x = new ErroresIUS("Carpeta no encontrada", ErroresIUS.tipoError.generico, 0, "", true);
                                throw x;
                            }
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpeta", carpeta);
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
                public ActionResult sp_repo_entrarCarpeta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            int idCarpeta = this.convertObjAjaxToInt(frm["idCarpeta"]);
                            Carpeta carpeta = new Carpeta(idCarpeta);
                            Dictionary<object, object> archivos=null;
                            if (idCarpeta != -1)
                            {
                                archivos = this._model.sp_repo_entrarCarpeta(carpeta, usuarioSession._idUsuario, this._idPagina);
                            }
                            else
                            {
                                archivos = this._model.sp_repo_getRootFolder(usuarioSession._idUsuario, this._idPagina);
                            }
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpetas", archivos["carpetas"]);
                            respuesta.Add("archivos", archivos["archivos"]);
                            respuesta.Add("carpetaPadre", archivos["carpetaPadre"]);
                            respuesta.Add("urlBase", this.Url.Action("DescargarFichero", "Repositorio"));
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
                public ActionResult sp_repo_deleteFolder()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estadoDelete = this._model.sp_repo_deleteFolder(Server.MapPath(this._RUTASGLOBALES["REPOSITORIO_DIGITAL"]), this.convertObjAjaxToInt(frm["idCarpeta"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", estadoDelete);
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
                public ActionResult sp_repo_updateCarpeta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Carpeta carpetaActualizar = new Carpeta(this.convertObjAjaxToInt(frm["txtHdIdCarpeta"]), frm["nombre"].ToString());
                            Carpeta carpetaActualizada = this._model.sp_repo_updateCarpeta(carpetaActualizar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpeta", carpetaActualizada);

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
                public ActionResult sp_repo_insertCarpeta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Carpeta carpetaPadre;
                            int idCarpetaPadre = this.convertObjAjaxToInt(frm["idCarpetaPadre"]);
                            carpetaPadre = new Carpeta(idCarpetaPadre);
                            Carpeta capetaIngresar = new Carpeta(frm["nombre"].ToString(), usuarioSession, carpetaPadre);
                            Carpeta carpetaIngresada = this._model.sp_repo_insertCarpeta(capetaIngresar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpeta", carpetaIngresada);
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
