using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class Institucion
    {
        #region "propiedades"
            public int      _idInstitucion;
            public string   _nombre;
            public string   _direccion;
            public Pais     _pais;
            public bool     _estado;
            public byte[]   _logo;
            // extras a tabla
                public List<TelefonoInstitucion> _telefonos;
                
        #endregion
        #region "constructores"
            public Institucion(int idInstitucion, byte[] logo)
            {
                this._idInstitucion = idInstitucion;
                this._logo          = logo;
            }
            public Institucion(int idInstitucion, string nombre, string direccion, int idPais,bool estado)
            {
                this._idInstitucion = idInstitucion;
                this._nombre        = nombre;
                this._direccion     = direccion;
                Pais pais           = new Pais(idPais);
                this._pais          = pais;
                
            }
            public Institucion(int idInstitucion, string nombre, string direccion, Pais pais,bool estado)
            {
                this._idInstitucion = idInstitucion;
                this._nombre        = nombre;
                this._direccion     = direccion;
                this._pais          = pais;
            }
            public Institucion(int idInstitucion)
            {
                this._idInstitucion = idInstitucion;
            }
            // para agregar
                public Institucion(string nombre, string direccion,int idPais)
                {
                    this._nombre        = nombre;
                    this._direccion     = direccion;
                    Pais pais           = new Pais(idPais);
                    this._pais          = pais;
                }
        #endregion
    }
}
