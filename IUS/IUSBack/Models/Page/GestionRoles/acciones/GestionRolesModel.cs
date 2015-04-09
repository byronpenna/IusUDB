using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.GestionRoles.acciones
{
    public class GestionRolesModel:PadreModel
    {

        #region "propiedades"
            public ControlRoles _control;
        #endregion
        #region "funciones publicas"
            
            #region "mandar a traer"
                public List<Submenu> getSubmenuRol(int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    List<Submenu> submenus = null;
                    submenus = this._control.getSubMenuRol(idRol,idUsuarioEjecutor,idPagina);
                    return submenus;
                }
                public List<Submenu> getSubMenuFaltantesRol(int idRol,int idUsuarioEjecutor, int idPagina)
                {
                    List<Submenu> submenus = null;
                    submenus = this._control.getSubMenuFaltantesRol(idRol, idUsuarioEjecutor, idPagina);
                    return submenus;
                }
                
            public List<Rol> getRolesFaltantes(int idUsuario,int idUsuarioEjecutor,int idPagina)
                {
                    List<Rol> roles = null;
                    roles = this._control.getRolesFaltantes(idUsuario, idUsuarioEjecutor, idPagina);
                    return roles;
                }
            public List<Rol> getRoles(int idUsuario)
            {
                List<Rol> roles = this._control.getRoles(idUsuario);
                return roles;
            }
            public List<Rol> getAllRoles(int idUsuarioEjecutor,int idPagina)
            {
                List<Rol> roles = this._control.getAllRoles(idUsuarioEjecutor,idPagina);
                return roles;
            }
            #endregion 
            #region "acciones"
            public Boolean quitarSubmenuArol(int idSubMenu,int idRol,int idUsuarioEjecutor,int idPagina)
            {
                bool toReturn = this._control.quitarSubmenu(idSubMenu, idRol, idUsuarioEjecutor, idPagina);
                return toReturn;
            }
            public Boolean agregarRoles(int[] rolesAgregar, int idUsuario, int idUsuarioEjecutor, int idPagina)
            {
                bool toReturn = false;
                toReturn = this._control.agregarRoles(rolesAgregar, idUsuario, idUsuarioEjecutor, idPagina);
                return toReturn;
            }
            public Boolean desasociarRol(int idRol, int idUsuario)
            {
                Boolean toReturn = false;
                toReturn = this._control.desasociarRol(idUsuario, idRol);
                return toReturn;
            }
            #endregion
        #endregion
        #region "contructores"
            public GestionRolesModel()
            {
                this._control = new ControlRoles();
            }
        #endregion

    }
}