using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// propias
        using IUSLibs.LOGS;
    // control
        using IUSBack.Models.General;
        using IUSLibs.SEC.Control;
        using IUSLibs.SECPU.Control;
        using IUSLibs.FrontUI.Eventos.Control;
    // entidades 
        using IUSLibs.ADMINFE.Entidades;
        using IUSLibs.SEC.Entidades;
        using IUSLibs.SECPU.Entidades;
namespace IUSBack.Models.Page.Home.Acciones
{
    public class HomeModel:PadreModel
    {
        #region "get"
            public String abrirSubMenu(String subMenuString)
        {
            String toReturn = "<ul>" +
                       subMenuString;
            return toReturn;
        }
        #endregion
        #region "acciones"
            #region "funciones especiales"
                public List<Evento> sp_adminfe_front_getTodayEvents(string ip,int idPagina)
                {
                    try
                    {
                        ControlEventos evt = new ControlEventos();
                        return evt.sp_adminfe_front_getTodayEvents(ip, idPagina);
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
            #region "Registro de usuarios publicos"
                public Usuario sp_usu_changePass(string pass, string ip, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        ControlUsuarios controlUsuario = new ControlUsuarios();
                        return controlUsuario.sp_usu_changePass(pass, ip, idUsuarioEjecutor, idPagina);
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
                public bool sp_secpu_verificarCuenta(int num, int idUsuario)
                {
                    try
                    {
                        ControlCodigoVerificacion control = new ControlCodigoVerificacion();
                        return control.sp_secpu_verificarCuenta(num, idUsuario);
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
                public UsuarioPublico sp_secpu_addUsuario(UsuarioPublico usuarioAgregar)
                {
                    try
                    {
                        ControlUsuarioPublico control = new ControlUsuarioPublico();
                        return control.sp_secpu_addUsuario(usuarioAgregar);
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