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
            public bool agregarRoles(int[] rolesAgregar,int idUsuario,int idUsuarioEjecutor,int idPagina)
            {
                bool toReturn = false;
                ControlRoles control = new ControlRoles();
                toReturn = control.agregarRoles(rolesAgregar, idUsuario, idUsuarioEjecutor,idPagina);
                return toReturn;
            }
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
                    ControlRoles control = new ControlRoles();
                    submenus = control.getSubMenuFaltantesRol(idRol, idUsuarioEjecutor, idPagina);
                    return submenus;
                }
                
            public List<Rol> getRolesFaltantes(int idUsuario,int idUsuarioEjecutor,int idPagina)
                {
                    ControlRoles control = new ControlRoles();
                    List<Rol> roles = null;
                    roles = control.getRolesFaltantes(idUsuario, idUsuarioEjecutor, idPagina);
                    return roles;
                }
                public List<Rol> getRoles(int idUsuario)
                {
                    ControlRoles control = new ControlRoles();
                    List<Rol> roles = control.getRoles(idUsuario);
                    return roles;
                }
                public List<Rol> getAllRoles(int idUsuarioEjecutor,int idPagina)
                {
                    ControlRoles control = new ControlRoles();
                    List<Rol> roles = control.getAllRoles(idUsuarioEjecutor,idPagina);
                    return roles;
                }
            #endregion 
            public Boolean desasociarRol(int idRol, int idUsuario)
            {
                Boolean toReturn = false;
                ControlRoles control = new ControlRoles();
                toReturn = control.desasociarRol(idUsuario, idRol);
                return toReturn;
            }
        #endregion
        #region "contructores"
            public GestionRolesModel()
            {
                this._control = new ControlRoles();
            }
        #endregion

    }
}