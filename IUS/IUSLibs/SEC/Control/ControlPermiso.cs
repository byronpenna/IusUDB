using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS; // para el manejo de errores
namespace IUSLibs.SEC.Control
{
    public class ControlPermiso:PadreLib
    {
        #region "propiedades"
        #endregion
        #region "funciones privadas"
            private void setVarsPermisos(int permiso,ref bool Crear,ref bool Editar,ref bool Eliminar,ref bool Ver){
                switch (permiso)
                {
                    case (int)Permisos.Crear:
                        {
                            Crear = true;
                            break;
                        }
                    case (int)Permisos.Editar:
                        {
                            Editar = true;
                            break;
                        }
                    case (int)Permisos.Eliminar:
                        {
                            Eliminar = true;
                            break;
                        }
                    case (int)Permisos.ver:
                        {
                            Ver = true;
                            break;
                        }
                }
            }
        #endregion
        #region "funciones publicas"
            public Permiso getPermisosSubmenuRol(int idSubMenu,int idRol,int idUsuarioEjecutor,int idPagina)
            {
                
                Permiso permiso = null;
                bool Crear=false,Editar=false,Eliminar=false,Ver = false;
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
                            int nPermiso;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                 nPermiso = (int)row["id_permiso_rol_fk"];
                                 this.setVarsPermisos(nPermiso,ref Crear,ref Editar,ref Eliminar,ref Ver);
                            }
                            permiso = new Permiso(Crear, Editar, Eliminar, Ver);
                        }
                    }
                }catch(ErroresIUS x){
                    throw x;
                }catch(Exception x){
                    throw x;
                }
                return permiso;
            }
            public Permiso sp_trl_getAllPermisoPagina(int idUsuarioEjecutor,int idPagina)
            {
                Permiso permisos = null;
                SPIUS sp = new SPIUS("sp_trl_getAllPermisoPagina");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                bool crear=false, editar=false, eliminar = false, ver = false;
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if(ds.Tables[0].Rows.Count>0){
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                switch ((int)row["permiso"])
                                {
                                    case 1:
                                        {
                                            crear = true;
                                            break;
                                        }
                                    case 2:
                                        {
                                            editar = true;
                                            break;
                                        }
                                    case 3:
                                        {
                                            eliminar = true;
                                            break;
                                        }
                                    case 4:
                                        {
                                            ver = true;
                                            break;
                                        }
                                }
                            }
                        }
                        permisos = new Permiso(crear, editar, eliminar, ver);
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
                return permisos;
            }
        #endregion
        #region "contructores"
        #endregion
    }
}
