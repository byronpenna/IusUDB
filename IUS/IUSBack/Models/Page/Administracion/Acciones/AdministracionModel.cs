using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Control;
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class AdministracionModel:PadreModel
    {
        #region "propiedades"

        #endregion 
        #region "funciones publicas"
            #region "eventos"
                #region "gets"
                    public List<Evento> sp_adminfe_eventosPropios(int idUsuario, int idPagina)
                    {
                        List<Evento> eventos = null;
                        ControlEvento control = new ControlEvento();
                        try
                        {
                            eventos = control.sp_adminfe_eventosPropios(idUsuario,idPagina);
                        }
                        catch (ErroresIUS)
                        {

                        }
                        catch (Exception)
                        {

                        }
                        return eventos;
                    }
                #endregion
                #region "creacion"
                    public Evento sp_adminfe_crearEvento(Evento eventoAgregar,int idUsuarioEjecutor,int idPagina)
                    {
                        Evento eventoAgregado = null;
                        ControlEvento control = new ControlEvento();
                        try
                        {
                            eventoAgregado = control.sp_adminfe_crearEvento(eventoAgregar, idUsuarioEjecutor, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                        return eventoAgregado;
                    }
                #endregion
            #endregion
            #region "Noticias"

            #endregion
        #endregion

    }
}