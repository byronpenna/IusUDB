using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class GestionPersonaModel:PadreModel
    {
        
        #region "propiedades"
        private ControlPersona _control;
        #endregion 
        #region "gets"
            public List<Persona> getPersonas()
            {
                List<Persona> personas = this._control.getPersonas();
                if (personas.Count != 0)
                {
                    return personas;
                }
                else
                {
                    return null;
                }
            }
        #endregion
        #region "Acciones"
            public bool sp_hm_eliminarPersona(int idPersona,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._control.sp_hm_eliminarPersona(idPersona,idUsuarioEjecutor,idPagina);
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
            public Dictionary<object,object> sp_hm_agregarPersona(Persona persona, int idUsuarioEjecutor,int idPagina)
            {
                Dictionary<object, object> retorno = new Dictionary<object, object>();
                try
                {
                    Persona personaAgregada;
                    Permiso permisos = this.sp_trl_getAllPermisoPagina(idUsuarioEjecutor, idPagina);
                    personaAgregada = this._control.sp_hm_agregarPersona(persona, idUsuarioEjecutor, idPagina);
                    retorno.Add("persona", personaAgregada);
                    retorno.Add("permisos", permisos);
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
            #region "actualizarPersona"
                public Persona actualizarPersona(Persona persona,int idUsuario,int idPagina)
                {
                    /*Dictionary<Object, Object> toReturn = new Dictionary<Object, Object>();
                    try
                    {
                        Persona personaActual = this._control.actualizarPersona(persona,idUsuario,idPagina);
                        if (personaActual != null)
                        {
                            toReturn.Add("estado", true);
                            toReturn.Add("persona", personaActual);
                        }
                        else
                        {
                            toReturn.Add("estado", false);
                            toReturn.Add("mensaje", "Error no controlado");
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        toReturn.Add("estado", false);
                        toReturn.Add("errorCode", x.errorNumber);
                        toReturn.Add("errorMessage", x.Message);
                    }
                    catch(Exception x){
                        toReturn.Add("estado", false);
                        toReturn.Add("errorCode", -1);
                        toReturn.Add("errorMessage", x.Message);
                    }
                    return toReturn;*/
                    try
                    {
                        return this._control.actualizarPersona(persona, idUsuario, idPagina);
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
                public Dictionary<Object, Object> actualizarPersona(List<Persona> personas, int idUsuarioEjecutor, int idPagina)
                {
                    Dictionary<object, object> respuesta = new Dictionary<object,object>();
                    List<Persona> personasActualizadas = new List<Persona>();
                    bool estadoIndividual = true; bool estadoUniversal;
                    Persona perso;
                    foreach (Persona persona in personas)
                    {
                        try
                        {
                            perso = this._control.actualizarPersona(persona, idUsuarioEjecutor, idPagina);
                            if (perso == null)
                            {
                                estadoIndividual = false;
                            }
                            personasActualizadas.Add(perso);
                        }
                        catch (ErroresIUS)
                        {
                            estadoIndividual = false;
                        }
                        catch (Exception)
                        {
                            estadoIndividual = false;
                        }
                    }
                    if (personasActualizadas.Count == 0)
                    {
                        estadoUniversal = false;
                    }
                    else
                    {
                        estadoUniversal = true;
                    }
                    respuesta.Add("estado", estadoUniversal);
                    respuesta.Add("estadoIndividual", estadoIndividual);
                    respuesta.Add("personas", personasActualizadas);
                    return respuesta;
                }
            #endregion
        #endregion
        #region "contructores"
            public GestionPersonaModel()
            {
                this._control = new ControlPersona();
            }
        #endregion
    }
}