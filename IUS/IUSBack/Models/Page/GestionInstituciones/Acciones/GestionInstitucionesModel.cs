using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.FrontUI.Control;
namespace IUSBack.Models.Page.GestionInstituciones.Acciones
{
    public class GestionInstitucionesModel:PadreModel
    {
        #region "propiedades"
            private ControlPais _controlPais;
        #endregion
        #region "funciones"
            #region "get"
            public Dictionary<object, object> cargaInicialIndex()
                {
                    
                    try
                    {
                        List<Pais> paises = this._controlPais.sp_frontui_getPaises();
                        Dictionary<object, object> retorno = new Dictionary<object,object>();
                        retorno.Add("paises", paises);
                        return retorno;
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
        #region "constructores"
            public GestionInstitucionesModel()
            {
                this._controlPais = new ControlPais();
            }
        #endregion
    }
}