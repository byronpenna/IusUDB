using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUS.Models.general;
// librerias externas
using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Control;
    isu
namespace IUS.Models.page.Instituciones.Acciones
{
    public class InstitucionesModel:ModeloPadre
    {
        #region "propiedades"
            private ControlInstitucion _controlInstitucion;
        #endregion
        #region "funciones"
            #region "get"
                public List<insti> sp_frontui_getInstitucionesByContinente(int idContinente,string ip,int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_getInstitucionesByContinente(idContinente, ip, idPagina);
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
        #region "constructores"
            public InstitucionesModel()
            {
                this._controlInstitucion = new ControlInstitucion();
            }
        #endregion
    }
}