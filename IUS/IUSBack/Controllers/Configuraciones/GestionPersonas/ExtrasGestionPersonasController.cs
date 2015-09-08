using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas

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
        #region "acciones ajax"
            public ActionResult sp_rrhh_guardarInformacionPersona()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        InformacionPersona informacion = new InformacionPersona(this.convertObjAjaxToInt(frm["cbPais"]), frm["txtNumeroIdentificacion"].ToString(), this.convertObjAjaxToInt(frm["cbEstadoCivil"]), this.convertObjAjaxToInt(frm["txtHdIdPersona"]));
                        if(informacion != null){
                            respuesta = new Dictionary<object, object>();
                            respuesta.Add("estado", true);
                            respuesta.Add("informacion", informacion);
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

    }
}
