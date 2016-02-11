using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas
    using IUSBack.Models.Page.Login.Forms;
// externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.Login.Acciones
{
    public class LoginModel
    {
        #region "propiedades"
            private Usuario _usuario;
            private ControlUsuarios _controlUsuario;
            public Usuario getUsuario
            {
                get
                {
                    return this._usuario;
                }
            }
        #endregion
        #region "constructores"
            public LoginModel()
            {
                this._controlUsuario = new ControlUsuarios();
            }
        #endregion
        public bool login(User usu)
        {
            bool toReturn = false ;
            ControlUsuarios control = new ControlUsuarios();
            if (control.login(usu.usuario,usu.pass))
            {
                this._usuario = control.getUsuario;
                toReturn = true;
            }
            return toReturn;
        }
        public Dictionary<object, object> sp_usu_solicitarCambioPass(string usuario, int idPagina)
        {
            try
            {
                return this._controlUsuario.sp_usu_solicitarCambioPass(usuario, idPagina);
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
        public Dictionary<object, object> logueo(User usu)
        {
            Dictionary<object, object> retorno = new Dictionary<object, object>();
            Usuario usuario = new Usuario(usu.usuario, usu.pass);
            try
            {
                return this._controlUsuario.logueo(usuario);
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
    }
}