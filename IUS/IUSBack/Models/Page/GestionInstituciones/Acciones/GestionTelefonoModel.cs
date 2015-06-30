using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.FrontUI.Control;
namespace IUSBack.Models.Page.GestionInstituciones.Acciones
{
    public class GestionTelefonoModel
    {
        #region "propiedades"
            private ControlTelefonoInstitucion _controlTelefono;
        #endregion
        #region "funciones"
            public TelefonoInstitucion sp_frontui_insertTelInstitucion(TelefonoInstitucion telefonoIngresar, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    return this._controlTelefono.sp_frontui_insertTelInstitucion(telefonoIngresar, idUsuarioEjecutor, idPagina);
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
        #region "constructores"
            public GestionTelefonoModel()
            {
                this._controlTelefono = new ControlTelefonoInstitucion();
            }
        #endregion
    }
}