using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Entidades.Laboral;
    using IUSLibs.RRHH.Control.Laboral;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class GestionLaboralModel:PadreModel
    {
        #region "propiedades"
            private ControlLaboralPersona   _controlLaboral;
            private ControlActividadEmpresa _controlActividad;
        #endregion
        #region "funciones"
            #region "do"
                #region "laboral"
                    public bool sp_rrhh_eliminarLaboralPersonas(int idLaboralPersona, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlLaboral.sp_rrhh_eliminarLaboralPersonas(idLaboralPersona, idUsuarioEjecutor, idPagina);
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
                    public LaboralPersona sp_rrhh_insertLaboralPersonas(LaboralPersona laboralAgregar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlLaboral.sp_rrhh_insertLaboralPersonas(laboralAgregar, idUsuarioEjecutor, idPagina);
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
                    public LaboralPersona sp_rrhh_editarLaboralPersonas(LaboralPersona laboralEditar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlLaboral.sp_rrhh_editarLaboralPersonas(laboralEditar, idUsuarioEjecutor, idPagina);
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
                #region "actividades"
                    public ActividadEmpresa sp_rrhh_editarActividadEmpresa(ActividadEmpresa actividadEditar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlActividad.sp_rrhh_editarActividadEmpresa(actividadEditar, idUsuarioEjecutor, idPagina);
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
                    public ActividadEmpresa sp_rrhh_insertActividadEmpresa(ActividadEmpresa actividadAgregar,int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlActividad.sp_rrhh_insertActividadEmpresa(actividadAgregar, idUsuarioEjecutor, idPagina);
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
                    public bool sp_rrhh_eliminarActividadadesEmpresa(int idActividadEmpresa, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlActividad.sp_rrhh_eliminarActividadadesEmpresa(idActividadEmpresa, idUsuarioEjecutor, idPagina);
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
                #region "noramles"
                    public List<ActividadEmpresa> sp_rrhh_getActividadesEmpresa(int idLaboralPersona, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            ControlActividadEmpresa control = new ControlActividadEmpresa();
                            return control.sp_rrhh_getActividadesEmpresa(idLaboralPersona,idUsuarioEjecutor, idPagina);
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
                #region "diccionarios"
                    public Dictionary<object, object> sp_rrhh_getInfoInicialLaboralPersona(int idPersona, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlLaboral.sp_rrhh_getInfoInicialLaboralPersona(idPersona, idUsuarioEjecutor, idPagina);
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
                    public Dictionary<object, object> sp_rrhh_getEditModeLaboralPersona(int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            Dictionary<object, object> retorno = new Dictionary<object, object>();
                            ControlEmpresa controlEmpresa = new ControlEmpresa();
                            ControlCargos controlCargos = new ControlCargos();
                            retorno.Add("empresas", controlEmpresa.sp_rrhh_getEmpresas(idUsuarioEjecutor, idPagina));
                            retorno.Add("cargos", controlCargos.sp_rrhh_getCargos(idUsuarioEjecutor, idPagina));
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
        #endregion
        #region "constructores"
            public GestionLaboralModel()
            {
                this._controlLaboral    = new ControlLaboralPersona();
                this._controlActividad  = new ControlActividadEmpresa();
            }
        #endregion
    }
}