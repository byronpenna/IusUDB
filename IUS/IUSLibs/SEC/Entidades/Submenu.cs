using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Submenu
    {
        #region "propiedades"
            public int _idSubMenu;
            public Menu _menu;
            public String _textoSubMenu;
            public String _enlace;
            public string _icono;
        #endregion
        #region "Constructores"
            public Submenu(int idSubMenu, int idMenu, String texto, String enlace)
            {
                this._idSubMenu = idSubMenu;
                Menu menu = new Menu(idMenu);
                this._menu = menu;
                this._textoSubMenu = texto;
                this._enlace = enlace;
            }
            public Submenu(int idSubMenu,Menu menu, String texto,String enlace)
            {
                this._idSubMenu = idSubMenu;
                this._menu = menu;
                this._textoSubMenu = texto;
                this._enlace = enlace;
            }
            public Submenu(int idSubMenu)
            {
                this._idSubMenu = idSubMenu;
            }
        #endregion
        #region "funciones publicas"

        #endregion
    }
}
