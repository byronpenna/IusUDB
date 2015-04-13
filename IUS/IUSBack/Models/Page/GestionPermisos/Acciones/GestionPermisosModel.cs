using System;
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
            #region "acciones"
                public bool agregarPermisoSubmenuRol(int idRol,int idSubmenu,int[] idPermisos,int idUsuarioEjecutor,int idPagina)
                {
                    bool toReturn = false;
                    ControlRolSubMenuPermiso control = new ControlRolSubMenuPermiso();
                    try
                    {
                        toReturn = control.agregarPermisoSubmenuRol(idRol, idSubmenu, idPermisos, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS)
                    {
                        // controlar el error
                    }
                    catch (Exception)
                    {
                        // controlar el error
                    }
                    return toReturn;
                }
                public bool eliminarPermisoSubmenuRol(int idRolSubmenuPermiso,int idUsuarioEjecutor,int idPagina)
            {
                ControlRolSubMenuPermiso control = new ControlRolSubMenuPermiso();
                bool toReturn = control.eliminarRolSubMenuPermiso(idRolSubmenuPermiso,idUsuarioEjecutor,idPagina);
                return toReturn;
            }
            #endregion 
            #region "Traer"
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
        #endregion
        #region "contructores"
            public GestionPermisosModel()
            {
                this._control = new ControlPermiso();
            }
        #endregion

    }
}