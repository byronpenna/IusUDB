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
            private ControlArchivoPublico _controlArchivoPublico;
        #endregion
        #region "constructores"
            public RepositorioPublicoModel()
            {
                this._controlCarpetaPublica = new ControlCarpetaPublica();
                this._controlArchivoPublico = new ControlArchivoPublico();
            }
        #endregion
        #region "funciones"
            #region "get"
                public CarpetaPublica sp_repo_getPublicoByRuta( string strRuta,int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlCarpetaPublica.sp_repo_getPublicoByRuta(strRuta, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> sp_repo_atrasCarpetaPublica(int idCarpeta, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        Dictionary<object, object> archivos = this._controlCarpetaPublica.sp_repo_atrasCarpetaPublica(idCarpeta, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> sp_repo_entrarCarpetaPublica(int idCarpeta, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        Dictionary<object, object> archivos = new Dictionary<object, object>();
                        archivos = this._controlCarpetaPublica.sp_repo_entrarCarpetaPublica(idCarpeta, idUsuarioEjecutor, idPagina);
                        //List<CarpetaPublica> carpetas = this._controlCarpetaPublica.sp_repo_entrarCarpetaPublica(idCarpeta, idUsuarioEjecutor, idPagina);
                        //archivos.Add("carpetas", carpetas);
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
                        archivos.Add("archivos", null);
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
                public ArchivoPublico sp_repo_renameFile(ArchivoPublico archivoEditar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoPublico.sp_repo_renameFile(archivoEditar, idUsuarioEjecutor, idPagina);
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
                public bool sp_repo_removeShareArchivoPublico(int idArchivoPublico, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoPublico.sp_repo_removeShareArchivoPublico(idArchivoPublico, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }catch(Exception x)
                    {
                        throw x;
                    }
                }
                public ArchivoPublico sp_repo_compartirArchivoPublico(ArchivoPublico archivoAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoPublico.sp_repo_compartirArchivoPublico(archivoAgregar, idUsuarioEjecutor, idPagina);
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