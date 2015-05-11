using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas
    using IUSBack.Models.Page.ConfiguracionWebsite.Acciones;
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
            private int _idPagina = (int)paginas.configuracionFront;
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
                        ViewBag.permiso = permisos;
                        ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
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
                        catch (ErroresIUS)
                        {

                            return RedirectToAction("Unhandled", "Errors");
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("Unhandled", "Errors");
                        }
                        ViewBag.redesSociales = redesSociales;
                        ViewBag.config  = config;
                        ViewBag.valores = valores;
                        ViewBag.slider = slider; 
                        return View();
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
        #region "generic"
            
        #endregion
        #region "ajax"
            #region "acciones"
                [HttpPost]
                public ActionResult UploadHomeReport(string id)
                {
                    List<byte[]> images = new List<byte[]>();
                    Dictionary<object, object> respuesta;
                    SliderImage imageAgregar,imageAgregada;
                    List<SliderImage> sliderAgregar = null,sliderAgregado=null;
                    try
                    {
                        //HttpPostedFileBase archivo;
                        if (Request.Files.Count > 0)
                        {
                            List<HttpPostedFileBase> archivos = this.getBaseFileFromRequest(Request);
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
                            if (imageAgregada != null)
                            {

                                respuesta = new Dictionary<object, object>();
                                respuesta.Add("estado", true);
                                respuesta.Add("archivos", sliderAgregado);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlado",ErroresIUS.tipoError.generico,0);
                                respuesta = errorTryControlador(3, x);
                            }
                            /*sliderAgregar   = new List<SliderImage>();
                            sliderAgregado  = new List<SliderImage>(); 
                            foreach (string file in Request.Files)
                            {
                                var fileContent = Request.Files[file];
                                archivo = fileContent;
                                Stream fileStream = archivo.InputStream;
                                var mStreamer = new MemoryStream();
                                mStreamer.SetLength(fileStream.Length);
                                fileStream.Read(mStreamer.GetBuffer(), 0, (int)fileStream.Length);
                                mStreamer.Seek(0, SeekOrigin.Begin);
                                byte[] fileBytes = mStreamer.GetBuffer();
                                Pagina pagina = new Pagina(1);
                                imageAgregar = new SliderImage(archivo.FileName, fileBytes, true, pagina);
                                sliderAgregar.Add(imageAgregar);
                            }
                            Usuario usuarioSesion = this.getUsuarioSesion();
                            foreach(sliderAgregar)
                            imageAgregada = this._model.sp_adminfe_saveImageSlider(imageAgregar, usuarioSesion._idUsuario, this._idPagina);*/

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
                [HttpPost]
                public ActionResult UploadSlider()
                {
                    return Json("");
                    
                        

                }
                public ActionResult sp_adminfe_eliminarValoresConfig()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && usuarioSession != null)
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
                            respuesta = this.errorTryControlador(1,x);
                        }catch(Exception x){
                            ErroresIUS error = new ErroresIUS(x.Message,ErroresIUS.tipoError.generico,x.HResult);
                            respuesta = this.errorTryControlador(2,error);
                        }
                        
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_adminfe_agregarValoresConfig()
                {
                    Dictionary<object, object> frm, respuesta;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null)
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
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                    

                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                public ActionResult sp_adminfe_actualizarInfoConfig()
                {
                    Dictionary<object, object> frm, respuesta;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSession = this.getUsuarioSesion();
                    if (frm != null && usuarioSession != null)
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
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
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
