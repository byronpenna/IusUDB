using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUS.Models.general;
// librerias externas
    using IUSLibs.LOGS;
namespace IUS.Controllers
{
    public class ErrorsController : PadreController
    {
        //
        // GET: /Erros/
        #region "propiedades"
            private ModeloPadre _PadreModel;
        #endregion
        #region "funciones"
            public ActionResult DisabledPost()
            {
                int idPagina = 5; // cambiar el id de la paginas
                string lang = this.getUserLang();
                this.setTraduccion(this._PadreModel.getTraduccion(lang, idPagina));
                ViewBag.noticias = this._PadreModel.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                return View();
            }
            public ActionResult NotFound()
            {
                // describe el error http 404 
                int idPagina = 5;
                string lang = this.getUserLang();
                this.setTraduccion(this._PadreModel.getTraduccion(lang, idPagina));
                ViewBag.noticias = this._PadreModel.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                return View();
            }
            public ActionResult Unhandled()
            {
                // describe el error http 404 
                int idPagina = 5;
                string lang = this.getUserLang();
                this.setTraduccion(this._PadreModel.getTraduccion(lang, idPagina));
                ViewBag.noticias = this._PadreModel.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                return View();
            }
            public ActionResult DBNotAccess()
            {
                return View();
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
        #region "constructores"
            public ErrorsController()
            {
                this._PadreModel = new ModeloPadre();
            }
        #endregion
    }
}
