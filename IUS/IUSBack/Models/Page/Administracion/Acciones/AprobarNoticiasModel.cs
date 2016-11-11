using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// liberias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    // entidades
        using IUSLibs.ADMINFE.Entidades.Noticias;
    // control
        using IUSLibs.ADMINFE.Control.Noticias;
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class AprobarNoticiasModel
    {
        #region "sets"
            public NotiEvento sp_adminfe_cambiarEstadoPublicacion(NotiEvento notiCambiar,int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    ControlAprobacion control = new ControlAprobacion();
                    return control.sp_adminfe_cambiarEstadoPublicacion(notiCambiar, idUsuarioEjecutor, idPagina);
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
            public NotiEvento sp_adminfe_aprobarNoticia_cambiarEstado(NotiEvento notiCambiar, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    ControlPost control = new ControlPost();
                    return control.sp_adminfe_aprobarNoticia_cambiarEstado(notiCambiar, idUsuarioEjecutor, idPagina);
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
        #region "gets"
            public List<NotiEvento> sp_adminfe_aprobarnoticia_getNoticiasAprobar(int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    ControlPost control = new ControlPost();
                    return control.sp_adminfe_aprobarnoticia_getNoticiasAprobar(idUsuarioEjecutor, idPagina);
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