using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Entidades
{
    public class RolSubMenu
    {
        #region "propiedades"
            public int _idRolSubMenu;
            public Submenu _submenu;
            public Rol _rol;
        #endregion
        #region "Contructores"

            public RolSubMenu(int idRolSubMenu,Submenu submenu,Rol rol)
            {
                this._idRolSubMenu = idRolSubMenu;
                this._submenu = submenu;
                this._rol = rol;
            }

            public RolSubMenu(int idRolSubmenu)
            {
                this._idRolSubMenu = idRolSubmenu;
            }

        #endregion
    }
}
