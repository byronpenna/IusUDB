using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUS.Models.general;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Control;
    using IUSLibs.FrontUI.Entidades;
    
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
namespace IUS.Models.page.Instituciones.Acciones
{
    public class InstitucionesModel:ModeloPadre
    {
        #region "propiedades"
            private ControlInstitucion _controlInstitucion;
        #endregion
        #region "funciones"
            #region "get"
                public Dictionary<object,object> sp_frontui_getInstitucionesByContinente(int idContinente,string lang,string ip,int idPagina)
                {
                    try
                    {
                        Dictionary<object, object> respuesta = new Dictionary<object, object>();
                        respuesta = this._controlInstitucion.sp_frontui_getInstitucionesByContinente(idContinente,lang, ip, idPagina);
                        respuesta.Add("paises", this.sp_frontui_getPaisesFromContinente(idContinente, lang,ip, idPagina));
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
                public List<Pais> sp_frontui_getPaisesFromContinente(int idContinente,string lang, string ip, int idPagina)
                {
                    try
                    {
                        ControlPais controlPais = new ControlPais();
                        return controlPais.sp_frontui_getPaisesFromContinente(idContinente,lang,ip, idPagina);
                    }
                    catch (ErroresIUS x) {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                public List<Persona> sp_rrhh_controlPersona_getPersonasByInstitucion(int idInstitucion,string idioma,string ip,int idPagina)
                {
                    try{
                        ControlPersona control = new ControlPersona();
                        return control.sp_rrhh_controlPersona_getPersonasByInstitucion(idInstitucion, idioma, ip, idPagina);
                    }catch(ErroresIUS x){
                        throw x;
                    }
                    catch(Exception x){
                        throw x;
                    }
                }
                public Dictionary<object, object> sp_frontui_front_getInstitucionById(int idInstitucion, string ip, int idPagina,string idioma="es")
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_front_getInstitucionById(idInstitucion, ip, idPagina,idioma);
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
            public InstitucionesModel()
            {
                this._controlInstitucion = new ControlInstitucion();
            }
        #endregion
    }
}