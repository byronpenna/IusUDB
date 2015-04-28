using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class PermisoUsuarioEvento
    {
        #region "propiedades"
            public int              _idPermisoUsuarioEvento;
            public UsuarioEvento    _usuarioEvento;
            public PermisoEvento    _permiso;
        #endregion
        #region "constructores"
            public PermisoUsuarioEvento(UsuarioEvento usuarioEvento,PermisoEvento permiso)
            {
                this._usuarioEvento = usuarioEvento;
                this._permiso = permiso;
            }
            public PermisoUsuarioEvento(int idPermisoUsuarioEvento,UsuarioEvento usuarioEvento,PermisoEvento permiso)
            {
                this._idPermisoUsuarioEvento = idPermisoUsuarioEvento;
                this._usuarioEvento = usuarioEvento;
                this._permiso = permiso;
            }
        #endregion
    }
}
