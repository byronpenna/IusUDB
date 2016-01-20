using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.GENERALS;
namespace IUSBack.Models.Page.Errors
{
    public class ErrorsModel:PadreModel
    {
        public bool sp_sec_registrarError(string mensaje,string detalle,int idUsuarioEjecutor,int idPagina)
        {
            bool retorno = false;
            try
            {
                PadreLib padre = new PadreLib();
                retorno = padre.sp_sec_registrarError(mensaje, detalle, idUsuarioEjecutor, idPagina);
            }
            catch (ErroresIUS)
            {
                
            }
            catch (Exception)
            {
                
            }
            return retorno;
        }
    }
}