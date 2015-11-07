using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias net
    using System.IO;
    using System.Drawing;
// librerias internas
    using IUSBack.Models.Page.GestionPersonas.acciones;
    
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Entidades;
namespace IUSBack.Controllers.GestionPersonas
{
    public class ExtrasGestionPersonasController : PadreController
    {
        //
        // GET: /ExtrasGestionPersonas/
        #region "propiedades" 
            public ExtraGestionPersonasModel _model;
            public int _idPagina = (int)paginas.gestionPersonas;
        #endregion
        #region "acciones ajax"
            public ActionResult sp_rrhh_setFotoInformacionPersona()
            {
                Dictionary<object, object> frm, respuesta = null;
                string path = "";  string fileName = "";bool agregado = false;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    InformacionPersona info = new InformacionPersona();
                    info._persona = new Persona(this.convertObjAjaxToInt(frm["idPersona"]));
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        if (Request.Files.Count > 0)
                        {
                            List<HttpPostedFileBase> files = this.getBaseFileFromRequest(Request);
                            if (files.Count > 0)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {
                                    fileName = Path.GetFileName(file.FileName);
                                    var strExtension = Path.GetExtension(file.FileName);
                                    string path2 = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["FOTOS_PERSONAL"] + info._persona._idPersona + "/"), info._persona._idPersona.ToString());
                                    path = this.gestionArchivosServer.getPathWithCreate(Server.MapPath(this._RUTASGLOBALES["FOTOS_PERSONAL"] + info._persona._idPersona + "/"), info._persona._idPersona.ToString()+strExtension);
                                    file.SaveAs(path);
                                    string rutaRecortada = path; 
                                    decimal xx = this.convertObjAjaxToDecimal(frm["x"]); decimal yy = this.convertObjAjaxToDecimal(frm["y"]);
                                    decimal xancho = this.convertObjAjaxToDecimal(frm["imgAncho"]); decimal yalto = this.convertObjAjaxToDecimal(frm["imgAlto"]);
                                    
                                    using (Image image = Image.FromFile(path))
                                    {
                                        rutaRecortada = path2 + "_recortada" + strExtension;
                                        if ((image.Width != image.Height || (xancho > 0 && yalto > 0 && xancho == yalto)))
                                        {
                                            int x       = decimal.ToInt32(image.Width * xx);
                                            int y       = decimal.ToInt32(image.Height * yy);
                                            int ancho   = decimal.ToInt32(image.Height * xancho); //decimal.ToInt32(xancho);
                                            int alto    = decimal.ToInt32(image.Height * yalto); //decimal.ToInt32(yalto);
                                            Rectangle cropArea = new Rectangle(x, y, ancho, ancho);
                                            try
                                            {
                                                using (Bitmap bitMap = new Bitmap(cropArea.Width, cropArea.Height))
                                                {
                                                    using (Graphics g = Graphics.FromImage(bitMap))
                                                    {
                                                        g.DrawImage(image, new Rectangle(0, 0, bitMap.Width, bitMap.Height), cropArea, GraphicsUnit.Pixel);
                                                    }
                                                    bitMap.Save(rutaRecortada);
                                                }

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                        else
                                        {
                                            //rutaRecortada
                                            file.SaveAs(rutaRecortada);
                                        }
                                    }
                                    info._fotoRuta = rutaRecortada;
                                    InformacionPersona infoAgregada = this._model.sp_rrhh_setFotoInformacionPersona(info, usuarioSession._idUsuario, this._idPagina);
                                    respuesta = new Dictionary<object, object>();
                                    respuesta.Add("estado", true);
                                    string ruta = Url.Content(this.getRelativePathFromAbsolute(rutaRecortada));
                                    respuesta.Add("imagen",ruta);
                                }
                            }
                        }
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
            #region "correo"
                public ActionResult sp_rrhh_actualizarCorreoPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            EmailPersona emailActualizar = new EmailPersona(this.convertObjAjaxToInt(frm["txtIdEmailPersona"]),frm["txtEmail"].ToString(),frm["txtEtiquetaEmail"].ToString());
                            EmailPersona emailActualizado = this._model.sp_rrhh_actualizarCorreoPersona(emailActualizar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("emailActualizado", emailActualizado);
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
                public ActionResult sp_rrhh_eliminarCorreoPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool eliminado = this._model.sp_rrhh_eliminarCorreoPersona(this.convertObjAjaxToInt(frm["txtIdEmailPersona"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", eliminado);

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
                public ActionResult sp_rrhh_guardarCorreoPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            EmailPersona emailAgregar = new EmailPersona(frm["txtEmail"].ToString(), frm["txtEtiquetaEmail"].ToString(), this.convertObjAjaxToInt(frm["idPersona"]));
                            EmailPersona emailAgregado = this._model.sp_rrhh_guardarCorreoPersona(emailAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("emailPersona", emailAgregado);
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
            #region "telefono"
                public ActionResult sp_rrhh_editarTelefonoPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            TelefonoPersona telefonoEditar = new TelefonoPersona(this.convertObjAjaxToInt(frm["txtHdIdTelefono"]), frm["txtTelefono"].ToString(), frm["txtEtiquetaTel"].ToString(), this.convertObjAjaxToInt(frm["cbPais"]));
                            TelefonoPersona telefonoActualizado = this._model.sp_rrhh_editarTelefonoPersona(telefonoEditar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("telefonoActualizado", telefonoActualizado);
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
                public ActionResult sp_rrhh_eliminarTel()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            bool agrego = this._model.sp_rrhh_eliminarTel(this.convertObjAjaxToInt(frm["txtHdIdTelefono"]), usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", agrego);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
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
                public ActionResult sp_rrhh_guardarTelefonoPersona()
                {
                    Dictionary<object, object> frm, respuesta = null;
                    try
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        frm = this.getAjaxFrm();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            TelefonoPersona telefonoAgregar = new TelefonoPersona(frm["txtTelefono"].ToString(), frm["txtEtiquetaTel"].ToString(), this.convertObjAjaxToInt(frm["cbPais"]), this.convertObjAjaxToInt(frm["idPersona"]));
                            TelefonoPersona telefonoAgregado = this._model.sp_rrhh_guardarTelefonoPersona(telefonoAgregar, usuarioSession._idUsuario, this._idPagina);
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("telefonoAgregado", telefonoAgregado);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
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

            public ActionResult sp_rrhh_guardarInformacionPersona()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        InformacionPersona informacion = new InformacionPersona(this.convertObjAjaxToInt(frm["cbPais"]), frm["txtNumeroIdentificacion"].ToString(), this.convertObjAjaxToInt(frm["cbEstadoCivil"]), this.convertObjAjaxToInt(frm["txtHdIdPersona"]));
                        InformacionPersona informacionAgregada = this._model.sp_rrhh_guardarInformacionPersona(informacion, usuarioSession._idUsuario, this._idPagina);
                        if(informacion != null){
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("informacion", informacionAgregada);
                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS("Error no controlado");
                            throw x;
                        }
                        
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
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
            public ExtrasGestionPersonasController()
            {
                this._model = new ExtraGestionPersonasModel();
            }
        #endregion
    }
}
