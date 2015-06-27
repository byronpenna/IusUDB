using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUS.Models.page.Instituciones.Acciones;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
namespace IUS.Controllers
{
    public class InstitucionesController : PadreController
    {
        //
        // GET: /Instituciones/
        #region "propiedades"
            public int idPagina = (int)paginas.Instituciones;
            private InstitucionesModel _model;
        #endregion
        #region "acciones url"
            public ActionResult VerInstituciones(int id)
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    //ViewBag.instituciones = this._model.
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
                catch (Exception x) {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();

            }
            
        #endregion
        #region "constructores"
            public InstitucionesController()
            {
                this._model = new InstitucionesModel();
            }
        #endregion
    }
}
