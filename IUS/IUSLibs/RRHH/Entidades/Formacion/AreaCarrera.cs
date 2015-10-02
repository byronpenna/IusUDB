using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class AreaCarrera
    {
        #region "propiedades"
            public int      _idArea;
            public string   _area;
        #endregion
        #region "Constructores"
            // minimo        
                public AreaCarrera(int idArea)
                {
                    this._idArea = idArea;
                }
            // full 
                public AreaCarrera(int idArea,string area)
                {
                    this._idArea    = idArea;
                    this._area      = area;
                }
        #endregion
    }
}
