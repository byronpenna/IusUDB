using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas 
    using IUS.Models.general;
// externas 
    using IUSLibs.LOGS;
    using IUSLibs.REPO.Control.Publico;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUS.Models.page.Repositorio.Acciones
{
    public class RepositorioModel:ModeloPadre
    {
        #region "propiedades"
            private ControlCarpetaPublica _controlCarpetaPublica;
        #endregion
            #region "funciones"
                #region "get"
                    public List<CarpetaPublica> sp_repo_front_GetAllCarpetasPublica(int idCarpetaPadre, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlCarpetaPublica.sp_repo_front_GetAllCarpetasPublica(idCarpetaPadre, ip, idPagina);
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
                #endregion
            #endregion
        #region "constructores"
            public RepositorioModel()
            {
                this._controlCarpetaPublica = new ControlCarpetaPublica();
            }
        #endregion
    }
}