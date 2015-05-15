using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUSBack.Controllers
{
    public class NoticiasController : PadreController
    {
        #region "propiedades"
            private int _idPagina = (int)paginas.Noticias;
            private NoticiasModel _model;
        #endregion 
        #region "constructores"
            public NoticiasController()
            {
                this._model = new NoticiasModel();
            }
        #endregion
        #region "url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                if (usuarioSession != null)
                {
                    Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                    if (permisos != null && permisos._ver)
                    {
                        List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                        ViewBag.permiso     = permisos;
                        ViewBag.categorias  = categorias;
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        return View("~/Views/Administracion/Noticias.cshtml");
                    }
                    else
                    {
                        return RedirectToAction("NotAllowed", "Errors");
                    }
                }
                else
                {
                    return RedirectToAction("index", "login");
                }

            }
        #endregion
        #region "acciones ajax"
        /*
            public ActionResult sp_adminfe_noticias_publicarPost()
            {
                Dictionary<object, object> frm, respuesta = null;
                frm                     = this.getAjaxFrm();
                Usuario usuarioSession  = this.getUsuarioSesion();
                if (usuarioSession != null && frm != null)
                {
                    //Post postAgregar = new Post();
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }*/
        #endregion
    }
}
