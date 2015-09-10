using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Entidades
{
    public class TelefonoPersona
    {
        #region "propiedades"
            public int      _idTelefonoPersona;
            public string   _telefono;
            public string   _descripcion;
            public Pais     _pais;
            public Persona  _persona;
        #endregion
        #region "constructores"
            public TelefonoPersona(int idTelefonoPersona)
            {
                this._idTelefonoPersona = idTelefonoPersona;
            }
            public TelefonoPersona(int idTelefonoPersona,string telefono,string descripcion,int idPais,int idPersona)
            {
                this._idTelefonoPersona = idTelefonoPersona;
                this._telefono          = telefono;
                this._descripcion       = descripcion;
                Pais pais               = new Pais(idPais);
                this._pais              = pais;
                Persona persona         = new Persona(idPersona);
                this._persona           = persona;
            }
            // Para agregar 
                public TelefonoPersona(string telefono, string descripcion, int idPais, int idPersona)
                {
                    this._telefono = telefono;
                    this._descripcion = descripcion;
                    Pais pais = new Pais(idPais);
                    this._pais = pais;
                    Persona persona = new Persona(idPersona);
                    this._persona = persona;
                }
            // para actualizar
                public TelefonoPersona(int idTelefonoPersona, string telefono, string descripcion, int idPais)
                {
                    this._idTelefonoPersona = idTelefonoPersona;
                    this._telefono = telefono;
                    this._descripcion = descripcion;
                    Pais pais = new Pais(idPais);
                    this._pais = pais;
                    
                }
        #endregion
    }
}
