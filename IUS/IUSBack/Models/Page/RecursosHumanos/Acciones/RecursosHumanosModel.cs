using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
// Externas
    using IUSLibs.RRHH.Entidades.Formacion;
    // control
        using IUSLibs.RRHH.Control.Formacion;
        using IUSLibs.FrontUI.Control;
        using IUSLibs.RRHH.Entidades;
namespace IUSBack.Models.Page.RecursosHumanos.Acciones
{
    public class RecursosHumanosModel:PadreModel
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
            #region "do"
                
            #endregion
            #region "get"
                public Dictionary<object, object> cargaInicial(int idUsuarioEjecutor,int idPagina)
                {
                    Dictionary<object, object> respuesta = null;
                    try
                    {
                        respuesta = new Dictionary<object, object>();
                        ControlNivelCarrera controlCarrera  = new ControlNivelCarrera();
                        ControlPais         controlPais     = new ControlPais();
                        controles
                        respuesta.Add("nivelesTitulos", controlCarrera.sp_rrhh_getNivelesCarreras(idUsuarioEjecutor, idPagina));
                        respuesta.Add("paises", controlPais.sp_frontui_getPaises());
                        respuesta.Add("estadoCiviles",)
                        return respuesta;
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
                /*public List<NivelTitulo> sp_rrhh_getNivelesCarreras(int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlNivelCarrera control = new ControlNivelCarrera();
                        return control.sp_rrhh_getNivelesCarreras(idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }*/
            #endregion
        #endregion
    }
}