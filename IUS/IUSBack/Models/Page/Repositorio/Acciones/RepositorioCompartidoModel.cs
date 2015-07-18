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

namespace IUSBack.Models.Page.Repositorio.Acciones
{
    public class RepositorioCompartidoModel:PadreModel
    {
        #region "propiedades"
            private ControlCarpeta _controlCarpeta;
            private ControlArchivo _controlArchivo;
        #endregion
        #region "constructores"
            public RepositorioCompartidoModel()
            {
                this._controlCarpeta = new ControlCarpeta();
                this._controlArchivo = new ControlArchivo();
            }
        #endregion
        #region "carpetas"
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
    }
}