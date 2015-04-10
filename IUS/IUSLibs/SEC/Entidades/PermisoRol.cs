using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    class PermisoRol
    {
        #region "propiedades"
            public int _idPermisoRol;
            public string _permiso;
        #endregion
        #region "constructores"
            public PermisoRol(int idPermiso,string permiso)
            {
                this._idPermisoRol = idPermiso;
                this._permiso = permiso;
            }
        #endregion
    }
}
