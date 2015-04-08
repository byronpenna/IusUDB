using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas 
    using IUSLibs.GENERALS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.BaseDatos;

namespace IUSLibs.SEC.Control
{
    public class ControlRoles:PadreLib
    {
        #region "Funciones"
            public bool desasociarRol(int idUsuario,int idRol)
            {
                bool toReturn = false;
                SPIUS sp = new SPIUS("sp_sec_desasociarRoles");
                sp.agregarParametro("idUsuario", idUsuario);
                sp.agregarParametro("idRol", idRol);
                DataSet ds = sp.EjecutarProcedimiento();
                if (!this.DataSetDontHaveTable(ds))
                {
                    if (Convert.ToBoolean((int)ds.Tables[0].Rows[0]["estadoDelete"]))
                    {
                        toReturn = true;
                    }
                }
                return toReturn;
            }
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
                List<Rol> roles = null;
                Rol rol;
                SPIUS sp = new SPIUS("sp_sec_getAllRoles");
                sp.agregarParametro("usuarioEjecutor", idUsuario);
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
    }
}
