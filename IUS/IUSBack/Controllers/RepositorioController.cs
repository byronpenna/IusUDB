using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
// liberias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;

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
            public int _idPagina = (int)paginas.Repositorio;
            public RepositorioModel _model;
        #endregion 
        #region "url"
            public ActionResult Index(int id = -1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
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
                    ViewBag.idCarpetaActual = id;
                    ViewBag.titleModulo = "Repositorio Digital";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.permisos = permisos;
                    ViewBag.carpetas = archivos["carpetas"];
                    ViewBag.archivos = archivos["archivos"];
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    return View();
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
            /*public ActionResult Index()
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> archivos = this._model.sp_repo_getRootFolder(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.titleModulo = "Repositorio Digital";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.permisos = permisos;
                    ViewBag.carpetas = archivos["carpetas"];
                    ViewBag.archivos = archivos["archivos"];
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                
            }*/
        #endregion
        #region "acciones ajax"
            public ActionResult sp_repo_entrarCarpeta()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        Carpeta carpeta = new Carpeta(this.convertObjAjaxToInt(frm["idCarpeta"]));
                        Dictionary<object,object> archivos = this._model.sp_repo_entrarCarpeta(carpeta, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("carpetas", archivos["carpetas"]);
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
                                //path = Path.Combine(Server.MapPath("~/RepositorioDigital/Usuarios/"+usuarioSession._idUsuario), fileName);
                                path = this.getPath("~/RepositorioDigital/Usuarios/" + usuarioSession._idUsuario+"/"+idCarpetaPadre, fileName);
                                file.SaveAs(path);
                                guardo = true;
                                ExtensionArchivo extension = new ExtensionArchivo(strExtension);
                                Archivo archivoAgregar = new Archivo(fileName, idCarpetaPadre ,path, extension);
                                Archivo archivoAgregado = this._model.sp_repo_uploadFile(archivoAgregar, usuarioSession._idUsuario, this._idPagina);
                                if(archivoAgregado != null){
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
            public ActionResult sp_repo_updateCarpeta()
            {
                Dictionary<object, object> frm, respuesta=null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        Carpeta carpetaActualizar = new Carpeta(this.convertObjAjaxToInt(frm["txtHdIdCarpeta"]), frm["nombre"].ToString());
                        Carpeta carpetaActualizada = this._model.sp_repo_updateCarpeta(carpetaActualizar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("carpeta", carpetaActualizada);

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
            public ActionResult sp_repo_insertCarpeta()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
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
        #endregion 

    }
}
