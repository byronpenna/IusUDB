﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUS.Models.page.Conocenos.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
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
                    //ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    
                    traducciones = this._model.getTraduccion(lang, this._idPaginaIus);
                    this.setTraduccion(traducciones);

                    traducciones = this._model.getTraduccion(lang, this._idPaginaSalesianos);
                    this.setTraduccion(traducciones);

                    ViewBag.menu21 = this.activeClass;
                    ViewBag.seleccionado = id;
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
                        switch(this.convertObjAjaxToInt(frm["idSeleccion"])){
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
                        }
                        
                        traducciones = this._model.getTraduccion(lang,idPagina);
                        respuesta = this.getDicTraduccion(traducciones,respuesta);
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
