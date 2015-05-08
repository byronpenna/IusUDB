using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Control;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSBack.Models.Page.ConfiguracionWebsite.Acciones
{
    public class ConfiguracionWebsiteModel:PadreModel
    {
        #region "propiedades"
            public ControlConfiguraciones _controlConfig;
        #endregion
        #region "Constructores"
            public ConfiguracionWebsiteModel()
            {
                this._controlConfig = new ControlConfiguraciones();
            }
        #endregion
        #region "funciones publicas"
            #region "gets"
                public Dictionary<object, object> sp_adminfe_getConfiguraciones(int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlConfig.sp_adminfe_getConfiguraciones(idUsuarioEjecutor, idPagina);
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
            #region "acciones"
                public Valor sp_adminfe_agregarValoresConfig(Valor valorAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try {
                        return this._controlConfig.sp_adminfe_agregarValoresConfig(valorAgregar, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x) {
                        throw x;
                    }
                }
                public Configuracion sp_adminfe_actualizarInfoConfig(Configuracion configActualizar,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlConfig.sp_adminfe_actualizarInfoConfig(configActualizar, idUsuarioEjecutor, idPagina);
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
    }
}