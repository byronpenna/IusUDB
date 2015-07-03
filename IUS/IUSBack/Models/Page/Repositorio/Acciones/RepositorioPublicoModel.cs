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
            #region "set"
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
            #endregion
        #endregion
    }
}