using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas
    using IUS.Models.Entidades;
// data
    using System.Data;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.ADMINFE.Control.Noticias;
    using IUSLibs.TRL.Control;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.ADMINFE.Pantalla;
namespace IUS.Models.general 
{
    public class ModeloPadre
    {
        #region "propiedades"
            private ControlPost _controlPost;
            private ControlIdioma _controlIdioma;
            private string _lang;
        #endregion
        #region "funciones"
            
            public List<NoticiaEvento> sp_adminfe_front_pantallaHome(int n, int pagina,string ip, int idPagina)
            {
                try
                {
                    PantallaHome pantalla = new PantallaHome();
                    List<NoticiaEvento> noticiasEventos = null;
                    NoticiaEvento notiEvento;
                    DataRowCollection rows = pantalla.sp_adminfe_front_pantallaHome(n, ip, idPagina);
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
            #region "getTopNoticias"
                public List<Post> sp_adminfe_front_getTopNoticias(int n,string userLang="")
                {
                    string lang = this.getStandarLang(userLang);
                    try
                    {
                        return this._controlPost.sp_adminfe_front_getTopNoticias(n,lang);
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
                /*
                public List<Post> sp_adminfe_front_getTopNoticias(int n)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_front_getTopNoticias(n);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }*/
            #endregion
            #region "traducciones"
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
                public string getStandarLang(string lang)
                {
                    this._controlIdioma = new ControlIdioma();
                    int index = lang.IndexOf('-');
                    if (index > 0)
                    {
                        this._lang = lang.Substring(0, index);
                    }
                    else
                    {
                        this._lang = lang;
                    }
                    return this._lang;
                }
                public List<LlaveIdioma> getTraduccion(string lang,int idPagina)
                {
                    lang = this.getStandarLang(lang);
                    //asdqw
                    //asdqwe
                    ControlLlaveIdioma control = new ControlLlaveIdioma(idPagina, lang);
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
        #endregion
        #region "constructores"
            public ModeloPadre()
            {
                this._controlPost = new ControlPost();
                this._controlIdioma = new ControlIdioma();
            }
        #endregion
    }
}