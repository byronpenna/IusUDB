using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUS.Models.general;
// capa de librerias
    using IUSLibs.TRL.Control;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Control;
    //
namespace IUS.Models.page.home.acciones
{
    public class HomeModel:ModeloPadre
    {
        #region "propiedades"
            private int idPagina = 1;
            private string lang;
            #region "Control"
                private ControlIdioma _controlIdioma;
                private ControlSliderImage _controlSlider;
            #endregion
        #endregion
        #region "funciones"
            
            public Idioma sp_trl_getIdiomaFromIds(int idIdioma)
            {
                try
                {
                    return this._controlIdioma.sp_trl_getIdiomaFromIds(idIdioma);
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
            public List<SliderImage> sp_front_getSliderFromPage(int idPagina)
            {
                try
                {
                    return this._controlSlider.sp_front_getSliderFromPage(idPagina);
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
            #region "genericas"
                
            #endregion
        #endregion
        #region "Contructores"
            public HomeModel()
            {
                this._controlIdioma = new ControlIdioma();
                this._controlSlider = new ControlSliderImage();
            }
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