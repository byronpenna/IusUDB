using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class EnlaceInstitucion
    {
        #region "propiedades"
            public int          _idEnlace;
            public string       _enlace;
            public string       _nombreEnlace;
            public Institucion  _institucion;
        #endregion
        #region "constructores"
            public EnlaceInstitucion(int idEnlace,string enlace,string nombreEnlace,int idInstitucion)
            {
                this._idEnlace          = idEnlace;
                this._enlace            = enlace;
                this._nombreEnlace      = nombreEnlace;
                Institucion institucion = new Institucion(idInstitucion);
                this._institucion       = institucion;
            }
            // para agregar
            public EnlaceInstitucion(string enlace,string nombreEnlace,int idInstitucion)
            {
                this._enlace            = enlace;
                this._nombreEnlace      = nombreEnlace;
                Institucion institucion = new Institucion(idInstitucion);
                this._institucion       = institucion;
            }
        #endregion
    }
}
