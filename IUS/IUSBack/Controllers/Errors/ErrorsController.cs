﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas 
    using IUSBack.Models.Page.Errors;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers
{
    public class ErrorsController : PadreController
    {
        #region "propiedades"
            ErrorsModel _model;
        #endregion
        #region "constructores"
            public ErrorsController()
            {
                this._model = new ErrorsModel();
            }
        #endregion
        #region "actions results"
            #region "redirectToError"
                public ActionResult redirectToError(ErroresIUS x,bool si)
                {
                    //bool error = this._model.sp_sec_registrarError(x.Message)
                    if (x.errorNumber == -5)
                    {
                        return RedirectToAction("NotFolderFound", "Repositorio");
                    }
                    else
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                }
                public ActionResult redirectToError(ErroresIUS x, bool si,string detalle,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = this._model.sp_sec_registrarError(x.Message, detalle, idUsuarioEjecutor, idPagina);
                    if (x.errorNumber == -5)
                    {
                        return RedirectToAction("NotFolderFound", "Repositorio");
                    }
                    else
                    {
                        return RedirectToAction("Unhandled", "Errors");
                    }
                }
                public ActionResult redirectToError(Exception x, string detalle, int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = this._model.sp_sec_registrarError(x.Message, detalle, idUsuarioEjecutor, idPagina);
                    return RedirectToAction("Unhandled", "Errors");
                }
                public Dictionary<String, String> redirectToError(ErroresIUS x)
                {
                    var accion = new Dictionary<String, String>();
                    if (x.errorType == ErroresIUS.tipoError.sql)
                    {
                        switch (x.errorNumber)
                        {
                            case 4060:
                                {
                                    // los parametros de conexion no son validos
                                    accion.Add("controlador", "Errors");
                                    accion.Add("accion", "DBNotAccess");
                                    break;
                                }
                        }
                    }
                    if (accion.Count == 0)
                    {
                        accion.Add("controlador", "Errors");
                        accion.Add("accion", "Unhandled");
                    }
                    return accion;
                }
            #endregion

            public ActionResult Index()
            {
                return View();
            }
            public ActionResult NotFound()
            {
                // describe el error http 404 
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    ViewBag.titleModulo = "Pagina solicitada no existe";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
            }
            public ActionResult NotAllowed()
            {
                
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    ViewBag.titleModulo = "Acceso prohibido";
                    ViewBag.usuario     = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
                
            }
            public ActionResult DBNotAccess()
            {
                return View();
            }
            public ActionResult Unhandled()
            {
                return View();
            }
            [HttpPost]
            public ActionResult exceptionAjax()
            {
                return Json("");
            }
            public ActionResult exceptionAjax(object x)
            {
                return Json(x);
            }
        #endregion
        #region " funciones" 
            
        #endregion
    }
}
