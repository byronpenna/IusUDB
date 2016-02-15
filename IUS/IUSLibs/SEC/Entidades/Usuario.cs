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
            public string _pass;
            // extra a tabla 
            //public bool logueado;
            #region "gets y sets"
                public string getFechaCreacion
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", this._fechaCreacion);
                    }
                }
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
                public string estadoUsuario
            {
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
        #endregion
       
        #region "Constructores"
            public Usuario(string usuario, string pass)
            {
                this._usuario = usuario;
                this._pass = pass;
            }
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
            public Usuario(int idUsuario, String usuario, DateTime fechaCreacion, bool estado, int id_persona_fk)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
                this._fechaCreacion = fechaCreacion;
                this._estado = estado;
                Persona persona = new Persona(id_persona_fk);
                this._persona = persona;
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
            public Usuario(int idUsuario, string usuario)
            {
                this._idUsuario = idUsuario;
                this._usuario = usuario;
            }
            public Usuario(int idUsuario)
            {
                this._idUsuario = idUsuario;
            }
            public Usuario(string usuario, DateTime fechaCreacion, bool estado, Persona persona, string pass)
            {
                this._usuario = usuario;
                this._fechaCreacion = fechaCreacion;
                this._estado = estado;
                this._persona = persona;
                this._pass = pass;
            }
            public Usuario()
            {

            }
        #endregion
    }
}
