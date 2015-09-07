using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades
{
    public class EstadoCivil
    {
        #region "propiedades"
            public int      _idEstadoCivil;
            public string   _estadoCivil;
        #endregion
        #region "constructores"
            public EstadoCivil(int idEstadoCivil)
            {
                this._idEstadoCivil = idEstadoCivil;
            }
            public EstadoCivil(int idEstadoCivil,string estadoCivil)
            {
                this._idEstadoCivil = idEstadoCivil;
                this._estadoCivil = estadoCivil;
            }
        #endregion
    }
}
