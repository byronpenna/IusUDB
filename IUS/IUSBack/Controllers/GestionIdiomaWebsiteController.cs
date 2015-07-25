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
            public int _idPagina = (int)paginas.gestionIdiomaWebsite;
            public GestionIdiomaWebsiteModel _model;
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
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
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
                    return error.redirectToError(x, true);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
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
                    if (frm != null && usuarioSession != null) // lo de usuario se puede mejorar
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
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                public ActionResult getObjetosTablita()
                {
                    Dictionary<Object, Object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null)// insistiendo que usuario deberia tener su propio manejo de error
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
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    } 
                    return Json(respuesta);
                }
            #endregion 
            #region "acciones"
                public ActionResult sp_trl_agregarLlaveIdioma()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Llave llave; Idioma idioma; // clases para crear la otra
                    LlaveIdioma llaveIdioma; LlaveIdioma aAgregar;
                    List<Llave> llaves;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null)
                    {
                        respuesta = new Dictionary<object, object>();
                        try
                        {
                            llave = new Llave( Convert.ToInt32(frm["idLlave"].ToString()) );
                            idioma = new Idioma( Convert.ToInt32(frm["idIdioma"].ToString()) );
                            aAgregar = new LlaveIdioma(idioma, llave, frm["traduccion"].ToString());
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
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_trl_eliminarLlaveIdioma()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null) 
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
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_trl_actualizarLlaveIdioma()
                {
                    Dictionary<Object, Object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null) // manejar diferente lo de los usuarios
                    {
                        respuesta = new Dictionary<object, object>();
                        // variables mandar 
                        int idLlaveIdioma = Convert.ToInt32( frm["txtHdIdLlaveIdioma"].ToString()); int frmIdLlave = Convert.ToInt32(frm["cbEditLlave"].ToString());
                        int idIdioma = Convert.ToInt32(frm["cbEditIdioma"].ToString()); string traduccion = frm["txtAreaEditTraduccion"].ToString();
                        bool respuestaModel = this._model.sp_trl_actualizarLlaveIdioma(idLlaveIdioma,frmIdLlave,idIdioma,traduccion,usuarioSession._idUsuario,this._idPagina);
                        respuesta.Add("estado", respuestaModel);
                    }
                    return Json(respuesta);
                }
            #endregion
        #endregion
    }
}
