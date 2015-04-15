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
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    List<Idioma> idiomas = this._model.sp_tra_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                    List<Pagina> paginas = this._model.sp_trl_getAllPaginas(usuarioSession._idUsuario, this._idPagina);
                    List<LlaveIdioma> tabla = this._model.sp_trl_tablitaGestionTraduccion(usuarioSession._idUsuario, this._idPagina);
                    // generales
                    ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    // propias de pagina
                    ViewBag.idiomas         = idiomas;
                    ViewBag.paginas         = paginas;
                    ViewBag.tbTraducciones = tabla;
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
                return View();
            }
        #endregion
        #region "ajax"
            public ActionResult sp_trl_getLlaveFromPage()
            {
                Dictionary<Object,Object> frm,respuesta = null;
                frm = this.getAjaxFrm();
                Usuario usuarioSession = this.getUsuarioSesion();
                if (frm != null && usuarioSession != null) // lo de usuario se puede mejorar
                {
                    respuesta = new Dictionary<Object, Object>();
                    try
                    {

                        List<Llave> llaves = this._model.sp_trl_getLlaveFromPage(Convert.ToInt32(frm["idPaginaFront"].ToString()), usuarioSession._idUsuario, this._idPagina);
                        respuesta.Add("estado", true);
                        respuesta.Add("Llaves", llaves);
                    }
                    catch (Exception)
                    {
                        respuesta.Add("estado", false);
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
