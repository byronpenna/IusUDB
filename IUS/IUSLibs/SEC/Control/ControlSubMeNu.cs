using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
namespace IUSLibs.SEC.Control
{
    public class ControlSubMenu:PadreLib
    {
        #region "propiedades"
        #endregion
        #region "funciones privadas"
        #endregion
        #region "funciones publicas"
            #region "acciones"
                public bool agregar()
                {
                    bool toReturn = false;

                    return toReturn;
                }
                public List<Submenu> sp_sec_getSubmenu(int idMenu, int idUsuario)
                {
                    List<Submenu> submenus= null; Submenu submenu;
                    SPIUS sp = new SPIUS("sp_sec_getSubmenu");
                    sp.agregarParametro("idMenu", idMenu);
                    sp.agregarParametro("idUsuario", idUsuario);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                submenus = new List<Submenu>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    submenu = new Submenu((int)row["idSubMenu"], (int)row["id_menu_fk"], row["submenu"].ToString(), row["enlace"].ToString());
                                    submenus.Add(submenu);
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
                    return submenus;
                }
            #endregion
        #endregion
        #region "contructores"
        #endregion
    }
}
