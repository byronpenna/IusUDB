using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// librerias internas 
    using IUSBack.Models.Page.GestionInstituciones.Acciones;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
namespace IUSBack.Controllers
{
    public class GestionTelefonosController : PadreController
    {
        #region "propiedades"
            public int                  _idPagina = (int)paginas.Instituciones;
            public GestionTelefonoModel _model;
        #endregion
        #region "acciones ajax"
            public ActionResult sp_frontui_insertTelInstitucion()
            {
                Dictionary<object, object> frm, respuesta = null;
                try
                {
                    Usuario usuarioSession = this.getUsuarioSesion();

                    frm = this.getAjaxFrm();
                    if (usuarioSession != null && frm != null)
                    {
                        TelefonoInstitucion telefonoIngresar = new TelefonoInstitucion(frm["txtTelefono"].ToString(), frm["txtEtiqueta"].ToString(), this.convertObjAjaxToInt(frm["txtHdIdInstitucion"]));
                        TelefonoInstitucion telefonoAgregado = this._model.sp_frontui_insertTelInstitucion(telefonoIngresar, usuarioSession._idUsuario, this._idPagina);
                        respuesta = new Dictionary<object, object>();
                        respuesta.Add("estado", true);
                        respuesta.Add("telefono", telefonoAgregado);

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
                    ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                    respuesta = this.errorTryControlador(2, error);
                }
                return Json(respuesta);
            }
        #endregion
        #region "constructores"
            public GestionTelefonosController()
            {
                this._model = new GestionTelefonoModel();
            }
        #endregion

    }
}
