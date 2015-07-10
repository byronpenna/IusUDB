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
            public ActionResult FileByCategory(int id=-1,int id2=-1)
            {
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
                    ViewBag.accion      = "FileByCategory";
                    ViewBag.tipo        = id2;
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
            public ActionResult AllFiles(int id=-1)
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.noticias    = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    string lang         = this.getUserLang();
                    traducciones        = this._model.getTraduccion(lang, this.idPagina);
                    string ip           = Request.UserHostAddress;
                    Dictionary<object, object> archivos = this._model.sp_repo_front_GetAllCarpetasPublica(id, ip, this.idPagina);
                    ViewBag.carpetas        = archivos["carpetas"];
                    ViewBag.archivos        = archivos["archivos"];
                    ViewBag.carpetaPadre    = archivos["carpetaPadre"];
                    ViewBag.accion          = "AllFiles";
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
                    ViewBag.tiposArchivos = this._model.sp_repo_front_getTiposArchivos(ip, this.idPagina);
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
    }
}
