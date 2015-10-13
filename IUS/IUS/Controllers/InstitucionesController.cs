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
                    string lang = this.getUserLang();
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    string ip = Request.UserHostAddress;
                    ViewBag.instituciones = this._model.sp_frontui_getInstitucionesByContinente(id, ip, this.idPagina);
                    ViewBag.paises = this._model.sp_frontui_getPaisesFromContinente(id, ip, this.idPagina);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    ViewBag.menu22 = this.activeClass;
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
                    string lang = this.getUserLang();
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    ViewBag.menu22 = this.activeClass;
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
