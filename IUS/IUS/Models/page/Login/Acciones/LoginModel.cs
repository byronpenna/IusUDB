using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUS.Models.general;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SECPU.Control;
    using IUSLibs.SECPU.Entidades;
namespace IUS.Models.page.Login.Acciones
{
    public class LoginModel:ModeloPadre
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
            public ValidadorPassPublico sp_secpu_solicitarCambio(string email)
            {
                try
                {
                    ControlUsuarioPublico control = new ControlUsuarioPublico();
                    return control.sp_secpu_solicitarCambio(email);
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
            public UsuarioPublico           sp_adminfe_front_getLogin(string email,string pass,string ip,int idPagina)
            {
                try
                {
                    ControlUsuarioPublico control = new ControlUsuarioPublico();
                    return control.sp_adminfe_front_getLogin(email, pass, ip, idPagina);
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
            
        #endregion
    }
}