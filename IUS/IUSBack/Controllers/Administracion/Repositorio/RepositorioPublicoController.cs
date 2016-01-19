using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Repositorio.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUSBack.Controllers
{
    public class RepositorioPublicoController : PadreController
    {
        #region "propiedades"
            public int                      _idPagina       = (int)paginas.RepositorioPublico;
            public RepositorioPublicoModel  _model;
            public string                   _nombreClass    = "RepositorioPublicoController";
        #endregion
        #region "constructor"
            public RepositorioPublicoController()
            {
                this._model = new RepositorioPublicoModel();
            }
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ViewBag.selectedMenu = 4; // menu seleccionado 
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> archivos;
                    CarpetaPublica carpetaPadre = null;
                    if (id != -1)
                    {
                       archivos = this._model.sp_repo_entrarCarpetaPublica(id,usuarioSession._idUsuario,this._idPagina);
                       carpetaPadre = (CarpetaPublica)archivos["carpetaPadre"];
                    }
                    else
                    {
                        carpetaPadre = new CarpetaPublica(id);
                        carpetaPadre._strRuta = "/";
                        archivos = this._model.sp_repo_getRootFolderPublico(usuarioSession._idUsuario, this._idPagina);
                    }
                    // viewback 
                    ViewBag.titleModulo = "Repositorio publico";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.permisos = permisos;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.carpetas = archivos["carpetas"];
                    ViewBag.archivos = archivos["archivos"];
                    ViewBag.carpetaPadre = carpetaPadre;
                    ViewBag.idCarpetaActual = id;
                    ViewBag.URL_IUS = this.URL_IUS;
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
                return View();
            }
        #endregion
        #region "genericas"
            public Dictionary<object, object> getArchivosNavegacion(int idCarpetaPadre,Usuario usuarioSession)
            {
                Dictionary<object, object> archivos;
                CarpetaPublica carpetaPadre;
                try
                {
                    if (idCarpetaPadre != -1)
                    {
                        archivos = this._model.sp_repo_entrarCarpetaPublica(idCarpetaPadre, usuarioSession._idUsuario, this._idPagina);
                        carpetaPadre = (CarpetaPublica)archivos["carpetaPadre"];
                    }
                    else
                    {
                        archivos = this._model.sp_repo_getRootFolderPublico(usuarioSession._idUsuario, this._idPagina);
                        carpetaPadre = new CarpetaPublica(-1);
                        carpetaPadre._strRuta = "/";
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                archivos.Add("carpetaPadreSend", carpetaPadre);
                return archivos;
            }
            /*public Dictionary<object, object> getRetornoStandarFicheros(Dictionary<object,object> data,int idCarpetaPadre,string baseUrl)
            {
                data.Add("idCarpetaPadre", idCarpetaPadre);
                data.Add("carpetaPadre", data["carpetaPadreSend"]);
                data.Add("base", this.URL_IUS);
                return data;
            }*/
        #endregion
        #region "resultados ajax"
            #region "get"
                public ActionResult sp_repo_getAjaxPublicoByRuta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            /* Podria encapsularse en una funcion */
                            string ruta = frm["txtRuta"].ToString();
                            CarpetaPublica carpeta;
                            if (ruta != "/")
                            {
                                carpeta = this._model.sp_repo_getPublicoByRuta(ruta, usuarioSession._idUsuario, this._idPagina);
                            }
                            else
                            {
                                carpeta = new CarpetaPublica(-1);
                            }
                            Dictionary<object,object> archivos = this.getArchivosNavegacion(carpeta._idCarpetaPublica, usuarioSession);
                            archivos.Add("estado", true);
                            archivos.Add("idCarpetaPadre", carpeta._idCarpetaPublica);
                            archivos.Add("carpetaPadre", archivos["carpetaPadreSend"]);
                            archivos.Add("base", this.URL_IUS);
                            respuesta = archivos;
                            /*
                             **************************
                             */
                                                              /* / \*/
                            /* Podria encapsularse en una funcion |*/
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
                public ActionResult sp_repo_getPublicoByRuta()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            string ruta = frm["txtRuta"].ToString();
                            CarpetaPublica carpeta;
                            if (ruta != "/")
                            {
                                carpeta = this._model.sp_repo_getPublicoByRuta(ruta, usuarioSession._idUsuario, this._idPagina);
                            }
                            else
                            {
                                carpeta = new CarpetaPublica(-1);
                            }
                            
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpetaPublica", carpeta);
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
                public ActionResult sp_repo_compartirArchivoPublico() { 
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            ArchivoPublico archivoAgregar = new ArchivoPublico(this.convertObjAjaxToInt(frm["txtHdIdArchivoCompartir"]), this.convertObjAjaxToInt(frm["txtHdCarpetaPadrePublica"]), frm["txtNombreFileCompartir"].ToString());
                            ArchivoPublico archivoAgregado = this._model.sp_repo_compartirArchivoPublico(archivoAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("archivoPublico", archivoAgregado);

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
                public ActionResult sp_repo_atrasCarpetaPublica()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Dictionary<object, object> archivos = this._model.sp_repo_atrasCarpetaPublica(this.convertObjAjaxToInt(frm["idCarpetaPublica"]), usuarioSession._idUsuario, this._idPagina);
                            
                            archivos.Add("estado", true);
                            archivos.Add("base", this.URL_IUS);
                            respuesta = archivos;
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
                // ***********
                    public ActionResult sp_repo_entrarCarpetaPublica()
                {
                     Dictionary<object, object> frm, respuesta = null;
                     try
                     {
                         Usuario usuarioSession = this.getUsuarioSesion();
                         frm = this.getAjaxFrm();
                         
                         respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                         if (respuesta == null)
                         {
                             int idCarpetaPadre = this.convertObjAjaxToInt(frm["idCarpetaPublica"]);
                             Dictionary<object, object> archivos ;
                             archivos = this.getArchivosNavegacion(idCarpetaPadre, usuarioSession);
                             respuesta = new Dictionary<object, object>();
                             respuesta.Add("estado", true);
                             respuesta.Add("carpetas", archivos["carpetas"]);
                             respuesta.Add("archivos", archivos["archivos"]);
                             respuesta.Add("idCarpetaPadre", idCarpetaPadre);
                             //respuesta.Add("carpetaPadre", carpetaPadre);
                             respuesta.Add("carpetaPadre", archivos["carpetaPadreSend"]);
                             respuesta.Add("base", this.URL_IUS);
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
                // ***********
                public ActionResult sp_repo_searchArchivoPublicoBack()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            List<ArchivoPublico> archivos = this._model.sp_repo_searchArchivoPublicoBack(frm["txtBusqueda"].ToString(), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("archivos", archivos);
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
                public ActionResult sp_repo_getRootFolderPublico()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();

                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            Dictionary<object,object> archivos = this._model.sp_repo_getRootFolderPublico(usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpetas", archivos["carpetas"]);
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
                public ActionResult sp_repo_removeShareArchivoPublico()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            int idArchivoPublico = this.convertObjAjaxToInt(frm["idArchivoPublico"]);
                            bool estado = this._model.sp_repo_removeShareArchivoPublico(idArchivoPublico, usuarioSession._idUsuario, this._idPagina);
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
                public ActionResult sp_repo_deleteCarpetaPublica()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool estado = this._model.sp_repo_deleteCarpetaPublica(this.convertObjAjaxToInt(frm["idCarpeta"]), usuarioSession._idUsuario, this._idPagina);
                            if (estado)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", estado);
                            }
                            else
                            {
                                ErroresIUS error = new ErroresIUS("Error inesperado", ErroresIUS.tipoError.generico, 0);
                                throw error;
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
                public ActionResult sp_repo_updateCarpetaPublica()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            CarpetaPublica carpetaUpdate = new CarpetaPublica(this.convertObjAjaxToInt(frm["txtHdIdCarpeta"]), frm["nombre"].ToString());
                            CarpetaPublica carpetaActualizada = this._model.sp_repo_updateCarpetaPublica(carpetaUpdate,usuarioSession._idUsuario,this._idPagina);
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
                public ActionResult sp_repo_renameFile()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            ArchivoPublico archivoEditar = new ArchivoPublico(this.convertObjAjaxToInt(frm["idArchivo"]), frm["nombreArchivo"].ToString());
                            ArchivoPublico archivoAgregado = this._model.sp_repo_renameFile(archivoEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("archivoPublico", archivoAgregado);
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
                public ActionResult sp_repo_insertCarpetaPublica()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            CarpetaPublica carpetaIngresar = new CarpetaPublica(frm["nombre"].ToString(), this.convertObjAjaxToInt(frm["idCarpetaPadre"]));
                            CarpetaPublica carpetaIngresada = this._model.sp_repo_insertCarpetaPublica(carpetaIngresar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("carpeta",carpetaIngresada);
                            respuesta.Add("base", this.URL_IUS);
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
