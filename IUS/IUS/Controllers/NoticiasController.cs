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
    using IUSLibs.FrontUI.Noticias.Entidades;
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
                string ip = Request.UserHostAddress;
                try
                {
                    Dictionary<object, object> cuerpoPagina;
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    cuerpoPagina = this._model.sp_adminfe_front_getNoticiaFromId(id);
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias);
                    ViewBag.idPost = id;
                    ViewBag.comentarios = this._model.sp_frontUi_noticias_getComentariosPost(id, ip, this.idPagina);
                    
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
        #region "acciones ajax"
            public ActionResult sp_frontUi_noticias_ponerComentario()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        //string ip = this.Request.ServerVariables["REMOTE_ADDR"];
                        string ip = Request.UserHostAddress;
                        Comentario comentarioAgregar = new Comentario(frm["txtAreaComentario"].ToString(), frm["txtEmail"].ToString(), ip, frm["txtNombre"].ToString(), this.convertObjAjaxToInt(frm["txtHdIdPost"]));
                        Comentario comentarioAgregado = this._model.sp_frontUi_noticias_ponerComentario(comentarioAgregar, this.idPagina);
                        if (comentarioAgregado != null)
                        {
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("comentario", comentarioAgregado);
                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                            respuesta = this.errorTryControlador(3, x);
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,0);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
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
