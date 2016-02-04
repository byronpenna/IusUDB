using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Entidades
{
    public class EmailPersona
    {
        #region "propiedades"
            public int      _idEmail;
            public string   _email;
            public string   _descripcion;
            public Persona  _persona;
            public bool     _principal;
        #endregion
        #region "Constructores"
            public EmailPersona(int idEmail)
            {
                this._idEmail = idEmail;
            }
            public EmailPersona(int idEmail,string email,string descripcion,int idPersona)
            {
                this._idEmail       = idEmail;
                this._email         = email;
                this._descripcion   = descripcion;
                Persona persona = new Persona(idPersona);
                this._persona = persona;
            }
            // para actualizar
                public EmailPersona(int idEmail, string email, string descripcion)
                {
                    this._idEmail = idEmail;
                    this._email = email;
                    this._descripcion = descripcion;
                    
                }
            // para agregar
                public EmailPersona(string email, string descripcion, int idPersona)
                {
                    this._email = email;
                    this._descripcion = descripcion;
                    Persona persona = new Persona(idPersona);
                    this._persona = persona;
                }
        #endregion
    }
}
