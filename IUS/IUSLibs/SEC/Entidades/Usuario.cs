using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Usuario
    {
        #region "Propiedades"
            public int _idUsuario;
            public string _usuario;
            public DateTime _fechaCreacion;
            public bool _estado;
            public Persona _persona;
            private string _pass;
        #endregion
        #region
            public string txtBtnHabilitar
            {
                get
                {
                    if (this._estado)
                    {
                        return "Deshabilitar";
                    }
                    else
                    {
                        return "Habilitar";
                    }
                }
            }
            public string estadoUsuario{
                get
                {
                    if (this._estado)
                    {
                        return "Activo";
                    }
                    else
                    {
                        return "Inactivo";
                    }
                }
            }
        #endregion
        #region "Constructores"
            public Usuario(int idUsuario, String usuario, bool estado)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
                this._estado = estado;
            }
            public Usuario(int idUsuario, String usuario, Persona persona)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
                this._persona = persona;

            }
            public Usuario(int idUsuario, String usuario, Persona persona,bool estado)
            {
                this._idUsuario = idUsuario;
                this._usuario   = usuario;
                this._persona   = persona;
                this._estado    = estado;

            }
            public Usuario(int idUsuario, String usuario, DateTime fechaCreacion, bool estado, Persona persona)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
                this._fechaCreacion = fechaCreacion;
                this._estado = estado;
                this._persona = persona;
            }
            public Usuario(int idUsuario,String usuario,DateTime fechaCreacion,bool estado,Persona persona,String pass)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
                this._fechaCreacion = fechaCreacion;
                this._estado = estado;
                this._persona = persona;
                this._pass = pass;
            }
            public Usuario(int idUsuario)
            {
                this._idUsuario = idUsuario;
            }
            public Usuario()
            {

            }
        #endregion
    }
}
