using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Entidades
{
    public class RolSubMenuPermiso
    {
        #region "propiedades"
            public int _idRolSubMenuPermiso;
            public RolSubMenu _rolsubmenu;
            public PermisoRol _permisoRol;
        #endregion
        #region "constructores"
            public RolSubMenuPermiso(int idRolSubMenuPermiso,RolSubMenu rolSubmenu,PermisoRol permisoRol)
            {
                this._idRolSubMenuPermiso = idRolSubMenuPermiso;
                this._rolsubmenu = rolSubmenu;
                this._permisoRol = permisoRol;
            }
        #endregion
    }
}
