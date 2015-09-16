using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Control.Formacion;
    using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class FormacionPersonasModel:PadreModel
    {
        #region "propiedades"
            private ControlFormacionPersona         _controlFormacion;
            private ControlInstitucionesEducativas  _controlInstitucionEducativa;
            private ControlCarrera                  _controlCarrera;
        #endregion
        #region "funciones"
            #region "do"
                public Carrera sp_rrhh_ingresarCarrera(Carrera carreraIngresar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlCarrera.sp_rrhh_ingresarCarrera(carreraIngresar, idUsuarioEjecutor, idPagina);
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
                public InstitucionEducativa sp_rrhh_ingresarInstitucionEducativa(InstitucionEducativa institucionAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlInstitucionEducativa.sp_rrhh_ingresarInstitucionEducativa(institucionAgregar, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> sp_rrhh_getInfoInicialFormacion(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlFormacion.sp_rrhh_getInfoInicialFormacion(idPersona, idUsuarioEjecutor, idPagina);
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
            public FormacionPersonasModel()
            {
                this._controlFormacion              = new ControlFormacionPersona();
                this._controlInstitucionEducativa   = new ControlInstitucionesEducativas();
                this._controlCarrera                = new ControlCarrera();
            }
        #endregion
    }
}