using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.REPO.Control;
    using IUSLibs.REPO.Entidades;
namespace IUSBack.Models.Page.Repositorio.Acciones
{
    public class RepositorioModel:PadreModel
    {
        #region "propiedades"
            public ControlCarpeta _controlCarpeta;
            public ControlArchivo _controlArchivo;
        #endregion
        #region "constructores"
            public RepositorioModel()
            {
                this._controlCarpeta = new ControlCarpeta();
                this._controlArchivo = new ControlArchivo();
            }
        #endregion
        #region "get"
            public Dictionary<object, object> sp_repo_entrarCarpeta(Carpeta carpeta,int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    return _controlCarpeta.sp_repo_entrarCarpeta(carpeta, idUsuarioEjecutor, idPagina);
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
            public Dictionary<object, object> sp_repo_getRootFolder(int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlCarpeta.sp_repo_getRootFolder(idUsuarioEjecutor,idPagina);
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
        #region "acciones"
            public Archivo sp_repo_uploadFile(Archivo archivoAgregar,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlArchivo.sp_repo_uploadFile(archivoAgregar, idUsuarioEjecutor, idPagina);
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
            public Carpeta sp_repo_updateCarpeta(Carpeta carpetaActualizar,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlCarpeta.sp_repo_updateCarpeta(carpetaActualizar, idUsuarioEjecutor, idPagina);
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
            public Carpeta sp_repo_insertCarpeta(Carpeta carpeta,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlCarpeta.sp_repo_insertCarpeta(carpeta, idUsuarioEjecutor, idPagina);
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
    }
}