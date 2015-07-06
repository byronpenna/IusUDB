using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.REPO.Control.Publico;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUSBack.Models.Page.Repositorio.Acciones
{
    public class RepositorioPublicoModel:PadreModel
    {
        #region "propiedades"
            private ControlCarpetaPublica _controlCarpetaPublica;
        #endregion
        #region "constructores"
            public RepositorioPublicoModel()
            {
                this._controlCarpetaPublica = new ControlCarpetaPublica();
            }
        #endregion
        #region "funciones"
            #region "get"
                public Dictionary<object, object> sp_repo_entrarCarpetaPublica(int idCarpeta, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        Dictionary<object, object> archivos = new Dictionary<object, object>();
                        List<CarpetaPublica> carpetas = this._controlCarpetaPublica.sp_repo_entrarCarpetaPublica(idCarpeta, idUsuarioEjecutor, idPagina);
                        archivos.Add("carpetas", carpetas);
                        return archivos;
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    
                }
                public Dictionary<object,object> sp_repo_getRootFolderPublico(int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        Dictionary<object,object> archivos = new Dictionary<object,object>();
                        List<CarpetaPublica> carpetas = this._controlCarpetaPublica.sp_repo_getRootFolderPublico(idUsuarioEjecutor, idPagina);
                        archivos.Add("carpetas", carpetas);
                        return archivos;
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
            #endregion
            #region "set"
                public bool sp_repo_deleteCarpetaPublica(int idCarpetaPublica, int idUsuarioEjecutor, int idPagina){
                    try
                    {
                        return this._controlCarpetaPublica.sp_repo_deleteCarpetaPublica(idCarpetaPublica, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                public CarpetaPublica sp_repo_insertCarpetaPublica(CarpetaPublica carpetaPublicaInsert, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlCarpetaPublica.sp_repo_insertCarpetaPublica(carpetaPublicaInsert, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                public CarpetaPublica sp_repo_updateCarpetaPublica(CarpetaPublica carpetaPublicaUpdate, int idUsuarioEjecutor, int idPagina)
                {
                    try{
                        return this._controlCarpetaPublica.sp_repo_updateCarpetaPublica(carpetaPublicaUpdate, idUsuarioEjecutor, idPagina);
                    }catch(ErroresIUS x){
                        throw x;
                    }
                    catch(Exception x){
                        throw x;
                    }
                    
                }
            #endregion
        #endregion
    }
}