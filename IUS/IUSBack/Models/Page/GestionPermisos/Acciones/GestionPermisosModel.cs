﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
    using IUSLibs.LOGS;
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
            public bool eliminarPermisoSubmenuRol(int idRolSubmenuPermiso,int idUsuarioEjecutor,int idPagina)
            {
                ControlRolSubMenuPermiso control = new ControlRolSubMenuPermiso();
                bool toReturn = control.eliminarRolSubMenuPermiso(idRolSubmenuPermiso,idUsuarioEjecutor,idPagina);
                return toReturn;
            }
            public List<RolSubMenuPermiso> getPermisosSubmenuRol(int idSubMenu, int idRol, int idUsuarioEjecutor, int idPagina)
            {
                ControlRolSubMenuPermiso control = new ControlRolSubMenuPermiso();
                List<RolSubMenuPermiso> permisos = control.getPermisosSubmenuRol(idSubMenu, idRol, idUsuarioEjecutor, idPagina);
                return permisos;
            }
            public List<PermisoRol> getPermisosSubmenuRolFaltantes(int idSubMenu, int idRol, int idUsuarioEjecutor, int idPagina)
            {
                ControlRolSubMenuPermiso control = new ControlRolSubMenuPermiso();
                List<PermisoRol> permisos = null;
                try
                {
                    permisos = control.getPermisosSubmenuRolFaltantes(idSubMenu, idRol, idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return permisos;
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