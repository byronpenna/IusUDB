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
        #endregion
        #region "Constructores"
            public Submenu(int idSubMenu,Menu menu, String texto,String enlace)
            {
                this._idSubMenu = idSubMenu;
                this._menu = menu;
                this._textoSubMenu = texto;
                this._enlace = enlace;
            }
            public Submenu()
            {

            }
        #endregion
    }
}
