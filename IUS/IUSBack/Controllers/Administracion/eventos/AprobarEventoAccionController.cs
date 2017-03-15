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
        public ActionResult preview(int id=-1,int id2=1)
        {
            /*
             id2 = 1 viene solo evento normal sin publicar
             id2 = 2 viene publicado se desea revisar
             */
            ControlEvento control = new ControlEvento();
            Usuario usuarioSession = this.getUsuarioSesion();
            ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
            if (seguridadInicial != null)
            {
                return seguridadInicial;
            }
            try
            {
                if (id != -1)
                {
                    
                    ControlPublicacionEvento controlPublicacion = new ControlPublicacionEvento();
                    PublicacionEvento publicacionEvento = null;
                    if (id2 == 1)
                    {
                        // aqui id se ocupa como idEvento
                        EventoWebsite eventoWeb = new EventoWebsite(-1);
                        Evento evento = control.sp_adminfe_getEventById(id, usuarioSession._idUsuario, this._idPagina);
                        eventoWeb._evento = evento;
                        publicacionEvento = new PublicacionEvento(-1);
                        publicacionEvento._eventoWeb = eventoWeb;
                    }
                    else
                    {
                        // aqui id se ocupa como idPublicacionEvento
                        publicacionEvento = controlPublicacion.sp_adminfe_getPublicacionEventoById(id, usuarioSession._idUsuario, this._idPagina);
                    }
                    ViewBag.eventoPublicado = publicacionEvento;
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
