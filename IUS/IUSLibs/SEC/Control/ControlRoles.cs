using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas 
    using IUSLibs.LOGS;
    using IUSLibs.GENERALS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.BaseDatos;

namespace IUSLibs.SEC.Control
{
    public class ControlRoles:PadreLib
    {
        #region "Funciones"
            #region "acciones"
                public bool quitarSubmenu(int idSubMenu,int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    bool toReturn = false;
                    SPIUS sp = new SPIUS("sp_sec_eliminarSubmenuRol");
                    sp.agregarParametro("idSubmenu", idSubMenu);
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
                                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["estadoDelete"].ToString()))
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
                    return toReturn;
                }
                public bool desasociarRol(int idUsuario,int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    bool toReturn = false;
                    SPIUS sp = new SPIUS("sp_sec_desasociarRoles");

                    sp.agregarParametro("idUsuario", idUsuario);
                    sp.agregarParametro("idRol", idRol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);

                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (Convert.ToBoolean((int)ds.Tables[0].Rows[0]["estadoProc"]))
                        {
                            toReturn = true;
                        }
                    }
                    return toReturn;
                }
                // esta funcion solo asigna rol a usuario
                public bool agregarRoles(int[] roles,int idUsuario,int idUsuarioEjecutor,int idPagina){
                    bool toReturn = false;
                    SPIUS sp = new SPIUS("sp_usu_asociarRol");
                    Dictionary<String, Object> parametro;
                    foreach (int idRol in roles)
                    {
                        parametro = new Dictionary<String, Object>();
                        parametro.Add("idRol", idRol);
                        parametro.Add("idUsuario", idUsuario);
                        parametro.Add("idUsuarioEjecutor", idUsuarioEjecutor);
                        parametro.Add("idPagina", idPagina);
                        sp.agregarParametro(parametro);
                    }
                    toReturn = sp.ejecutarInsertMultiple();
                    return toReturn;
                }
                // esta funcion si agrega a tabla roles
                public Rol sp_sec_addRol(Rol rolAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    Rol rol = null;
                    SPIUS sp = new SPIUS("sp_sec_addRol");
                    sp.agregarParametro("rol", rolAgregar._rol);
                    sp.agregarParametro("estado", rolAgregar._estado);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow rowRol = tb[1].Rows[0];
                                rol = new Rol((int)rowRol["idRol"], rowRol["rol"].ToString(), (bool)rowRol["estado"]);
                            }
                        }
                        else
                        {
                            DataRow rowError = tb[0].Rows[0];
                            ErroresIUS x = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"], rowError["errorSql"].ToString(),(bool)rowError["mostrar"]);
                            throw x;
                        }
                        /*DataSet ds = sp.EjecutarProcedimiento();
                        DataTableCollection tables = this.getTables(ds);
                        if (tables != null && (int)tables[0].Rows[0]["estadoInsert"] == 1)
                        {
                            DataRow rowRol = tables[1].Rows[0];
                            rol = new Rol((int)rowRol["idRol"],rowRol["rol"].ToString(),(bool)rowRol["estado"]);
                        }*/
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
                // esta funcion si quita de la tabla roles
                public bool sp_sec_eliminarRol(int idRol,int idUsuario,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_sec_eliminarRol");
                    sp.agregarParametro("idRol", idRol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataSet ds = sp.EjecutarProcedimiento();
                        DataTableCollection tables = this.getTables(ds);
                        if (tables != null && (int)tables[0].Rows[0]["estadoDelete"] == 1)
                        {
                            estado = true;
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
                    return estado;
                }
                public Rol sp_sec_cambiarEstadoRol(int idRol, int idUsuarioEjecutor, int idPagina)
                {
                    Rol rolRegreso = null;
                    SPIUS sp = new SPIUS("sp_sec_cambiarEstadoRol");
                    sp.agregarParametro("idRol", idRol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        DataRow rowResultado = tb[1].Rows[0];
                        if (tb != null && (int)tb[0].Rows[0]["estadoProc"] == 1)
                        {
                            rolRegreso = new Rol((int)rowResultado["idRol"], rowResultado["rol"].ToString(), (bool)rowResultado["estado"]);
                        }
                        else
                        {
                            ErroresIUS x = new ErroresIUS(rowResultado["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowResultado["errorCode"]);
                            throw x;
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
                    return rolRegreso;
                }
                public Rol sp_sec_editarRol(Rol rol, int idUsuarioEjecutor, int idPagina)
                {
                    Rol rolRegresar =null;
                    SPIUS sp = new SPIUS("sp_sec_editarRol");
                    sp.agregarParametro("idRol",rol._idRol);
                    sp.agregarParametro("rol", rol._rol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            rolRegresar = new Rol((int)rowResultado["idRol"], rowResultado["rol"].ToString(), (bool)rowResultado["estado"]);
                        }else{
                            DataRow rowResult = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(rowResult);
                            throw x;
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
                    return rolRegresar;
                }
            #endregion
            #region "traer"
                public List<Submenu> getSubMenuRol(int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    /*
                     * la diferencia de este con el de control de usuario es que este trae los 
                     * submenus independientemente del permiso que posea
                     */
                    List<Submenu> submenus = null;
                    Submenu submenu; Menu menu;
                    SPIUS sp = new SPIUS("sp_sec_getSubMenuPorRol");
                    sp.agregarParametro("idRol", idRol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina",idPagina);
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            submenus = new List<Submenu>();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                menu = new Menu((int)row["id_menu_fk"], row["menu"].ToString(), row["menuEnlace"].ToString());
                                submenu = new Submenu((int)row["id_submenu_fk"], menu, row["submenu"].ToString(), row["subMenuEnlace"].ToString());
                                submenus.Add(submenu);
                            }
                        }
                    }
                    return submenus;
                }
                public List<Submenu> getSubMenuFaltantesRol(int idRol,int idUsuarioEjecutor,int idPagina)
                {
                    List<Submenu> submenus = null;
                    Submenu submenu; Menu menu;
                    SPIUS sp = new SPIUS("sp_sec_getSubMenuFaltante");
                    sp.agregarParametro("idRol", idRol);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            submenus = new List<Submenu>();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                menu = new Menu((int)row["id_menu_fk"],row["menu"].ToString(),row["menuEnlace"].ToString());
                                submenu = new Submenu((int)row["id_submenu_fk"], menu, row["submenu"].ToString(), row["subMenuEnlace"].ToString());
                                submenus.Add(submenu);
                            }
                        }
                    }
                    return submenus;
                }
                
                public List<Rol> getRoles(int idUsuario)
                {
                    List<Rol> roles = null;
                    Rol rol;
                    SPIUS sp = new SPIUS("sep_sec_getRoles");// mandarle los parametros para el permiso
                    sp.agregarParametro("idUsuario", idUsuario);
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        DataTable tablaEstado = ds.Tables[0];
                        if (tablaEstado.Rows.Count > 0)
                        {
                            roles = new List<Rol>();
                            foreach (DataRow row in tablaEstado.Rows)
                            {
                                rol = new Rol((int)row["idRol"], row["rol"].ToString(), (bool)row["estado"]);
                                roles.Add(rol);
                            }
                        }
                    }
                    return roles;
                    //return roles;
                }
                public List<Rol> getAllRoles(int idUsuario, int idPagina)
                {
                    // manda a traer solo los activos
                    return this.getAllRoles(idUsuario, idPagina, 1);
                }
                public List<Rol> getAllRoles(int idUsuario, int idPagina,int op)
                {
                    List<Rol> roles = null;
                    Rol rol;
                    SPIUS sp = new SPIUS("sp_sec_getAllRoles");
                    sp.agregarParametro("usuarioEjecutor", idUsuario);
                    sp.agregarParametro("rolesActivos", op); // quiero todos los roles y no solo los activos
                    sp.agregarParametro("idPagina", idPagina);
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        DataTable tb = ds.Tables[0];
                        if (tb.Rows.Count > 0)
                        {
                            roles = new List<Rol>();
                            foreach (DataRow row in tb.Rows)
                            {
                                rol = new Rol((int)row["idRol"], row["rol"].ToString(), Convert.ToBoolean(row["estado"].ToString()));
                                roles.Add(rol);
                            }
                        }
                    }
                    return roles;
                }
                public List<Rol> getRolesFaltantes(int idUsuario, int idUsuarioEjecutor, int idPagina)
                {
                    List<Rol> roles = null;
                    Rol rol;
                    SPIUS sp = new SPIUS("sp_sec_getRolesFaltantesUsuario");
                    sp.agregarParametro("idUsuario", idUsuario);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        DataTable tb = ds.Tables[0];
                        if (tb.Rows.Count > 0)
                        {
                            roles = new List<Rol>();
                            foreach (DataRow row in tb.Rows)
                            {
                                rol = new Rol((int)row["idRol"], row["rol"].ToString(), Convert.ToBoolean(row["estado"].ToString()));
                                roles.Add(rol);
                            }
                        }
                    }
                    return roles;
                }
            #endregion
            
        #endregion
    }
}
