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
            
        #endregion
    }
}
