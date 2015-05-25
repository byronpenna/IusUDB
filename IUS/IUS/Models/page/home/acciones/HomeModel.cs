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
            public List<LlaveIdioma> getTraduccion(string lang)
            {
                lang = this.getStandarLang(lang);
                ControlLlaveIdioma control = new ControlLlaveIdioma(this.idPagina,lang);
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
            #region "genericas"
                public string getStandarLang(string lang)
                {
                    this._controlIdioma = new ControlIdioma();
                    int index = lang.IndexOf('-');
                    if (index > 0)
                    {
                        this.lang = lang.Substring(0, index);
                    }
                    else
                    {
                        this.lang = lang;
                    }
                    return this.lang;
                }
            #endregion
        #endregion
            #region "Contructores"
            public HomeModel()
            {
                this._controlIdioma = new ControlIdioma();
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