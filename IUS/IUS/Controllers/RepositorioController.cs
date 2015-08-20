using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// libreriras internas
    using IUS.Models.page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUS.Controllers
{
    public class RepositorioController : PadreController
    {
        #region "propiedades"
            private RepositorioModel _model;
            public int idPagina = (int)paginas.repositorio;
        #endregion
        #region "constructores"
            public RepositorioController()
            {
                this._model = new RepositorioModel();
            }
        #endregion
        #region "acciones url"
            public ActionResult FileByCategory(int id=-1,int id2=-1,int id3 = -1)
            {
                /*
                 id = carpeta que estamos buscando
                 id2 = filtro de busqueda de archivos
                 id3    = vista a mostrar || -1 cuadricula, 1 lista
                 */
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    string ip = Request.UserHostAddress;
                    Dictionary<object, object> archivos = this._model.sp_repo_front_getArchivosPublicosByType(id,id2, ip, this.idPagina);
                    ViewBag.carpetas    = archivos["carpetas"];
                    ViewBag.archivos    = archivos["archivos"];
                    ViewBag.carpetaPadre = archivos["carpetaPadre"];
                    ViewBag.accion      = "FileByCategory";
                    ViewBag.tipo        = id2;
                    ViewBag.menu24  = this.activeClass;
                    ViewBag.filtro  = id2;
                    ViewBag.vista = id3;
                    this.setTraduccion(traducciones);
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/Repositorio/AllFiles.cshtml");
                
            }
            public ActionResult AllFiles(int id=-1,int id2=-1,int id3=-1)
            {
                /*
                 id     = carpeta que estamos buscando
                 id2    = filtro de busqueda de archivos
                 id3    = vista a mostrar 
                 */
                List<LlaveIdioma> traducciones;
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
                    //************************************
                    ViewBag.noticias                    = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    string lang                         = this.getUserLang();
                    traducciones                        = this._model.getTraduccion(lang, this.idPagina);
                    string ip                           = Request.UserHostAddress;
                    Dictionary<object, object> archivos = this._model.sp_repo_front_GetAllCarpetasPublica(id, ip, this.idPagina);
                    ViewBag.carpetas                    = archivos["carpetas"];
                    ViewBag.archivos                    = archivos["archivos"];
                    ViewBag.carpetaPadre                = archivos["carpetaPadre"];
                    ViewBag.accion                      = "AllFiles";
                    ViewBag.menu24                      = this.activeClass;
                    ViewBag.filtro                      = -1;
                    ViewBag.vista                       = id3;
                    this.setTraduccion(traducciones);
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
            public ActionResult downloadFile(int id)
            {
                try
                {
                    string ip           = Request.UserHostAddress;
                    Archivo archivo     = this._model.sp_repo_front_getDownloadFilePublic(id, ip, this.idPagina);
                    byte[] fileBytes    = System.IO.File.ReadAllBytes(archivo._src);
                    string fileName = archivo._nombre + archivo._extension._extension;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
            }
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    
                    string lang     = this.getUserLang();
                    traducciones    = this._model.getTraduccion(lang, this.idPagina);
                    string ip       = Request.UserHostAddress;
                    this.setTraduccion(traducciones);
                    ViewBag.tiposArchivos = this._model.sp_repo_front_getTiposArchivos(lang,ip, this.idPagina);
                    ViewBag.menu24 = this.activeClass;
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
        #endregion
        #region "acciones ajax"
            public ActionResult navHistory()
            {
                Dictionary<object, object> frm, respuesta=null;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        string ip = Request.UserHostAddress;
                        int direccion = this.convertObjAjaxToInt(frm["direccion"]);
                        HistoryRepo history = (HistoryRepo)Session["HistoryRepo"];
                        Carpeta carpeta;
                        if (direccion == 1)
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
                        respuesta.Add("url", this.Url.Action(frm["accion"].ToString(), "Repositorio", new { id = carpeta._idCarpeta, id2 = this.convertObjAjaxToInt(frm["filtro"]), id3 = this.convertObjAjaxToInt(frm["vista"]) }));
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                return Json(respuesta);
            }
            public ActionResult getArchivosSinBusqueda()
            {
                Dictionary<object, object> frm, respuesta=null;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        string ip = Request.UserHostAddress;
                        int idCategoria = this.convertObjAjaxToInt(frm["idCategoria"]); int idCarpeta = this.convertObjAjaxToInt(frm["idCarpeta"]);
                        Dictionary<object, object> archivos;
                        if (idCategoria == -1)
                        {
                            archivos = this._model.sp_repo_front_GetAllCarpetasPublica(idCarpeta, ip, this.idPagina);
                        }
                        else
                        {
                            archivos = this._model.sp_repo_front_getArchivosPublicosByType(idCarpeta, idCategoria, ip, this.idPagina);
                        }
                        archivos.Add("estado", true);
                        respuesta = archivos;
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                return Json(respuesta);
            }
            public ActionResult sp_repo_searchArchivoPublico()
            {
                Dictionary<object, object> frm, respuesta=null;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {

                        List<LlaveIdioma> traducciones;
                        string lang = this.getUserLang();
                        traducciones = this._model.getTraduccion(lang, this.idPagina);
                        this.setTraduccion(traducciones);
                        string ip           = Request.UserHostAddress; bool encontrado = false;
                        List<ArchivoPublico> archivos = this._model.sp_repo_searchArchivoPublico(this.convertObjAjaxToInt(frm["idCategoria"]), frm["nombre"].ToString(), ip, this.idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        if (archivos != null && archivos.Count > 0)
                        {
                            encontrado = true;
                        }
                        else
                        {
                            //string mjs = ;
                            respuesta.Add("notFoundMjs",ViewData["archivo-no-encontrado"]);
                        }
                        respuesta.Add("encontrado", encontrado);
                        respuesta.Add("archivos", archivos);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
            public ActionResult sp_repo_front_getCarpetaPublicaByRuta()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        string ip                       = Request.UserHostAddress;
                        string ruta                     = frm["txtRutaPublica"].ToString();
                        CarpetaPublica carpetaPublica;
                        if (ruta != "/")
                        {
                            carpetaPublica = this._model.sp_repo_front_getCarpetaPublicaByRuta(ruta, ip, this.idPagina);
                        }
                        else
                        {
                            carpetaPublica = new CarpetaPublica(-1);
                        }
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("carpetaPublica", carpetaPublica);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql,x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
        #endregion
    }
}
