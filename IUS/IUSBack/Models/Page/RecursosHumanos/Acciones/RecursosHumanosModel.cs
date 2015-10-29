using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
// Externas
    // entidades
        using IUSLibs.RRHH.Entidades.Formacion;
        using IUSLibs.RRHH.Entidades;
        using IUSLibs.SEC.Entidades;
    // control
        using IUSLibs.SEC.Control;
        using IUSLibs.FrontUI.Control;
        // recursos humanos
        using IUSLibs.RRHH.Control.Formacion;
        using IUSLibs.RRHH.Control;
        using IUSLibs.RRHH.Control.Laboral;
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
                public List<Persona> sp_rrhh_buscarPersonas(
                        /*string *niveles, *string areas,
                        string carrera, *string rubros,
                        string cargos,  int idUsuarioEjecutor,
                        int idPagina*/
                    Dictionary<object, object> strArrElements, string carrera,
                    int tipoBusqueda,int idUsuarioEjecutor,int idPagina
                )
                {
                    try
                    {
                        ControlInformacionPersona control = new ControlInformacionPersona();
                        return control.sp_rrhh_buscarPersonas(strArrElements, carrera, tipoBusqueda,idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> cargaInicial(int idUsuarioEjecutor,int idPagina)
                {
                    Dictionary<object, object> respuesta = null;
                    try
                    {
                        respuesta = new Dictionary<object, object>();
                        // controles
                            ControlNivelCarrera     controlNivelCarrera     = new ControlNivelCarrera();
                            ControlPais             controlPais             = new ControlPais();
                            ControlEstadoCivil      controlEstadoCivil      = new ControlEstadoCivil();
                            ControlRubroEmpresa     controlRubro            = new ControlRubroEmpresa();
                            ControlEstadoCarrera    controlEstadoCarrera    = new ControlEstadoCarrera();
                            ControlCargos           controlCargo            = new ControlCargos();
                            ControlCarrera          controlCarrera          = new ControlCarrera();
                            ControlAreaCarrera      controlAreaCarrera      = new ControlAreaCarrera();
                            ControlActividadEmpresa controlActividades      = new ControlActividadEmpresa();
                        // respuestas
                            respuesta.Add("nivelesTitulos", controlNivelCarrera.sp_rrhh_getNivelesCarreras(idUsuarioEjecutor, idPagina));
                            respuesta.Add("paises", controlPais.sp_frontui_getPaises());
                            respuesta.Add("estadoCiviles", controlEstadoCivil.sp_rrhh_getEstadosCiviles());
                            respuesta.Add("rubrosEmpresas", controlRubro.sp_rrhh_getRubrosEmpresas());
                            
                            respuesta.Add("estadosCarreras",controlEstadoCarrera.sp_rrhh_getEstadosCarreras(idUsuarioEjecutor,idPagina));
                            respuesta.Add("cargos", controlCargo.sp_rrhh_getCargos(idUsuarioEjecutor, idPagina));
                            respuesta.Add("areasCarreras", controlAreaCarrera.sp_rrhh_getAreasCarreras(idUsuarioEjecutor, idPagina));
                            respuesta.Add("actividades", controlActividades.sp_rrhh_getActividadesEmpresaBuscar());
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
                public Dictionary<object, object> sp_rrhh_detallePesona(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlPersona control = new ControlPersona();
                        return control.sp_rrhh_detallePesona(idPersona, idUsuarioEjecutor, idPagina);
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