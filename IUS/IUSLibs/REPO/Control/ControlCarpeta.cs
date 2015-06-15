using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// liberias
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Control
{
    public class ControlCarpeta:PadreLib
    {
        #region "Get"
            public Dictionary<object,object> sp_repo_getRootFolder(int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_getRootFolder");
                Dictionary<object, object> retorno = new Dictionary<object,object>();
                List<Carpeta> carpetas = new List<Carpeta>();
                Carpeta carpeta; Usuario usuario; Carpeta carpetaPadre;
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if(this.resultadoCorrectoGet(tb)){
                        if (tb[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in tb[0].Rows)
                            {
                                usuario = new Usuario((int)row["id_usuario_fk"]);
                                carpetaPadre = new Carpeta();
                                carpeta = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(),usuario, carpetaPadre, row["ruta"].ToString());
                                carpetas.Add(carpeta);
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
                retorno.Add("carpetas", carpetas);
                return retorno;
            }
        #endregion
        #region "acciones"
            public Carpeta sp_repo_updateCarpeta(Carpeta carpetaActualizar, int idUsuarioEjecutor, int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_updateCarpeta");
                Carpeta carpetaActualizada=null,carpetaPadre;
                sp.agregarParametro("nombre", carpetaActualizar._nombre);
                sp.agregarParametro("idCarpeta", carpetaActualizar._idCarpeta);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (tb[1].Rows.Count > 0)
                    {
                        DataRow row = tb[1].Rows[0];
                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                        {
                            carpetaPadre = new Carpeta((int)row["id_carpetapadre_fk"]);
                        }
                        else
                        {
                            carpetaPadre = new Carpeta();
                        }
                        carpetaActualizada = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(), (int)row["id_usuario_fk"], carpetaPadre, row["ruta"].ToString());
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
                if (carpetaActualizada == null)
                {
                    ErroresIUS x = new ErroresIUS("Error desconocido", ErroresIUS.tipoError.generico, 0);
                    throw x;
                }
                return carpetaActualizada;
            }
            public Carpeta sp_repo_insertCarpeta(Carpeta carpetaIngresar,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_insertCarpeta");
                sp.agregarParametro("nombre", carpetaIngresar._nombre);
                if (carpetaIngresar._carpetaPadre._idCarpeta > 0)
                {
                    sp.agregarParametro("idCarpetaPadre", carpetaIngresar._carpetaPadre._idCarpeta);
                }
                else
                {
                    sp.agregarParametro("idCarpetaPadre",null);
                }
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                Carpeta carpetaEditada = null;
                Carpeta carpetaPadre;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if(tb[1].Rows.Count > 0){
                            DataRow row = tb[1].Rows[0];
                            if(row["id_carpetapadre_fk"] != DBNull.Value){
                                carpetaPadre = new Carpeta((int)row["id_carpetapadre_fk"]);
                            }else{
                                carpetaPadre = new Carpeta();
                            }
                            carpetaEditada = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(), (int)row["id_usuario_fk"], carpetaPadre, row["ruta"].ToString());
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
                if (carpetaEditada == null)
                {
                    ErroresIUS x = new ErroresIUS("Error desconocido",ErroresIUS.tipoError.generico,0);
                    throw x;
                }
                return carpetaEditada;
            }
        #endregion
    }
}
