using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;    
// librerias internas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.GENERALS;
    using IUSLibs.BaseDatos;
    using IUSLibs.LOGS;
namespace IUSLibs.SEC.Control
{
    public class ControlRolSubMenuPermiso:PadreLib
    {
        #region "funciones publicas"
            public bool eliminarRolSubMenuPermiso(int idRolSubmenuPermiso,int idUsuarioEjecutor,int idPagina)
            {
                bool toReturn = false;
                SPIUS sp = new SPIUS("sp_sec_quitarSubRol");
                sp.agregarParametro("idRolSubmenuPermiso", idRolSubmenuPermiso);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        toReturn = Convert.ToBoolean((int)ds.Tables[0].Rows[0]["estadoDelete"]);
                        if (!toReturn && ds.Tables.Count > 1)
                        {
                            //ErroresIUS x =  
                            // manejar error ius
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
            public List<PermisoRol> getPermisosSubmenuRolFaltantes(int idSubMenu, int idRol, int idUsuarioEjecutor, int idPagina)
            {
                List<PermisoRol> permisosRetorno = null;
                PermisoRol permiso;
                SPIUS sp = new SPIUS("sp_sec_getPermisoSubMenuRolFaltantes");
                sp.agregarParametro("idSubMenu", idSubMenu);
                sp.agregarParametro("idRol", idRol);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            permisosRetorno = new List<PermisoRol>();
                            foreach(DataRow row in ds.Tables[0].Rows){
                                permiso = new PermisoRol((int)row["idPermisoRol"], row["nivelPermiso"].ToString());
                                permisosRetorno.Add(permiso);
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
                return permisosRetorno;
            }
            public List<RolSubMenuPermiso> getPermisosSubmenuRol(int idSubMenu, int idRol, int idUsuarioEjecutor, int idPagina)
            {

                List<RolSubMenuPermiso> toReturn = null;
                // clases genericas para armar el retorno
                    RolSubMenu rolSubMenu;
                    PermisoRol permisoRol;
                    RolSubMenuPermiso rolSubmenuPermiso;
                SPIUS sp = new SPIUS("sp_sec_getPermisoSubMenuRol");
                sp.agregarParametro("idSubMenu", idSubMenu);
                sp.agregarParametro("idRol", idRol);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            toReturn = new List<RolSubMenuPermiso>();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                rolSubMenu = new RolSubMenu((int)row["id_rolsubmenu_fk"]);
                                permisoRol = new PermisoRol((int)row["id_permiso_rol_fk"],row["nivelPermiso"].ToString());
                                rolSubmenuPermiso = new RolSubMenuPermiso((int)row["idRolSubMenuPermiso"], rolSubMenu, permisoRol);
                                toReturn.Add(rolSubmenuPermiso);
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
        #endregion 
    }
}
