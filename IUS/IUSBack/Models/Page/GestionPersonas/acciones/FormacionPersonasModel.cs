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
                #region "formacion personas"
                    public FormacionPersona sp_rrhh_ingresarFormacionPersona(FormacionPersona formacionAgregar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlFormacion.sp_rrhh_ingresarFormacionPersona(formacionAgregar, idUsuarioEjecutor, idPagina);
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
                    public bool sp_rrhh_eliminarTituloPersona(int idFormacionPersona, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlFormacion.sp_rrhh_eliminarTituloPersona(idFormacionPersona, idUsuarioEjecutor, idPagina);
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
                #region "carrera"
                    public bool sp_rrhh_eliminarCarrera(int idCarrera, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlCarrera.sp_rrhh_eliminarCarrera(idCarrera, idUsuarioEjecutor, idPagina);
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
                #endregion
                #region "institucion educativa"
                    public InstitucionEducativa sp_rrhh_editarInstitucionEducativa(InstitucionEducativa institucionEditar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlInstitucionEducativa.sp_rrhh_editarInstitucionEducativa(institucionEditar, idUsuarioEjecutor, idPagina);
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
                    public bool sp_rrhh_eliminarInstitucionEducativa(int idInstitucionFormacion, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlInstitucionEducativa.sp_rrhh_eliminarInstitucionEducativa(idInstitucionFormacion, idUsuarioEjecutor, idPagina);
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