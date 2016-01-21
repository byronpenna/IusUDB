using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.GestionIdiomaWebsite.Acciones;    
// librerias externas 
    using IUSLibs.TRL.Entidades;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class GestionIdiomaWebsiteController : PadreController
    {
        //
        // GET: /GestionIdiomaWebsite/
        #region "propiedades"
            public int                          _idPagina = (int)paginas.gestionIdiomaWebsite;
            public GestionIdiomaWebsiteModel    _model;
            public string                       _nombreClass = "GestionIdiomaWebsiteController";
        #endregion
        #region "constructores"
            public GestionIdiomaWebsiteController()
            {
                this._model = new GestionIdiomaWebsiteModel();
            } 
        #endregion
        #region "URL"
            public ActionResult Index()
            {
                ActionResult    seguridadInicial    = this.seguridadInicial(this._idPagina);
                Usuario         usuarioSession      = this.getUsuarioSesion();
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    ViewBag.selectedMenu = 3; // menu seleccionado 
                    
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (usuarioSession != null)
                    {
                        List<Idioma> idiomas = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                        List<Pagina> paginas = this._model.sp_trl_getAllPaginas(usuarioSession._idUsuario, this._idPagina);
                        List<LlaveIdioma> tabla = this._model.sp_trl_tablitaGestionTraduccion(usuarioSession._idUsuario, this._idPagina);
                        
                        // generales
                        ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                        // propias de pagina
                        ViewBag.idiomas = idiomas;
                        ViewBag.paginas = paginas;
                        ViewBag.tbTraducciones = tabla;
                        ViewBag.titleModulo = "Traduccion del sitio web";
                        ViewBag.permisos = permisos;
                        ViewBag.usuario = usuarioSession;
                    }
                    else
                    {
                        return RedirectToAction("index", "login");
                    }
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                return View();
            }
        #endregion
        #region "ajax"
            #region "gets"
                public ActionResult sp_trl_getLlaveFromPageAndIdioma()
                {
                    Dictionary<Object,Object> frm,respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        try
                        {
                            
                            List<Llave> llaves = this._model.sp_trl_getLlaveFromPageAndIdioma(Convert.ToInt32(frm["idPaginaFront"].ToString()), Convert.ToInt32(frm["idIdioma"].ToString()), usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", true);
                            respuesta.Add("Llaves", llaves);
                        }
                        catch (Exception x)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 2);
                            respuesta.Add("error", x);
                        }
                    }
                    return Json(respuesta);
                }
                public ActionResult getObjetosTablita()
                {
                    Dictionary<Object, Object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        int idLlaveIdioma = Convert.ToInt32(frm["idLlaveIdioma"].ToString());
                        List<Llave> llaves; List<Idioma> idiomas; List<Pagina> paginas;
                        try
                        {
                            llaves = this._model.sp_trl_getLlaveFromLlaveIdioma(idLlaveIdioma, usuarioSession._idUsuario, this._idPagina);
                            idiomas = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                            paginas = this._model.sp_trl_getAllPaginas(usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("llaves", llaves);
                            respuesta.Add("idiomas", idiomas);
                            respuesta.Add("paginas", paginas);
                            respuesta.Add("estado", true);
                        }
                        catch (ErroresIUS)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("error", "ocurrio un error al cargar la informacion");
                        }
                        catch (Exception)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("error", "ocurrio un error al cargar la informacion");
                        }
                    }
                    return Json(respuesta);
                }
            #endregion 
            #region "acciones"
                //*********************************
                public ActionResult sp_trl_agregarLlaveIdioma()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Llave llave; Idioma idioma; // clases para crear la otra
                    LlaveIdioma llaveIdioma; LlaveIdioma aAgregar;
                    List<Llave> llaves;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        try
                        {
                            llave = new Llave( Convert.ToInt32(frm["idLlave"].ToString()) );
                            idioma = new Idioma( Convert.ToInt32(frm["idIdioma"].ToString()) );
                            aAgregar = new LlaveIdioma(idioma, llave, frm["traduccion"].ToString());
                            aAgregar._traduccion = aAgregar._traduccion.Replace("\n", "<br>");
                            llaveIdioma = this._model.sp_trl_agregarLlaveIdioma(aAgregar,usuarioSession._idUsuario,this._idPagina);
                            if (llaveIdioma != null)
                            {
                                llaves = this._model.sp_trl_getLlaveFromPageAndIdioma(llaveIdioma._llave._pagina._idPagina,llaveIdioma._idioma._idIdioma, usuarioSession._idUsuario, this._idPagina);
                                respuesta.Add("estado", true);
                                respuesta.Add("llaveIdioma", llaveIdioma);
                                respuesta.Add("llaves", llaves);
                            }
                            else
                            {
                                respuesta.Add("estado", false);
                                respuesta.Add("errorType", 3);
                                respuesta.Add("error", "Ocurrio un error al intentar agregar");
                            }
                        }
                        catch (ErroresIUS x)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 1);
                            respuesta.Add("error", x);
                        }
                        catch (Exception x)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 2);
                            respuesta.Add("error", x);
                        }
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_trl_eliminarLlaveIdioma()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        try
                        {
                            bool elimino = this._model.sp_trl_eliminarLlaveIdioma(Convert.ToInt32(frm["idLlaveIdioma"]), usuarioSession._idUsuario, this._idPagina);
                            if (elimino)
                            {
                                respuesta.Add("estado", true);
                            }
                            else
                            {
                                respuesta.Add("estado", false);
                                respuesta.Add("errorType", 3);
                                respuesta.Add("error", "Error no controlado");
                            }
                        }
                        catch (ErroresIUS x)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 1);
                            respuesta.Add("error", x);
                        }
                        catch (Exception x)
                        {
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 2);
                            respuesta.Add("error", x);
                        }
                        
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_trl_actualizarLlaveIdioma()
                {
                    Dictionary<Object, Object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<object, object>();
                        // variables mandar 
                        //int idLlaveIdioma = Convert.ToInt32( frm["txtHdIdLlaveIdioma"].ToString()); int frmIdLlave = Convert.ToInt32(frm["cbEditLlave"].ToString());
                        int idLlaveIdioma = Convert.ToInt32(frm["txtHdIdLlaveIdioma"].ToString()); int frmIdLlave = Convert.ToInt32(frm["txtHdIdLlave"].ToString());
                        int idIdioma = Convert.ToInt32(frm["cbEditIdioma"].ToString()); string traduccion = frm["txtAreaEditTraduccion"].ToString();
                        traduccion = traduccion.Replace("\n", "<br>");
                        bool respuestaModel = this._model.sp_trl_actualizarLlaveIdioma(idLlaveIdioma,frmIdLlave,idIdioma,traduccion,usuarioSession._idUsuario,this._idPagina);
                        respuesta.Add("estado", respuestaModel);
                    }
                    return Json(respuesta);
                }
            #endregion
        #endregion
    }
}
