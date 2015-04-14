using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Control;
namespace IUSBack.Models.Page.GestionIdiomaWebsite.Acciones
{
    public class GestionIdiomaWebsiteModel:PadreModel
    {
        #region "propiedades"
            
        #endregion
        #region "funciones publicas"
            public List<Idioma> sp_tra_getAllIdiomas(int idUsuarioEjecutor,int idPagina)
            {
                ControlIdioma control = new ControlIdioma();
                List<Idioma> idiomas = null;
                try{
                     idiomas = control.sp_tra_getAllIdiomas(idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return idiomas;
            }
        #endregion 
        #region
            public GestionIdiomaWebsiteModel()
            {

            }
        #endregion 

    }
}
