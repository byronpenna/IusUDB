using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Control;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Controllers.Administracion.eventos
{
    public class AprobarEventoAccionController : PadreController
    {
        #region "propiedades"
        private int _idPagina = (int)paginas.Noticias;
        private string _nombreClass = "AprobarEventoAccionController";
        #endregion
        #region "url"
        public ActionResult preview(int id=-1)
        {
            ControlEvento control = new ControlEvento();
            Usuario usuarioSession = this.getUsuarioSesion();
            try
            {
                if (id != -1)
                {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    Evento evento = control.sp_adminfe_getEventById(id, usuarioSession._idUsuario, this._idPagina);
                    ViewBag.evento = evento;
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    return View();
                }
                else
                {
                    return null;
                }
                
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
        public ActionResult Index()
        {
            return View();
        }

    }
}
