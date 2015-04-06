using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Menu
    {
        #region "propiedades"
            public int _idMenu;
            public String _menu;
            public String _enlace;
        #endregion 
        #region "constructores"
            public Menu(int idMenu,String menu,String enlace)
            {
                this._idMenu = idMenu;
                this._menu = menu;
                this._enlace = enlace;
            }
            public Menu()
            {

            }
        #endregion
    }
}
