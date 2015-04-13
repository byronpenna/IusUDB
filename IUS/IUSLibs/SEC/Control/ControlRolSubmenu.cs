using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.GENERALS;
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Control
{
    public class ControlRolSubmenu
    {
        public bool agregarRolSubMenu(int idRol,int[] idSubmenus,int idUsuarioEjecutor,int idPagina)
        {
            bool toReturn = false;
            SPIUS sp = new SPIUS("sp_sec_agregarSubMenuRol");
            Dictionary<String, Object> parametro;
            foreach (int idSubmenu in idSubmenus)
            {
                parametro = new Dictionary<String, Object>();
                parametro.Add("idSubMenu", idSubmenu);
                parametro.Add("idRol", idRol);
                parametro.Add("idUsuarioEjecutor", idUsuarioEjecutor);
                parametro.Add("idPagina", idPagina);
                sp.agregarParametro(parametro);
            }
            toReturn = sp.ejecutarInsertMultiple();
            return toReturn;
        }
    }
}
