using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class RedSocial
    {
        #region "propiedades"
            public int      _idRedSocial;
            public string   _nombre;
            public string   _enlace;
            public string   _claseIcono;
        #endregion
        #region "constructores"
            public RedSocial(int idRedSocial, string nombre, string enlace, string claseIcono)
            {
                this._idRedSocial = idRedSocial;
                this._nombre = nombre;
                this._enlace = enlace;
                this._claseIcono = claseIcono;
            }
        #endregion
    }
}
