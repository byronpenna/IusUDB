using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

    using System.Net.Mail;
// Librerias net
    using System.IO;
    using System.Drawing;
// librerias internas
    using IUSBack.Models.Page.Administracion.Acciones;
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.TRL.Entidades;
namespace IUSBack.Controllers
{
    public class NoticiasController : PadreController
    {
        #region "propiedades"
            private int _idPagina = (int)paginas.Noticias;
            private NoticiasModel _model;
            private string _nombreClass = "NoticiasController";
        #endregion 
        #region "constructores"
            public NoticiasController()
            {
                this._model = new NoticiasModel();
            }
        #endregion
        #region "url"
            public ActionResult NoPost()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "NoPost-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "NoPost-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult preview(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try
                {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    Dictionary<object, object> cuerpoPagina = this._model.sp_adminfe_noticias_getPostsFromId(id,usuarioSession._idUsuario,this._idPagina);
                    bool postNull       = (bool)cuerpoPagina["postNull"];
                    Post post           = (Post)cuerpoPagina["post"];
                    if (postNull)
                    {
                        ViewBag.post = post;
                        //ViewBag.postNull    = postNull;
                        ViewBag.origen = 1; //origen solo preview 
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("NotFound", "Errors");
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
                    return error.redirectToError(x, "NoPost-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                try {
                    ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                    if (seguridadInicial != null)
                    {
                        return seguridadInicial;
                    }
                    ViewBag.titleModulo = "Noticias";
                    ViewBag.usuario     = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    List<Post> posts    = this._model.sp_adminfe_noticias_getPosts(usuarioSession._idUsuario, this._idPagina);
                    ViewBag.posts = posts;
                    return View();
                }catch(ErroresIUS x){
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x) {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "Index-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
            }
            
            public ActionResult AprobarNoticia()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    AprobarNoticiasModel modelo = new AprobarNoticiasModel();
                    ViewBag.notiEvento = modelo.sp_adminfe_aprobarnoticia_getNoticiasAprobar(usuarioSession._idUsuario, this._idPagina);
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
            public ActionResult IngresarNoticia()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                    //ViewBag.permiso     = permisos;
                    ViewBag.categorias  = categorias;
                    //ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    ViewBag.menus       = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.editMode    = false;
                    ViewBag.idiomas     = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);
                    #region "Labels"
                        ViewBag.titleModulo     = "Ingresar noticia";
                        ViewBag.usuario         = usuarioSession;
                        ViewBag.botonAccion     = "Guardar";
                        ViewBag.accion          = 1;
                    #endregion
                    ViewBag.post = new Post();
                    return View("~/Views/Administracion/Noticias.cshtml");
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "IngresarNoticia-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                    //return RedirectToAction("Unhandled", "Errors");
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "IngresarNoticia-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                /*}
                else
                {
                    return RedirectToAction("NotAllowed", "Errors");
                }
            }
            else
            {
                return RedirectToAction("index", "login");
            }*/
            }
            public ActionResult ModificarNoticia(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();

                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {

                    ViewBag.selectedMenu = 4; // menu seleccionado 
                    //List<PostCategoria> categorias = this._model.sp_adminfe_noticias_getCategorias(usuarioSession._idUsuario, this._idPagina);
                    Dictionary<object, object> datosPost = this._model.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina);
                    Post post = (Post)datosPost["post"];
                    //ViewBag.permiso = permisos;
                    ViewBag.categorias = this._model.sp_adminfe_noticias_getCategoriasPostById(post._idPost, usuarioSession._idUsuario, this._idPagina);//categorias;
                    //ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    ViewBag.editMode = true;
                    ViewBag.idiomas = this._model.sp_trl_getAllIdiomas(usuarioSession._idUsuario, this._idPagina);

                    #region "Labels"
                    ViewBag.titleModulo = "Modificar noticia";
                    ViewBag.botonAccion = "Modificar";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.accion = 0;
                    #endregion
                    #region "Valores"
                    ViewBag.post = post;
                    ViewBag.tags = this._model.getComaTags((List<Tag>)datosPost["tags"]);
                    #endregion
                    return View("~/Views/Administracion/Noticias.cshtml");
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, true, "ModificarNoticia-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "ModificarNoticia-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
            }
            public ActionResult setMiniatura(int id)
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina, 4);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                try
                {
                    //ViewBag.selectedMenu = 4; // menu seleccionado 
                    ViewBag.titleModulo = "Escoger miniatura foto";
                    ViewBag.usuario = usuarioSession;
                    ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                    //ViewBag.permiso = permisos;
                    ViewBag.post = this._model.sp_adminfe_noticias_getPostsFromId(id, usuarioSession._idUsuario, this._idPagina)["post"];
                    return View();
                }
                catch (ErroresIUS x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "setMiniatura-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }
                catch (Exception x)
                {
                    ErrorsController error = new ErrorsController();
                    return error.redirectToError(x, "setMiniatura-" + this._nombreClass, usuarioSession._idUsuario, this._idPagina);
                }

            }
        #endregion
        #region "generics"
            
