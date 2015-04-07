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
        public List<Rol> getRoles(int idUsuario)
        {
            ControlRoles control = new ControlRoles();
            List<Rol> roles = control.getRoles(idUsuario);
            return roles;
        }
        public Boolean desasociarRol(int idRol, int idUsuario)
        {
            Boolean toReturn = false;
            ControlRoles control = new ControlRoles();
            toReturn = control.desasociarRol(idUsuario, idRol);
            return toReturn;
        }
    }
}