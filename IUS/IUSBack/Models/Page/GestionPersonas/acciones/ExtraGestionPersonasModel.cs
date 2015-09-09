using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Entidades;
    using IUSLibs.RRHH.Control;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class ExtraGestionPersonasModel : PadreModel
    {
        #region "propiedades"
            private ControlInformacionPersona   _controlInformacion;
            private ControlTelefonoPersona      _controlTelefono;
        #endregion
        #region "funciones"
            #region "do"
                public TelefonoPersona sp_rrhh_guardarTelefonoPersona(TelefonoPersona telefonoAgregar,int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlTelefono.sp_rrhh_guardarTelefonoPersona(telefonoAgregar, idUsuarioEjecutor, idPagina);
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
                public bool sp_rrhh_eliminarTel(int idTelefonoPersona,int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        return this._controlTelefono.sp_rrhh_eliminarTel(idTelefonoPersona, idUsuarioEjecutor, idPagina);
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
                #region "informacion persona"
                    public InformacionPersona sp_rrhh_guardarInformacionPersona(InformacionPersona infoAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlInformacion.sp_rrhh_guardarInformacionPersona(infoAgregar, idUsuarioEjecutor, idPagina);
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
                #endregion
            #endregion
            #region "get"
            #endregion
        #endregion
        #region "constructores"
                public ExtraGestionPersonasModel()
            {
                this._controlInformacion    = new ControlInformacionPersona();
                this._controlTelefono       = new ControlTelefonoPersona();
            }
        #endregion
    }
}