            public void enviarCorreo(string email)
            {
                //usuarioAgregado._email,codigo._numero
                string ruta = Request.Url.AbsoluteUri;
                ruta = ruta.Substring(0, this.CustomIndexOf(ruta, '/', 3));
                MailMessage m = new MailMessage();
                m.To.Add(email);
                m.Subject = "Por favor confirmar cuenta IUS";
                m.Body = "para confirmar su cuenta por favor ingrese al siguiente enlace <br>" +
                    ruta + Url.Action("Verificar", "Home");
                m.IsBodyHtml = true;
                m.Priority = MailPriority.Normal;
                SmtpClient cliente = new SmtpClient();
                cliente.Send(m);
            }
            public string getSubject(int op)
            {
                switch (op)
                {
                    case 1:
                        {
                            return "Solicitud de cambio noticias - IUS ";
                        }
                    case 2:
                        {
                            return "No aceptada noticia - IUS";
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
        #endregion
        #region "acciones ajax"
            public ActionResult getImageThumbnail(int id)
            {
                //string retorno = "";
                try
                {
                    Post post = this._model._controlPost.sp_adminfe_front_getPicNoticiaFromId(id);
                    //retorno = "data:image/png;base64," + Convert.ToBase64String(post._miniatura, 0, post._miniatura.Length);
                    
                    if(post != null && post._miniatura != null){
                        Stream stream = new MemoryStream(post._miniatura);
                        return new FileStreamResult(stream, "image/jpeg");
                    }
                    else
                    {
                        // /Content/themes/iusback_theme/img/general/noBanerMiniatura.png
                        string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                        return base.File(path, "image/jpeg");
                    }
                    //Image image = Image.FromStream(stream);
                    
                }
                catch (ErroresIUS x)
                {
                    //throw x;
                    string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                    return base.File(path, "image/jpeg");
                }
                catch (Exception x)
                {
                    //throw x;
                    string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                    return base.File(path, "image/jpeg");
                }
                
            }
            public ActionResult sp_adminfe_noticias_setThumbnailPost()
            {
                Dictionary<object, object> frm, 
                    
                    respuesta = null;
                try
                {
                    var form = this._jss.Deserialize<Dictionary<object,object>>(Request.Form["form"]);
                    Usuario usuarioSession = this.getUsuarioSesion();
                    respuesta = this.seguridadInicialAjax(usuarioSession, form);
                    if (respuesta == null)
                    {
                        if (Request.Files.Count > 0)
                        {
                            List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                            if (files != null)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {
                                    //
                                    /*decimal xx = this.convertObjAjaxToDecimal(form["x"]); decimal yy = this.convertObjAjaxToDecimal(form["y"]);
                                    decimal xancho = this.convertObjAjaxToDecimal(form["imgAncho"]); decimal yalto = this.convertObjAjaxToDecimal(form["imgAlto"]);
                                    MemoryStream memory = new MemoryStream();
                                    byte[] fileBytes = this.getBytesFromFile(file); ;
                                    using (Image image = Image.FromStream(file.InputStream))
                                    {
                                        if (image.Width != image.Height || (xancho > 0 && yalto > 0 && xancho > 0))
                                        {
                                            int x = decimal.ToInt32(image.Width * xx);
                                            int y = decimal.ToInt32(image.Height * yy);
                                            int ancho = decimal.ToInt32(image.Height * xancho);
                                            int alto = decimal.ToInt32(image.Height * yalto);
                                            Rectangle cropArea = new Rectangle(x, y, ancho, ancho);
                                            try
                                            {
                                                using (Bitmap bitMap = new Bitmap(cropArea.Width, cropArea.Height))
                                                {
                                                    using (Graphics g = Graphics.FromImage(bitMap))
                                                    {
                                                        g.DrawImage(image, new Rectangle(0, 0, bitMap.Width, bitMap.Height), cropArea, GraphicsUnit.Pixel);
                                                    }
                                                    bitMap.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                }
                                                fileBytes = memory.ToArray(); //
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }

                                    }*/
                                    Coordenadas coordenadas = new Coordenadas(this.convertObjAjaxToDecimal(form["x"]),this.convertObjAjaxToDecimal(form["y"]),this.convertObjAjaxToDecimal(form["imgAncho"]),this.convertObjAjaxToDecimal(form["imgAlto"]));
                                    byte[] fileBytes = this.getBytesRecortadosFromFile(file,coordenadas,false);
                                    Post postAgregar = new Post(this.convertObjAjaxToInt(form["txtHdIdPost"]));
                                    postAgregar._miniatura = fileBytes;
                                    bool estado = this._model.sp_adminfe_noticias_setThumbnailPost(postAgregar, usuarioSession._idUsuario, this._idPagina);
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", estado);
                                    respuesta.Add("id", postAgregar._idPost);
                                }
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("No hay imagenes", ErroresIUS.tipoError.generico, 0);
                                throw x;
                            }
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                    }
                    
                }
                catch (ErroresIUS x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                    respuesta = this.errorTryControlador(1, error);
                }
                catch (Exception x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                    respuesta = this.errorTryControlador(2, error);
                }
                return Json(respuesta);
            }
            public ActionResult sp_adminfe_noticias_cambiarEstadoPost()
            {
                Dictionary<object, object> frm, respuesta ;
                frm = this.getAjaxFrm();
                Usuario usuarioSession = this.getUsuarioSesion();
                
                respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                if (respuesta == null)
                {
                    try
                    {
                        Post post = this._model.sp_adminfe_noticias_cambiarEstadoPost(this.convertObjAjaxToInt(frm["idPost"]), usuarioSession._idUsuario, this._idPagina);
                        if (post != null)
                        {
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("post", post);

                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                            respuesta = this.errorTryControlador(3, x);
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                        respuesta = this.errorTryControlador(1, error);
                    }
                    catch (Exception x)
                    {
                        ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                        respuesta = this.errorTryControlador(2, error);
                    }
                }
                return Json(respuesta);
            }
            [ValidateInput(false)]
            public ActionResult sp_adminfe_noticias_publicarPost()
            {
                // vars 
                Dictionary<object, object> frm,respuestaTag,respuestaCate, respuesta = null;
                frm = this.getAjaxFrmWithOutValidate();
                Usuario usuarioSession  = this.getUsuarioSesion();
                // do it
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        try
                        {
                            Idioma idioma = new Idioma(this.convertObjAjaxToInt(frm["cbIdioma"]));
                            Post postAgregar = new Post(frm["txtTitulo"].ToString(), frm["contenido"].ToString(), usuarioSession,idioma);
                            postAgregar._descripcion = frm["txtDescripcion"].ToString();
                            Post postAgregado = this._model.sp_adminfe_noticias_publicarPost(postAgregar, usuarioSession._idUsuario, this._idPagina);
                            //Post postAgregado = null;
                            if (postAgregado != null)
                            {
                                
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("post", postAgregado);
                                // agregar tags
                                    string[] tags;
                                    try
                                    {
                                        tags = this.converArrAajaxToString((object[])frm["tags"]);
                                    }
                                    catch (Exception x)
                                    {
                                        string strTag = frm["tags"].ToString();
                                        if (strTag == "")
                                        {
                                            tags = null;
                                        }else{
                                            tags = new string[1];
                                            tags[0] = strTag;
                                        }
                                    }
                                    if (tags != null)
                                    {
                                        respuestaTag = this._model.sp_adminfe_noticias_agregarTag(postAgregado._idPost, tags, usuarioSession._idUsuario, this._idPagina);
                                        respuesta.Add("respuestaTag", respuestaTag);
                                    }
                                    else
                                    {
                                        respuesta.Add("respuestaTag", null);
                                    }
                                // agregar categoria 
                                    int[] categorias;
                                    try
                                    {
                                        categorias = this.convertArrAjaxToInt((object[])frm["cbCategorias"]);
                                    }
                                    catch (Exception)
                                    {
                                        categorias = new int[1];
                                        categorias[0] = this.convertObjAjaxToInt(frm["cbCategorias"]);
                                    }
                                    if (categorias != null)
                                    {
                                        respuestaCate = this._model.sp_adminfe_noticias_insertCategoriasPosts(postAgregado._idPost, categorias, usuarioSession._idUsuario, this._idPagina);
                                        respuesta.Add("respuestaCate", respuestaCate);
                                    }
                                    else
                                    {
                                        respuesta.Add("respuestaCate", null);
                                    }

                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                respuesta = errorTryControlador(3, x);
                            }
                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                            respuesta = errorTryControlador(1, error);

                        }
                        catch (Exception x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                    

                    
                    }
                // return 
                return Json(respuesta);
            }
            #region "Modificar"
                public ActionResult sp_adminfe_noticias_modificarPost()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    frm = this.getAjaxFrmWithOutValidate();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        try
                        {
                            int idPost          = this.convertObjAjaxToInt(frm["txtHdIdPost"]);
                            /*********************/
                                //int[] idCategorias  = this.convertArrAjaxToInt( (object[]) frm["cbCategorias"]);
                                int[] idCategorias;
                                try
                                {
                                    idCategorias = this.convertArrAjaxToInt((object[])frm["cbCategorias"]);
                                }
                                catch (Exception)
                                {
                                    idCategorias = new int[1];
                                    idCategorias[0] = this.convertObjAjaxToInt(frm["cbCategorias"]);
                                }
                            /*****************/
                            Idioma idioma       = new Idioma(this.convertObjAjaxToInt(frm["cbIdioma"]));
                            Post postActualizar = new Post(idPost, frm["txtTitulo"].ToString(), frm["contenido"].ToString());
                            postActualizar._idioma      = idioma;
                            postActualizar._descripcion = frm["txtDescripcion"].ToString();
                            string tags         = frm["tags"].ToString();
                            bool actualizo      = this._model.sp_adminfe_noticias_modificarPost(postActualizar, usuarioSession._idUsuario, this._idPagina);
                            List<Tag> tagList   = this._model.sp_adminfe_noticias_updateTag(tags, idPost, usuarioSession._idUsuario, this._idPagina);
                            List<CategoriaPost> categorias = this._model.sp_adminfe_noticias_updateCategoriaPost(idCategorias, idPost, usuarioSession._idUsuario, _idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", actualizo);
                            respuesta.Add("tags", tagList);
                        }
                        catch (ErroresIUS x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                    }
                    return Json(respuesta);
                }
            #endregion
        #endregion
    }
}
