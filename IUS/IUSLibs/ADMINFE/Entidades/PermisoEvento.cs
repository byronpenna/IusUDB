using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class PermisoEvento
    {
        #region "propiedades"
            public int _idPermiso;
            public string _permiso;
        #endregion
        #region "constructores"
            public PermisoEvento(int idPermiso,string _permiso)
            {
                this._idPermiso = idPermiso;
                this._permiso = _permiso;
            }
            public PermisoEvento(int idPermiso)
            {
                this._idPermiso = idPermiso;
            }
        #endregion
    }
}
