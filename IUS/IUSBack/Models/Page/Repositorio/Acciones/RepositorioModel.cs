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
            private ControlCarpeta _controlCarpeta;
            private ControlArchivo _controlArchivo;
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
                public bool sp_repo_deleteFolder(string rutaRepositorio,int idCarpetaPadre,int idUsuarioEjecutor, int idPagina)
            {
                bool retorno = false;
                try
                {
                    List<Carpeta> carpetasEliminar = this._controlCarpeta.sp_repo_deleteFolder(idCarpetaPadre, idUsuarioEjecutor, idPagina);
                    retorno = true;
                    string path = "";
                    foreach (Carpeta carpeta in carpetasEliminar)
                    {
                        path = rutaRepositorio + idUsuarioEjecutor + "/" + carpeta._idCarpeta;
                        if (System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.Delete(path,true);
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
                return retorno;
            }
            #region "controlArchivo"
                public bool sp_repo_deleteFile(int idArchivo,int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        return this._controlArchivo.sp_repo_deleteFile(idArchivo, idUsuarioEjecutor, idPagina);
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
                public Archivo sp_repo_uploadFile(Archivo archivoAgregar, int idUsuarioEjecutor, int idPagina)
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
                public Archivo sp_repo_refreshSourceFile(Archivo archivoModificar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivo.sp_repo_refreshSourceFile(archivoModificar, idUsuarioEjecutor, idPagina);
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
                public Archivo sp_repo_changeFileName(Archivo archivoModificar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivo.sp_repo_changeFileName(archivoModificar, idUsuarioEjecutor, idPagina);
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
            #region "controlCarpeta"
                public Carpeta sp_repo_updateCarpeta(Carpeta carpetaActualizar, int idUsuarioEjecutor, int idPagina)
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
                public Carpeta sp_repo_insertCarpeta(Carpeta carpeta, int idUsuarioEjecutor, int idPagina)
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
                
        #endregion
    }
}