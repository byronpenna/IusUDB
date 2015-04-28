using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control
{
    public class ControlPermisoUsuarioEvento:PadreLib
    {
        #region "funciones"
            #region "acciones"
                public PermisoUsuarioEvento sp_adminfe_agregarPermisoUsuarioEvento(PermisoUsuarioEvento agregar,int idUsuarioEjecutor,int idPagina)
                {
                    PermisoUsuarioEvento toReturn = null;
                    UsuarioEvento usuarioEvento; PermisoEvento permiso;
                    SPIUS sp = new SPIUS("sp_adminfe_agregarPermisoUsuarioEvento");
                    sp.agregarParametro("idUsuarioEvento", agregar._usuarioEvento._idEventoUsuario);
                    sp.agregarParametro("idPermiso", agregar._permiso._idPermiso);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            usuarioEvento = new UsuarioEvento((int)rowResultado["id_usuarioevento_fk"]);
                            permiso = new PermisoEvento((int)rowResultado["idPermiso"], rowResultado["permiso"].ToString());
                            toReturn = new PermisoUsuarioEvento((int)rowResultado["idPermisoUsuarioEvento"], usuarioEvento,permiso);
                        }
                    }
                    catch (ErroresIUS x) {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return toReturn;
                }
            #endregion
            #region "gets"
                public Dictionary<object, object> sp_adminfe_getPermisosUsuarioEvento(int idUsuarioEvento, int idUsuarioEjecutor, int idPagina)
                {
                    // declare 
                    Dictionary<object, object> toReturn = null;
                    List<PermisoEvento> permisosFaltantes = null;
                    List<PermisoUsuarioEvento> permisosActuales = null;
                    PermisoEvento permiso; PermisoUsuarioEvento permisoUsuarioEvento;
                    UsuarioEvento usuarioEvento;
                    // do it 
                    SPIUS sp = new SPIUS("sp_adminfe_getPermisosUsuarioEvento");
                    sp.agregarParametro("idUsuarioEvento", idUsuarioEvento);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                permisosFaltantes = new List<PermisoEvento>();
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    permiso = new PermisoEvento((int)row["idPermiso"], row["permiso"].ToString());
                                    permisosFaltantes.Add(permiso);
                                }
                            }
                            if (tb[2].Rows.Count > 0)
                            {
                                permisosActuales = new List<PermisoUsuarioEvento>();
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    permiso = new PermisoEvento((int)row["idPermiso"], row["permiso"].ToString());
                                    usuarioEvento = new UsuarioEvento((int)row["id_usuarioevento_fk"]);
                                    permisoUsuarioEvento = new PermisoUsuarioEvento((int)row["idPermisoUsuarioEvento"], usuarioEvento, permiso);
                                    permisosActuales.Add(permisoUsuarioEvento);
                                }
                            }
                            toReturn = new Dictionary<object, object>();
                            toReturn.Add("permisosActuales", permisosActuales);
                            toReturn.Add("permisosFaltantes", permisosFaltantes);
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
        #endregion
    }
}
