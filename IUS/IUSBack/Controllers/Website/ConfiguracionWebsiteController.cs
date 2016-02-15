using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.ConfiguracionWebsite.Acciones;
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
// subir
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
namespace IUSBack.Controllers
{
    public class ConfiguracionWebsiteController : PadreController
    {
        //
        // GET: /ConfiguracionWebsite/
        #region "propiedades"
            public ConfiguracionWebsiteModel _model;
            private int                     _idPagina       = (int)paginas.configuracionFront;
            private string                  _nombreClass    = "ConfiguracionWebsiteController";
        #endregion
        #region "url"
            public ActionResult Index()
            {
                Usuario usuarioSession = this.getUsuarioSesion();
                ActionResult seguridadInicial = this.seguridadInicial(this._idPagina);
                if (seguridadInicial != null)
                {
                    return seguridadInicial;
                }
                ViewBag.datosIUS = (DatosIUS)this._model.getDatosIus();
                ViewBag.selectedMenu = 3; // menu seleccionado 
                ViewBag.titleModulo = "Configuración Web Site";
                ViewBag.usuario = usuarioSession;
                ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                List<RedSocial> redesSociales = null;
                Configuracion config = null;
                List<Valor> valores = null;
                List<SliderImage> slider = null;
                try
                {
                    Dictionary<object, object> dic = this._model.sp_adminfe_getConfiguraciones(usuarioSession._idUsuario, this._idPagina);
                    slider = this._model.sp_adminfe_getSliderImage(1, usuarioSession._idUsuario, this._idPagina);
                    if (dic != null)
                    {
                        config = (Configuracion)dic["configuracion"];
                        redesSociales = (List<RedSocial>)dic["redesSociales"];
                        valores = (List<Valor>)dic["valores"];
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
                ViewBag.redesSociales = redesSociales;
                ViewBag.config = config;
                ViewBag.valores = valores;
                ViewBag.slider = slider;
                return View();
            }
            public ActionResult sliderImageFromId(int id=-1)
            {
                
                if (id != -1)
                {
                    try
                    {
                        SliderImage sliderImage = this._model.sp_adminfe_getImageSliderFromId(id);
                        if (sliderImage != null)
                        {
                            Stream stream = new MemoryStream(sliderImage._imagen);
                            return new FileStreamResult(stream, "image/jpeg");
                        }
                        else
                        {
                            string path = Server.MapPath("/Content/themes/iusback_theme/img/general/image.png");
                            return base.File(path, "image/jpeg");
                        }
                    }
                    catch (ErroresIUS)
                    {
                        string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noimage.png");
                        return base.File(path, "image/jpeg");
                    }
                    catch (Exception)
                    {
                        string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noimage.png");
                        return base.File(path, "image/jpeg");
                    }
                }
                else
                {
                    string path = Server.MapPath("/Content/themes/iusback_theme/img/general/noimage.png");
                    return base.File(path, "image/jpeg");
                }
            }
        #endregion
        #region "generic"
            
        #endregion
        #region "ajax"
            #region "acciones"
                #region "slider"
                    [HttpPost]
                    public ActionResult UploadHomeReport(string id)
                    {
                        List<byte[]> images = new List<byte[]>();
                        Dictionary<object, object> respuesta;
                        SliderImage imageAgregar,imageAgregada;
                        List<SliderImage> sliderAgregar = null;//,sliderAgregado=null;
                        var form    = Request.Files["form"];
                        //var frm     = Request.Form["form"];
                        var frm     = this._jss.Deserialize<Dictionary<object, object>>(Request.Form["form"]);
                        var vari    = Request.Form["frm"];
                        try
                        {
                            
                            if (Request.Files.Count > 0)
                            {
                                List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                                if (files.Count > 0)
                                {
                                    sliderAgregar = new List<SliderImage>();
                                    foreach (HttpPostedFileBase file in files)
                                    {
                                        Coordenadas coordenadas = new Coordenadas(this.convertObjAjaxToDecimal(frm["x"]), this.convertObjAjaxToDecimal(frm["y"]), this.convertObjAjaxToDecimal(frm["imgAncho"]), this.convertObjAjaxToDecimal(frm["imgAlto"]));
                                        byte[] fileBytes = this.getBytesRecortadosFromFile(file, coordenadas,false);
                                        Pagina pagina = new Pagina(1);
                                        imageAgregar = new SliderImage(file.FileName, fileBytes, true, pagina);
                                        sliderAgregar.Add(imageAgregar);
                                    }
                                    Usuario usuarioSesion = this.getUsuarioSesion(); 
                                    imageAgregada = this._model.sp_adminfe_saveImageSlider(sliderAgregar[0], usuarioSesion._idUsuario, this._idPagina);
                                    //imageAgregada = null;
                                    if (imageAgregada != null)
                                    {
                                        respuesta = new Dictionary<object, object>();
                                        respuesta.Add("estado", true);
                                        respuesta.Add("archivos", imageAgregada);
                                        //respuesta.Add("archivos", "bla bla bla");
                                    }
                                    else
                                    {
                                        ErroresIUS x = new ErroresIUS("Error no controlado", ErroresIUS.tipoError.generico, 0);
                                        respuesta = errorTryControlador(3, x);
                                    }
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("No hay imagenes", ErroresIUS.tipoError.generico, 0);
                                    throw x;
                                }
                                /*List<HttpPostedFileBase> archivos = this.getBaseFileFromRequest(Request);
                                sliderAgregar = new List<SliderImage>();
                                foreach (HttpPostedFileBase archivo in archivos)
                                {
                                    byte[] fileBytes = this.getBytesFromFile(archivo);
                                    Pagina pagina = new Pagina(1);
                                    imageAgregar = new SliderImage(archivo.FileName, fileBytes, true, pagina);
                                    sliderAgregar.Add(imageAgregar);
                                }
                                Usuario usuarioSesion = this.getUsuarioSesion();
                                imageAgregada = this._model.sp_adminfe_saveImageSlider(sliderAgregar[0], usuarioSesion._idUsuario, this._idPagina);
                                //imageAgregada = null;
                                if (imageAgregada != null)
                                {
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                    respuesta.Add("archivos", imageAgregada);
                                    //respuesta.Add("archivos", "bla bla bla");
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                    respuesta = errorTryControlador(3, x);
                                }*/
                            }
                            else
                            {
                                respuesta = this.errorEnvioFrmJSON();
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
                    
                    public ActionResult sp_adminfe_cambiarEstado()
                    {
                        Dictionary<object, object> frm, respuesta;
                        frm                         = this.getAjaxFrm();
                        Usuario usuarioSession      = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try{
                                SliderImage image = this._model.sp_adminfe_cambiarEstado(this.convertObjAjaxToInt(frm["txtHdIdSliderImage"]), usuarioSession._idUsuario, this._idPagina);
                                if (image != null)
                                {
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                    respuesta.Add("image",image);
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("error no controlado", ErroresIUS.tipoError.generico, 0);
                                    respuesta = this.errorTryControlador(3, x);
                                }
                            }catch(ErroresIUS x){
                                ErroresIUS error    = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                                respuesta           = this.errorTryControlador(1, error);
                            }catch(Exception x){
                                ErroresIUS error    = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                                respuesta           = this.errorTryControlador(2, error);
                            }
                            
                            
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_adminfe_eliminarImagenSlider()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try{
                                bool estado = this._model.sp_adminfe_eliminarImagenSlider(this.convertObjAjaxToInt(frm["txtHdIdSliderImage"]), usuarioSession._idUsuario, this._idPagina);
                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", estado);
                            }catch(ErroresIUS x){
                                ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber,x._errorSql);
                                respuesta = this.errorTryControlador(1,error);
                            }catch(Exception x){
                                ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                                respuesta = this.errorTryControlador(2,error);
                            }
                            
                        }
                        return Json(respuesta);
                    }
                #endregion
                #region "basicas"
                    public ActionResult sp_adminfe_eliminarValoresConfig()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try{
                                int idValor = this.convertObjAjaxToInt(frm["txtIdValor"]);
                                bool estado = this._model.sp_adminfe_eliminarValoresConfig(idValor, usuarioSession._idUsuario, this._idPagina);
                            
                                if (estado)
                                {
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                    respuesta = this.errorTryControlador(3, x);
                                }
                            }catch(ErroresIUS x){
                                ErroresIUS error = new ErroresIUS(x.Message,x.errorType,x.errorNumber);
                                respuesta = this.errorTryControlador(1,error);
                            }catch(Exception x){
                                ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                                respuesta = this.errorTryControlador(2,error);
                            }
                        
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_adminfe_agregarValoresConfig()
                    {
                        Dictionary<object, object> frm, respuesta;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try
                            {
                                Valor valorAgregado, valorAgregar = new Valor(frm["txtValores"].ToString());
                                valorAgregado = this._model.sp_adminfe_agregarValoresConfig(valorAgregar, usuarioSession._idUsuario, this._idPagina);
                                respuesta = new Dictionary<object,object>();
                                if (valorAgregado != null)
                                {
                                    respuesta.Add("estado", true);
                                    respuesta.Add("valor", valorAgregado);
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Ocurrio un error", ErroresIUS.tipoError.generico, 0);
                                    respuesta = this.errorTryControlador(3, x);
                                }
                            }
                            catch (ErroresIUS x)
                            {
                                ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql,x._mostrar);
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
                    public ActionResult sp_adminfe_actualizarInfoConfig()
                    {
                        Dictionary<object, object> frm, respuesta;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try
                            {
                                Configuracion confActualizar = new Configuracion(frm["txtAreaVision"].ToString(), frm["txtAreaMision"].ToString(), frm["txtAreaHistoria"].ToString());
                                Configuracion configActualizada = this._model.sp_adminfe_actualizarInfoConfig(confActualizar, usuarioSession._idUsuario, this._idPagina);
                                if (configActualizada != null)
                                {
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                    respuesta.Add("configuracion", configActualizada);

                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Error desconocido", ErroresIUS.tipoError.generico, 0);
                                    x._mostrar = true;
                                    respuesta = errorTryControlador(3, x);
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
                    

                        }
                        return Json(respuesta);
                    }
                #endregion
            #endregion
        #endregion
        #region "constructores"
            public ConfiguracionWebsiteController()
            {
                this._model = new ConfiguracionWebsiteModel();
            }
        #endregion

    }
}
