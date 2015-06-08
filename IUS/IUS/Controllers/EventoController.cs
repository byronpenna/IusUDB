using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// internas 
    using IUS.Models.page.Evento.Acciones;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Entidades;
namespace IUS.Controllers
{
    public class EventoController : PadreController
    {
        #region "propiedades"
            public int idPagina = (int)paginas.Eventos;
            public EventoModel _model;
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
                    return View();
                }
                catch (ErroresIUS x)
                {
                    return RedirectToAction("Unhandled", "Errors");
  
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }


            }
        #endregion
        #region "constructores"
            public EventoController()
            {
                this._model = new EventoModel();
            }
        #endregion
    }
}
