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
namespace IUS.Controllers
{
    public class ConocenosController : PadreController
    {
        //
        // GET: /Conocenos/
        #region "propiedades"
            private ConocenosModel _model;
            private int idPagina = (int)paginas.conocenos;
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
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
        #region "constructores"
            public ConocenosController()
            {
                this._model = new ConocenosModel();
            }
        #endregion
    }
}
