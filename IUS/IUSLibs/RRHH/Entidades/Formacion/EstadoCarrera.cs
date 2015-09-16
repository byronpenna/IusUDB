using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class EstadoCarrera
    {
        #region "propiedades"
            public int      _idEstadoCarrera;
            public string   _estado;
        #endregion
        #region "constructores"
            public EstadoCarrera(int idEstadoCarrera)
            {
                this._idEstadoCarrera = idEstadoCarrera;

            }
            public EstadoCarrera(int idEstadoCarrera,string estado)
            {
                this._idEstadoCarrera   = idEstadoCarrera;
                this._estado            = estado;
            }
        #endregion
    }
}
