using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// interno
    using IUS.Models.page.Noticias.Acciones;
// librerias externas 
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUS.Controllers
{
    public class NoticiasController : PadreController
    {
        #region "Propiedades"
            private int idPagina = (int)paginas.Noticias;
            private NoticiaModel _model;
        #endregion
        #region "acciones url"
            public ActionResult Index(int id)
            {
                List<LlaveIdioma> traducciones;
                try
                {
                    Dictionary<object, object> cuerpoPagina;
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    cuerpoPagina = this._model.sp_adminfe_front_getNoticiaFromId(id);
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    Post post = (Post)cuerpoPagina["post"];
                    if (post._estado) {
                        ViewBag.post = post;
                        ViewBag.tags = cuerpoPagina["tags"];
                        return View();   
                    }
                    else
                    {
                        return RedirectToAction("DisabledPost", "Errors");
                    }
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    //Response.Redirect(vista);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
            }
        #endregion
        #region "constructores"
            public NoticiasController()
            {
                this._model = new NoticiaModel();
            }
        #endregion
    }
}
