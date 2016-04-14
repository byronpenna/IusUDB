using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUS.Models.general;
// otros modelos
    using IUS.Models.Entidades;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// capa de librerias
    using IUSLibs.TRL.Control;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Control;
    using IUSLibs.ADMINFE.Pantalla;
    //
    using IUSLibs.FrontUI.Eventos.Control;
namespace IUS.Models.page.home.acciones
{
    public class HomeModel:ModeloPadre
    {
        #region "propiedades"
            //private int idPagina = 1;
            private string lang;
            #region "Control"
                private ControlIdioma _controlIdioma;
                private ControlSliderImage _controlSlider;
            #endregion
        #endregion
        #region "funciones"
                public List<NoticiaEvento> sp_adminfe_front_pantallaHome(int n, string ip, int idPagina)
                {
                    try
                    {
                        PantallaHome pantalla = new PantallaHome();
                        List<NoticiaEvento> noticiasEventos = null;
                        NoticiaEvento notiEvento;
                        DataRowCollection rows = pantalla.sp_adminfe_front_pantallaHome(n,ip,idPagina);
                        if (rows.Count > 0)
                        {
                            noticiasEventos = new List<NoticiaEvento>();
                            foreach (DataRow row in rows)
                            {
                                notiEvento = new NoticiaEvento((int)row["id"], row["titulo"].ToString(), row["descripcion"].ToString(), (int)row["tipoEntrada"]);
                                noticiasEventos.Add(notiEvento);
                            }
                        }
                        return noticiasEventos;
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
            
            public List<Evento> sp_adminfe_front_getMonthEvents(string ip,int idPagina)
            {
                
                try
                {
                    ControlEventos control = new ControlEventos();
                    return control.sp_adminfe_front_getMonthEvents(ip, idPagina);
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x) {
                    throw x;
                }

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