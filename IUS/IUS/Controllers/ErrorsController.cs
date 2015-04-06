using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias externas
    using IUSLibs.LOGS;
namespace IUS.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Erros/

        public ActionResult NotFound()
        {
            // describe el error http 404 
            return View();
        }
        public ActionResult DBNotAccess()
        {
            return View();
        }
        #region "redirigir errores a sus respectivas vistas"
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
    }
}
