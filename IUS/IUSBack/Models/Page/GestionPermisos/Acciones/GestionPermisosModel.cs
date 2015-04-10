using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
namespace IUSBack.Models.Page.GestionPermisos.Acciones
{
    
    public class GestionPermisosModel
    {
        #region "propiedades"
            private ControlPermiso _control;
        #endregion
        #region "funciones privadas"
            
        #endregion
        #region "funciones publicas"
            public Permiso getPermisosSubmenuRol(int idSubMenu, int idRol, int idUsuarioEjecutor, int idPagina)
            {
                Permiso permiso = this._control.getPermisosSubmenuRol(idSubMenu, idRol, idUsuarioEjecutor, idPagina);
                return permiso;
            }
        #endregion
        #region "contructores"
            public GestionPermisosModel()
            {
                this._control = new ControlPermiso();
            }
        #endregion

    }
}