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
    }
}
