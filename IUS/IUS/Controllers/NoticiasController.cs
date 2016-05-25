using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// interno
    using IUS.Models.page.Noticias.Acciones;
// stream
    using System.IO;
// librerias externas 
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.FrontUI.Noticias.Entidades;
    using IUSLibs.SECPU.Entidades;
namespace IUS.Controllers
{
    public class NoticiasController : PadreController
    {
        #region "Propiedades"
            private int idPagina = (int)paginas.Noticias;
            private NoticiaModel _model;
        #endregion
        #region "acciones url"
            public ActionResult getImageThumbnail(int id)
            {
                //string retorno = "";
                try
                {
                    Post post = this._model.sp_adminfe_front_getPicNoticiaFromId(id);
                    //retorno = "data:image/png;base64," + Convert.ToBase64String(post._miniatura, 0, post._miniatura.Length);

                    if (post._miniatura != null)
                    {
                        Stream stream = new MemoryStream(post._miniatura);
                        return new FileStreamResult(stream, "image/jpeg");
                    }
                    else
                    {
                        string path = Server.MapPath("~/Content/images/generales/noBanerMiniatura.png");
                        return base.File(path, "image/jpeg");
                    }
                    //Image image = Image.FromStream(stream);

                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }

            }
            public ActionResult Todas(int id=1,int id2=12)
            {
                /*
                 id:    pagina que se desea visualizar
                 id2:   elementos a mostrar
                 */
                List<LlaveIdioma> traducciones;
                try
                {
                    ViewBag.usuarioSession = this.getUsuarioSession();
                    string lang         = this.getUserLang();
                    traducciones        = this._model.getTraduccion(lang, this.idPagina);
                    string ip = Request.UserHostAddress;
                    this.setTraduccion(traducciones);
                    // por el momento no habra noticias
                    //ViewBag.noticias        = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias, lang);
                    ViewBag.noticiasPagina  = this._model.sp_adminfe_front_getNoticiasPagina(id, id2, lang, ip, this.idPagina);
                    ViewBag.menu25          = this.activeClass;
                    ViewBag.numPage         = id;
                    ViewBag.rango           = id2;
                }
                catch (ErroresIUS x) {
                    ErrorsController error = new ErrorsController();
                    var obj = error.redirectToError(x);
                    return RedirectToAction(obj["accion"], obj["controlador"]);
                }
                catch (Exception x)
                {
                    return RedirectToAction("Unhandled", "Errors");
                }
                return View();
            }
            public ActionResult Index(int id)
            {
                List<LlaveIdioma> traducciones;
                string ip = Request.UserHostAddress;
                try
                {
                    ViewBag.usuarioSession = this.getUsuarioSession();
                    Dictionary<object, object> cuerpoPagina;
                    string lang = this.getUserLang();
                    traducciones = this._model.getTraduccion(lang, this.idPagina);
                    this.setTraduccion(traducciones);
                    cuerpoPagina = this._model.sp_adminfe_front_getNoticiaFromId(id);
                    ViewBag.noticias = this._model.sp_adminfe_front_getTopNoticias(this._numeroNoticias,lang);
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
            public ActionResult sp_adminfe_front_getNoticiasPagina()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    string lang = this.getUserLang();
                    string ip   = Request.UserHostAddress;
                    respuesta = this._model.sp_adminfe_front_getNoticiasPagina(this.convertObjAjaxToInt(frm["pagina"]), this.convertObjAjaxToInt(frm["cn"]), lang, ip, this.idPagina);
                    respuesta.Add("estado", true);
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
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
                        Comentario comentarioAgregar = new Comentario(frm["txtAreaComentario"].ToString(), "", ip, "", this.convertObjAjaxToInt(frm["txtHdIdPost"]));
                        UsuarioPublico usuario = this.getUsuarioSession();
                        if (usuario != null)
                        {
                            Comentario comentarioAgregado = this._model.sp_frontUi_noticias_ponerComentario(comentarioAgregar, usuario._idUsuarioPublico, this.idPagina);
                            if (comentarioAgregado != null)
                            {
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("comentario", comentarioAgregado);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                respuesta = this.errorTryControlador(3, x);
                            }
                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Debe iniciar sesion para poder comentar", ErroresIUS.tipoError.generico, 0);
                            x._mostrar = true;
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
            public ActionResult sp_adminfe_front_buscarNoticias()
            {
                Dictionary<object, object> frm, respuesta;
                frm = this.getAjaxFrm();
                if (frm != null)
                {
                    try
                    {
                        //string ip = this.Request.ServerVariables["REMOTE_ADDR"];
                        string ip = Request.UserHostAddress;
                        Post postBuscar     = new Post();
                        postBuscar._titulo  = frm["txtTituloNoticia"].ToString();
                        string dateIni      = frm["dtpInicio"].ToString();string dateFin = frm["dtpFin"].ToString();
                        postBuscar._fechaFinBusqueda = null;
                        postBuscar._fechaInicioBusqueda = null;
                        try
                        {
                            if (dateIni != "")
                            {
                                postBuscar._fechaInicioBusqueda = this.convertObjAjaxToDateTime(dateIni, "");
                                if (dateFin != "")
                                {
                                    postBuscar._fechaFinBusqueda = this.convertObjAjaxToDateTime(dateFin, "");
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                        string lang = this.getUserLang();
                        respuesta   = this._model.sp_adminfe_front_buscarNoticias(postBuscar, this.convertObjAjaxToInt(frm["txtHdNumPage"]), this.convertObjAjaxToInt(frm["txtHdRango"]), lang, ip, this.idPagina);
                        respuesta.Add("estado", true);
                        /*List<Post> posts = (List<Post>)objPosts["posts"];
                        respuesta = new Dictionary<object,object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("posts", posts);
                        respuesta.Add("")*/
                        
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, 0);
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
