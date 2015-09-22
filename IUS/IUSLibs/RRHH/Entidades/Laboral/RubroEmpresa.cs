using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Laboral
{
    public class RubroEmpresa
    {
        #region "Propiedades"
            public int      _idRubro;
            public string   _rubro;
        #endregion
        #region "Constructores"
            public RubroEmpresa(int idRubro)
            {
                this._idRubro = idRubro;
            }
            public RubroEmpresa(int idRubro,string rubro)
            {
                this._idRubro   = idRubro;
                this._rubro     = rubro;
            }
        #endregion
    }
}
