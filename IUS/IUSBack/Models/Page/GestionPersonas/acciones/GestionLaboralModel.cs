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
            private ControlLaboralPersona _controlLaboral;
            
        #endregion
        #region "funciones"
            #region "do"
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
            #endregion
            #region "get"
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
                /*public Dictionary<object, object> sp_rrhh_getEditModeLaboralPersona(int idPersona,int idUsuario)
                {
                    try
                    {
                        Dictionary<object,object> retorno = new Dictionary<object,object>();
                        //retorno.Add("empresas",)
                        //retorno.Add("cargos",)
                        ControlEmpresa controlEmpresa = new ControlEmpresa();

                        //this._controlLaboral.
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
        #region "constructores"
            public GestionLaboralModel()
            {
                this._controlLaboral = new ControlLaboralPersona();
            }
        #endregion
    }
}