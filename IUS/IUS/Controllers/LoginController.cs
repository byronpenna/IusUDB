using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// modelos
    using IUS.Models.page.Login.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
namespace IUS.Controllers
{
    public class LoginController : PadreController
    {
        //
        // GET: /Login/
        #region "propiedades"
            public int idPagina = (int)paginas.login;
            private LoginModel _model;
        #endregion
        #region "URL"
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    //Response.Redirect(vista);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                    //return "El error es" + x.Message;
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                
            }
        #endregion
        #region "Constructores"
            public LoginController()
            {
                this._model = new LoginModel();
            }
        #endregion
    }
}
