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
            private ControlIdioma _controlIdioma;
        #endregion 
        #region "funciones"
            public List<Idioma> getIdiomas()
            {
                List<Idioma> idiomas = null;
                try
                {
                    return this._controlIdioma.sp_trl_getAllIdiomas(1, 1);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return idiomas;
            }
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
            this._controlIdioma = new ControlIdioma();
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