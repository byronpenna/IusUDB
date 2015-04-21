using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
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
                // esta funcion unicamente asigna el rol al usuario 
                public Boolean agregarRoles(int[] rolesAgregar, int idUsuario, int idUsuarioEjecutor, int idPagina)
                {
                    bool toReturn = false;
                    toReturn = this._control.agregarRoles(rolesAgregar, idUsuario, idUsuarioEjecutor, idPagina);
                    return toReturn;
                }
                // esta funcion si agrega a la tabla "roles"
                public Rol sp_sec_addRol(Rol rolAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    Rol rol = null;
                    try
                    {
                        rol = this._control.sp_sec_addRol(rolAgregar, idUsuarioEjecutor , idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return rol;
                }
                public Boolean desasociarRol(int idRol, int idUsuario)
                {
                    Boolean toReturn = false;
                    toReturn = this._control.desasociarRol(idUsuario, idRol);
                    return toReturn;
                }
                public Boolean sp_sec_eliminarRol(int idRol,int idUsuarioEjecutor, int idPagina)
                {
                    bool respuesta = false;
                    try
                    {
                        respuesta = this._control.sp_sec_eliminarRol(idRol, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return respuesta;
                }
                public Rol sp_sec_cambiarEstadoRol(int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    Rol rol = null;
                    try
                    {
                        rol = this._control.sp_sec_cambiarEstadoRol(idRol, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return rol;
                }
                public Rol sp_sec_editarRol(Rol rol,int idUsuarioEjecutor,int idPagina)
                {
                    Rol rolRegresar = null;
                    try
                    {
                        rolRegresar = this._control.sp_sec_editarRol(rol, idUsuarioEjecutor, idPagina);
                    }catch(ErroresIUS x){
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return rolRegresar;
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