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
    using IUSLibs.SEC.Entidades;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUSLibs.REPO.Control.Publico
{
    public class ControlCarpetaPublica:PadreLib
    {
        #region "funciones"
            #region "get"
        public List<CarpetaPublica> sp_repo_getRootFolderPublico(int idUsuarioEjecutor, int idPagina)
                {
                    List<CarpetaPublica> carpetas = null;
                    SPIUS sp = new SPIUS("sp_repo_getRootFolderPublico");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            { }
                        }
                    }
                    return carpetas;
                }
            #endregion
            #region "set"
                public CarpetaPublica sp_repo_insertCarpetaPublica(CarpetaPublica carpetaPublicaInsert,int idUsuarioEjecutor, int idPagina)
                {
                    CarpetaPublica carpetaPublica = null;
                    SPIUS sp = new SPIUS("sp_repo_insertCarpetaPublica");

                    sp.agregarParametro("nombre", carpetaPublicaInsert._nombre);
                    if(carpetaPublicaInsert._carpetaPadre._idCarpetaPublica > 0){
                        sp.agregarParametro("idCarpetaPadre", carpetaPublicaInsert._carpetaPadre._idCarpetaPublica);
                    }
                    else
                    {
                        sp.agregarParametro("idCarpetaPadre", null);
                    }
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);

                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                CarpetaPublica carpetaPadre;
                                if(row["id_carpetapadre_fk"] != DBNull.Value){
                                    carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                }else{
                                    carpetaPadre = new CarpetaPublica();
                                }
                                carpetaPublica = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
                            }
                        }
                        else
                        {

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
                    return carpetaPublica;

                }
            #endregion
        #endregion
    }
}
