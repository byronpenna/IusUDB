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
        #endregion
        #region "constructores"
            public Institucion(int idInstitucion, string nombre, string direccion, int idPais)
            {
                this._idInstitucion = idInstitucion;
                this._nombre = nombre;
                this._direccion = direccion;
                Pais pais = new Pais(idPais);
                this._pais = pais;
            }
            public Institucion(int idInstitucion, string nombre, string direccion, Pais pais)
            {
                this._idInstitucion = idInstitucion;
                this._nombre = nombre;
                this._direccion = direccion;
                this._pais = pais;
            }
            public Institucion(int idInstitucion)
            {
                this._idInstitucion = idInstitucion;
            }
        #endregion
    }
}
