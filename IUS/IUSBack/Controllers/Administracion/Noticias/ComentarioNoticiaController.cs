using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
// externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers.Administracion.Noticias
{
    public class ComentarioNoticiaController : PadreController
    {
        #region "propiedades"
            public int              _idPagina       = (int)paginas.Noticias;
            public string           _nombreClass    = "ComentarioNoticiaController";
            public ComentarioModel  _model;
        #endregion
        #region "acciones url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    ViewBag.titleModulo = "Comentarios";
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
            }
        #endregion
        #region "constructores"
            public ComentarioNoticiaController()
            {
                this._model = new ComentarioModel();

            }
        #endregion
    }
}
