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
                public Dictionary<object,object> sp_sec_getSubmenu(int idMenu, int idUsuario)
                {
                    List<Submenu> submenus= null; Submenu submenu;
                    Dictionary<object, object> retorno = new Dictionary<object,object>();
                    Menu menuPadre = null;
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
                                    if (row["icono"] != DBNull.Value)
                                    {
                                        submenu._icono = row["icono"].ToString();
                                    }
                                    submenus.Add(submenu);
                                }
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                menuPadre = new Menu((int)row["idMenu"], row["menu"].ToString(), "");
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
                    retorno.Add("submenus", submenus);
                    retorno.Add("menuPadre", menuPadre);
                    return retorno;
                }
            #endregion
        #endregion
        #region "contructores"
        #endregion
    }
}
