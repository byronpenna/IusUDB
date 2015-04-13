using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Control
{
    public class ControlRolSubmenu:PadreLib
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
        public bool eliminarRolSubmenu(int idSubmenu,int idRol,int idUsuarioEjecutor,int idPagina)
        {
            bool toReturn = false;
            SPIUS sp = new SPIUS("sp_sec_eliminarRolSubmenu");
            sp.agregarParametro("idSubMenu", idSubmenu);
            sp.agregarParametro("idRol", idRol);
            sp.agregarParametro("idUsuario", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            try
            {
                DataSet ds = sp.EjecutarProcedimiento();
                if (!this.DataSetDontHaveTable(ds)) {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToBoolean((int)ds.Tables[0].Rows[0]["estadoDelete"]))
                        {
                            toReturn = true;
                        }
                    }
                }

            }
            catch (ErroresIUS x)
            {
                throw x;
            }
            catch (Exception x)
            {
                throw x;
            }
            return toReturn;
        }
    }
}
