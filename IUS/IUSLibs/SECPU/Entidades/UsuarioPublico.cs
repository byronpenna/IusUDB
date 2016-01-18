using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SECPU.Entidades
{
    public class UsuarioPublico
    {
        #region "propiedades"
            public int                  _idUsuarioPublico;
            public string               _nombres;
            public string               _apellidos;
            public string               _email;
            public DateTime             _fechaNac;
            public string               _pass;
            public EstadoUsuarioPublico _estadoUsuario;
        #endregion
        #region "constructores"
            public UsuarioPublico(int idUsuarioPublico, string nombres, string apellidos, string email, DateTime fechaNac, int idEstadoUsuario)
            {
                this._idUsuarioPublico = idUsuarioPublico;
                this._nombres = nombres;
                this._apellidos = apellidos;
                this._email = email;
                this._fechaNac = fechaNac;
                EstadoUsuarioPublico estadoUsuario = new EstadoUsuarioPublico(idUsuarioPublico);
                this._estadoUsuario = estadoUsuario;
            }
            public UsuarioPublico(int idUsuarioPublico,string nombres,string apellidos,string email,DateTime fechaNac,string pass,int idEstadoUsuario)
            {
                this._idUsuarioPublico = idUsuarioPublico;
                this._nombres = nombres;
                this._apellidos = apellidos;
                this._email = email;
                this._fechaNac = fechaNac;
                this._pass = pass;
                EstadoUsuarioPublico estadoUsuario = new EstadoUsuarioPublico(idUsuarioPublico);
                this._estadoUsuario = estadoUsuario;
            }
            public UsuarioPublico(string nombres, string apellidos, string email, DateTime fechaNac, string pass)
            {
                this._nombres = nombres;
                this._apellidos = apellidos;
                this._email = email;
                this._fechaNac = fechaNac;
                this._pass = pass;
            }
            public UsuarioPublico()
            {

            }
        #endregion
    }
}
