using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Entidades
{
    public class InformacionPersona
    {
        #region "propiedades"
            public int          _idInformacionPersona;
            public Pais         _pais;
            public string       _numeroIdentificacion;
            public EstadoCivil  _estadoCivil;
            public Persona      _persona;
            public byte[]       _foto;
        #endregion
        #region "Constructores"
            public InformacionPersona(int idInformacionPersona)
            {
                this._idInformacionPersona = idInformacionPersona;
            }
            
        #endregion
    }
}
