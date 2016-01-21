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
            public ActionResult Index(int id)
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
                    ViewBag.comentarios = this._model.sp_frontUi_noticias_back_getComentariosPost(id, usuarioSession._idUsuario, this._idPagina);
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
        #region "ajax"
            public ActionResult sp_frontUi_noticias_back_delComentarioPost()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        bool elimino = this._model.sp_frontUi_noticias_back_delComentarioPost(this.convertObjAjaxToInt(frm["idComentario"]), usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", elimino);
                    }
                }
                catch (ErroresIUS x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                    respuesta = this.errorTryControlador(1, error);
                }
                catch (Exception x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                    respuesta = this.errorTryControlador(2, error);
                }
                
                return Json(respuesta);
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
