using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    // sec
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
    // repo
    using IUSLibs.REPO.Control;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.REPO.Entidades.Compartido;
    using IUSLibs.REPO.Control.Compartido;
namespace IUSBack.Models.Page.Repositorio.Acciones
{
    public class RepositorioCompartidoModel:PadreModel
    {
        #region "propiedades"
            private ControlCarpeta _controlCarpeta;
            private ControlArchivoCompartido _controlArchivoCompartido;
        #endregion
        #region "constructores"
            public RepositorioCompartidoModel()
            {
                this._controlCarpeta            = new ControlCarpeta();
                this._controlArchivoCompartido  = new ControlArchivoCompartido();
            }
        #endregion
        #region "Funciones"
            #region "repositorio compartido"
                public ArchivoCompartido sp_repo_compartirArchivo(ArchivoCompartido archivoAgregar, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    return this._controlArchivoCompartido.sp_repo_compartirArchivo(archivoAgregar, idUsuarioEjecutor, idPagina);
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
                public List<Usuario> sp_repo_getUsuariosArchivosCompartidos(int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoCompartido.sp_repo_getUsuariosArchivosCompartidos(idUsuarioEjecutor, idPagina);
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
                public List<ArchivoCompartido> sp_repo_getFilesFromShareUserId(int idUserFile,int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoCompartido.sp_repo_getFilesFromShareUserId(idUserFile, idUsuarioEjecutor, idPagina);
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
                public bool sp_repo_removeShareFile(int idArchivo,int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlArchivoCompartido.sp_repo_removeShareFile(idArchivo, idUsuarioEjecutor, idPagina);
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
            #region "otras"
                public List<Usuario> sp_sec_getAllUsuarios(int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlUsuarios control = new ControlUsuarios();
                        return control.sp_sec_getAllUsuarios(idUsuarioEjecutor, idPagina);
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
            #region "archivos"
                public Dictionary<object, object> sp_repo_entrarCarpeta(Carpeta carpeta, int idUsuarioEjecutor, int idPagina)
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
                public Dictionary<object, object> sp_repo_getRootFolder(int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlCarpeta.sp_repo_getRootFolder(idUsuarioEjecutor, idPagina);
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