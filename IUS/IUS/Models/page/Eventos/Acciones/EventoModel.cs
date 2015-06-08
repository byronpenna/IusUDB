using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas 
    using IUS.Models.general;
// externas
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Eventos.Control;
namespace IUS.Models.page.Eventos.Acciones
{
    public class EventoModel:ModeloPadre
    {
        #region "propiedades"
            public ControlEventos _controlEvento;
        #endregion
        #region "acciones"
            public List<Evento> sp_adminfe_front_getTodayEvents(string ip,int idPagina)
            {
                try
                {
                    return this._controlEvento.sp_adminfe_front_getTodayEvents(ip, idPagina);
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
            public EventoModel()
            {
                this._controlEvento = new ControlEventos();
            }
        #endregion
    }
}