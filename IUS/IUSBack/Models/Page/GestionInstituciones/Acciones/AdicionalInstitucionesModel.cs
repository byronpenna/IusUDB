using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    // generales
        using IUSLibs.LOGS;
    // otras
        //using IUSLibs.FrontUI.Entidades;
        using IUSLibs.FrontUI.Control;
        using IUSLibs.RRHH.Control.Formacion;
namespace IUSBack.Models.Page.GestionInstituciones.Acciones
{
    public class AdicionalInstitucionesModel:PadreModel
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
        public Dictionary<object, object> getInfoInicialAdicionalInstituciones(int idUsuarioEjecutor,int idPagina)
        {
            Dictionary<object, object> retorno = new Dictionary<object, object>();
            try
            {
                ControlNivelesEducaion  controlNivel    = new ControlNivelesEducaion();
                ControlAreaCarrera      controlArea     = new ControlAreaCarrera();
                retorno.Add("nivelesEducacion", controlNivel.sp_frontui_getNivelesEducacion(idUsuarioEjecutor, idPagina));
                retorno.Add("areasConocimiento", controlArea.sp_rrhh_getAreasCarreras(idUsuarioEjecutor, idPagina));
                return retorno;
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