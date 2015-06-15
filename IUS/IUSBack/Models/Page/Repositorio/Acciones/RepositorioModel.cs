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
namespace IUSBack.Models.Page.Repositorio.Acciones
{
    public class RepositorioModel:PadreModel
    {
        #region "propiedades"
            public ControlCarpeta _controlCarpeta;
        #endregion
        #region "constructores"
            public RepositorioModel()
            {
                this._controlCarpeta = new ControlCarpeta();
            }
        #endregion
        #region "get"
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
            
        #endregion
    }
}