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
            private int _idPaginaHistoria = (int)paginasInternas.Historia;
            private int _idPaginaOrganizacion = (int)paginasInternas.Organizacion;
            private int _idPaginaIus = (int)paginasInternas.Ius;
            private int _idPaginaSalesianos = (int)paginasInternas.Salesianos;
            private int _idPaginaIdentidad = (int)paginasInternas.Identidad;
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    string lang = this.getUserLang();
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    
                    traducciones = this._model.getTraduccion(lang, this._idPaginaIus);
                    this.setTraduccion(traducciones);

                    traducciones = this._model.getTraduccion(lang, this._idPaginaSalesianos);
                    this.setTraduccion(traducciones);

                    ViewBag.menu21 = this.activeClass;
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
