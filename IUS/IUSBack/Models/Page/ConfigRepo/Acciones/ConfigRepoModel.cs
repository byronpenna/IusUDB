using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
    // repositorio    
        using IUSLibs.REPO.Entidades;
        using IUSLibs.REPO.Control;
        using IUSLibs.REPO.Pantalla;
namespace IUSBack.Models.Page.ConfigRepo.Acciones
{
    public class ConfigRepoModel:PadreModel
    {
        #region "funciones"
            #region "do"
                public ExtensionArchivo sp_repo_actualizarTipoArchivoExt(ExtensionArchivo extension, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlExtensionArchivo control = new ControlExtensionArchivo();
                        return control.sp_repo_actualizarTipoArchivoExt(extension, idUsuarioEjecutor, idPagina);
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
            #region "get"
                public List<TipoArchivo> sp_repo_getTipoArchivo(string lang, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlTipoArchivo control = new ControlTipoArchivo();
                        return control.sp_repo_getTipoArchivo(lang, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> sp_repo_inicialesConfigRepo(string lang, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        PantallaControlConfig pantalla = new PantallaControlConfig();
                        return pantalla.sp_repo_inicialesConfigRepo(lang, idUsuarioEjecutor, idPagina);
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