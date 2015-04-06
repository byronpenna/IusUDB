using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Librerias propias
using IUSLibs.SEC.Control;
using IUSBack.Models.Page.Login.Forms;
using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.Login.Acciones
{
    public class LoginModel
    {
        #region "propiedades"
            private Usuario _usuario;
            public Usuario getUsuario
            {
                get
                {
                    return this._usuario;
                }
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
    }
}