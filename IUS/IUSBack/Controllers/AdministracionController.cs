﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Controllers
{
    public class AdministracionController : PadreController
    {

        #region "propiedades"
            private int _idPaginaEventos = (int)paginas.Eventos;
            private int _idPaginaNoticias = (int)paginas.Noticias;
            private AdministracionModel _model;
        #endregion
        #region "url"
            public ActionResult Eventos()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPaginaEventos);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.permiso     = permisos;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                    
                }
                
            }
            public ActionResult Noticias()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPaginaNoticias);
                    if (permisos != null && permisos._ver)
                    {
                        ViewBag.permiso     = permisos;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
                
            }
        #endregion 
        #region "acciones"
            #region "Eventos"
                public ActionResult sp_adminfe_crearEvento()
                {
                    Dictionary<object, object> frm,respuesta;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        if (usuarioSession != null && usuarioSession != null)
                        {
                            respuesta = new Dictionary<object, object>();
                            
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        respuesta = this.errorTryControlador(1, x);
                    }
                    catch (Exception x)
                    {
                        respuesta = this.errorTryControlador(2, x);
                    }
                    return Json(respuesta);
                }
            #endregion
            #region "Noticias"
                
            #endregion
        #endregion
        #region "constructores"
            public AdministracionController()
            {
                this._model = new AdministracionModel();
            }
        #endregion
    }
}
