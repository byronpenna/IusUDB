using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUS.Models.general;
// capa de librerias
    using IUSLibs.TRL.Control;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
namespace IUS.Models.page.home.acciones
{
    public class HomeModel:ModeloPadre
    {
        #region "propiedades"
            private int idPagina = 1;
            private string lang;
        #endregion 
        #region "funciones"
            public List<LlaveIdioma> getTraduccion()
            {
                ControlLlaveIdioma control = new ControlLlaveIdioma(this.idPagina,this.lang);
                List<LlaveIdioma> traduccion;
                try
                {
                    traduccion = control.getLlavesSitio();
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return traduccion;
            }
        #endregion
        #region "Contructores"
        public HomeModel(string pidIdioma)
        {
            //this.lang = pidIdioma;
          int index = pidIdioma.IndexOf('-');
            if(index > 0){
                this.lang = pidIdioma.Substring(0,index);
            }else{
                this.lang = pidIdioma;
            }
        }
        #endregion
        
    }
}