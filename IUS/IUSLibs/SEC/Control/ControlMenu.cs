using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Control
{
    public class ControlMenu:PadreLib
    {
        #region "funciones"
            //sp_sec_getMenu
            public List<Menu> sp_sec_getMenu(int idUsuario)
            {
                List<Menu> menus = null; Menu menu;
                SPIUS sp = new SPIUS("sp_sec_getMenu");
                sp.agregarParametro("idUsuario", idUsuario);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            menus = new List<Menu>();
                            foreach(DataRow row in tb[0].Rows){
                                menu = new Menu((int)row["idMenu"], row["menu"].ToString(), row["enlace"].ToString()+row["idMenu"].ToString());
                                menus.Add(menu);
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
                return menus;
            }
        #endregion
    }
}
