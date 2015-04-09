using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.GestionUsuarios.Acciones
{
    public class GestionUsuarioModel:PadreModel
    {
        #region "propiedades"
            public int _idPagina;
            private Permiso _permisosGestion;
            private ControlUsuarios _control;
            #region "get y set" 
                public Permiso permisoGestion
                {
                    get
                    {
                        Permiso permisoTemp = this._permisosGestion;
                        this._permisosGestion = null;
                        return permisoTemp;
                    }
                }
            #endregion 
        #endregion
        #region "constructores"
        public GestionUsuarioModel(int idPagina)
        {
            this._idPagina = idPagina;
            this._control = new ControlUsuarios();
        }
        #endregion
        public List<Usuario> getUsuarios(int idUsuarioEjecutor)
        {
            List<Usuario> usuarios;
            usuarios = this._control.getUsuarios(idUsuarioEjecutor, this._idPagina);
            Permiso permisoGestion = this._control.permisoGestion;
            this._permisosGestion = permisoGestion;
            return usuarios;
        }
        #region "respuestas para json"
            
            public Dictionary<Object, Object> actualizarUsuario(Dictionary<Object,Object> frm,int idUsuarioEjecutor)
            {
                Dictionary<Object, Object> toReturn = new Dictionary<Object,Object>();
                Persona persona = new Persona();
                persona._idPersona = Convert.ToInt32((string)frm["cbPersona"]);
                Usuario UsuarioRetorno;
                Usuario usu = new Usuario(Convert.ToInt32((String)frm["txtHdIdUser"]), frm["txtEditUsuario"].ToString(), persona);
                UsuarioRetorno = this._control.actualizarUsuario(usu,idUsuarioEjecutor,this._idPagina);
                if (UsuarioRetorno != null)
                {
                    toReturn.Add("estado", true);
                    toReturn.Add("usuario", UsuarioRetorno);
                }
                else
                {
                    toReturn.Add("estado", false);
                }
                return toReturn;
            }
            public Dictionary<Object,Object> cambiarEstadoUsuario(int idUsuario,int usuarioEjecutor){
                /*
                 * Resultados: 
                     * Cambiado correctamente 
                     * No posee permisos para cambiar
                     * Error no controlado
                 */
                Dictionary<Object, Object> toReturn = new Dictionary<Object, Object>();
                Usuario usu = this._control.cambiarEstadoUsuario(idUsuario, this._idPagina, usuarioEjecutor);
                if (usu != null)
                {
                    toReturn.Add("estadoEjecucion",true);
                    toReturn.Add("nuevoEstadoUsuario", usu.estadoUsuario);
                    toReturn.Add("_estado", usu._estado);
                }
                else
                {
                    toReturn.Add("estadoEjecucion",false);
                    toReturn.Add("error", "Error en la actualizacion del usuario");
                }
                return toReturn;
            }
        #endregion
    }
}