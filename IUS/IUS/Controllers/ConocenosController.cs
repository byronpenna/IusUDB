using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUS.Models.page.Conocenos.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
namespace IUS.Controllers
{
    public class ConocenosController : PadreController
    {
        //
        // GET: /Conocenos/
        #region "propiedades"
            private ConocenosModel _model;
            private int idPagina = (int)paginas.conocenos;
            private int _idPaginaHistoria = (int)paginasInternas.Historia;
            private int _idPaginaOrganizacion = (int)paginasInternas.Organizacion;
            private int _idPaginaIus = (int)paginasInternas.Ius;
            private int _idPaginaSalesianos = (int)paginasInternas.Salesianos;
            private int _idPaginaIdentidad = (int)paginasInternas.Identidad;
        #endregion
        #region "acciones url"
            public ActionResult Index(int id=-1)
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.usuarioSession = this.getUsuarioSession();
                    string lang = this.getUserLang();
                    string ip = Request.UserHostAddress;
                    //################################ prestar atencion aqui y en login #
                    ViewBag.notiEvento = this._model.sp_adminfe_front_pantallaHome(3,1, ip, this.idPagina);
                    //ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    
                    traducciones = this._model.getTraduccion(lang, this._idPaginaIus);
                    this.setTraduccion(traducciones);

                    traducciones = this._model.getTraduccion(lang, this._idPaginaSalesianos);
                    this.setTraduccion(traducciones);

                    ViewBag.menu12 = this.activeClass;
                    ViewBag.seleccionado = id;

                    /*
                     1: Historia 
                     2: Identidad 
                     3: Organización 
                     4: Ius
                     5: Salesianos
                     */
                    ViewBag.idHistoria      = this._idPaginaHistoria;
                    ViewBag.idIdentidad     = this._idPaginaIdentidad;
                    ViewBag.idOrganizacion  = this._idPaginaOrganizacion;
                    ViewBag.idIus           = this._idPaginaIus;
                    ViewBag.idSalesianos    = this._idPaginaSalesianos;

                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View("~/Views/Conocenos/Indexi.cshtml");
            }
        #endregion
        #region "acciones ajax"
            public ActionResult descargarDocumento(int id)
            {
                ViewBag.usuarioSession = this.getUsuarioSession();
                string ip = Request.UserHostAddress;
                //VersionDocumentoOficial documentoOficial = this._model.
                return null;
            }
            public ActionResult getDocumentosByIdioma()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        string  lang        = this.getUserLang();
                                respuesta   = new Dictionary<object, object>();
                        int     idPagina    = this.idPagina;
                        string  ip          = Request.UserHostAddress;
                        List<VersionDocumentoOficial> documentosOficiales = this._model.sp_adminfe_front_getDocumentosOficiales(lang, ip, idPagina);
                        respuesta.Add("estado", true);
                        respuesta.Add("documentosOficiales",documentosOficiales);

                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
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
            public ActionResult getInfo()
            {
                /*
                 1: Historia 
                 2: Identidad 
                 3: Organización 
                 4: Ius
                 5: Salesianos
                 */
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        List<LlaveIdioma> traducciones; 
                        string  lang        = this.getUserLang();
                                respuesta   = new Dictionary<object, object>();
                        int     idPagina    = -1;
                        string  ip          = Request.UserHostAddress;
                        ViewBag.menu12      = this.activeClass;
                        idPagina = this.convertObjAjaxToInt(frm["idSeleccion"]);
                        /*switch(this.convertObjAjaxToInt(frm["idSeleccion"])){
                            case 1:{
                                idPagina = this._idPaginaHistoria;
                                break;
                            }
                            case 2:{
                                idPagina = this._idPaginaIdentidad;
                                break;
                            }
                            case 3:{
                                idPagina = this._idPaginaOrganizacion;
                                break;
                            }
                            case 4:
                                {
                                    idPagina = this._idPaginaIus;
                                    respuesta.Add("datosIus", this._model.sp_adminfe_front_getDatosIUS(ip, idPagina));
                                    break;
                                }
                            case 5:
                                {
                                    idPagina    = this._idPaginaSalesianos;
                                    
                                    respuesta.Add("datosIus", this._model.sp_adminfe_front_getDatosSalesianos(ip, idPagina));
                                    break;
                                }
                        }*/
                        
                        traducciones = this._model.getTraduccion(lang,idPagina);
                        respuesta = this.getDicTraduccion(traducciones,respuesta);
                        respuesta.Add("urlImage", Url.Content("~/Content/images/generales/conocenos/"));
                        respuesta.Add("estado", true);

                        //respuesta.Add("traducciones", traducciones);
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
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
        #region "constructores"
            public ConocenosController()
            {
                this._model = new ConocenosModel();
            }
        #endregion
    }
}
