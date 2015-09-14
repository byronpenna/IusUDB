using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class Carrera
    {
        #region "propiedades"
            public int                  _idCarrera;
            public string               _carrera;
            public NivelTitulo          _nivelCarrera;
            public InstitucionEducativa _institucion;
            
        #endregion
        #region "constructores"
            public Carrera(int idCarrera)
            {
                this._idCarrera = idCarrera;
            }
        #endregion
    }
}
