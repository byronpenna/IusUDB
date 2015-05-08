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
                        try
                        {
                            Dictionary<object, object> dic = this._model.sp_adminfe_getConfiguraciones(usuarioSession._idUsuario, this._idPagina);
                            
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
                        ViewBag.config = config;
                        ViewBag.valores = valores;
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
        #region "ajax"
            #region "acciones"
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
