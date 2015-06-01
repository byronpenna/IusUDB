using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// sistema
    using System.Web.Script.Serialization;
// librerias internas
    using IUS.Models.general;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Entidades;
    using IUSLibs.TRL.Control;
namespace IUS.Controllers
{
    public class PadreController : Controller
    {
        #region "propiedades"
            public enum paginas { 
                home=1
            }
            public enum cookiesSistema {
                Principal=0,idioma = 0
            }
            protected JavaScriptSerializer _jss;
            private ModeloPadre _model;
        #endregion
        #region "funciones"
            #region "manejo de errores"
                public Dictionary<Object, Object> errorTryControlador(int errorType, object obj)
                {
                    Dictionary<Object, Object> retorno = new Dictionary<object, object>();
                    retorno.Add("estado", false);
                    retorno.Add("errorType", errorType);
                    retorno.Add("error", obj);
                    return retorno;
                }
                public Dictionary<object, object> errorEnvioFrmJSON()
                {
                    Dictionary<Object, Object> toReturn = new Dictionary<Object, Object>();
                    ErroresIUS x = new ErroresIUS("Formulario no se envio correctamente", ErroresIUS.tipoError.generico, 0);
                    toReturn.Add("estado", false);
                    toReturn.Add("errorType", 3);
                    toReturn.Add("error",x);
                    return toReturn;
                }
            #endregion
            #region "genericas"
                #region "getAjaxFrm"
                    public Dictionary<Object, Object> getAjaxFrm()
                    {
                        return this.getAjaxFrm("form");
                    }
                    public Dictionary<Object, Object> getAjaxFrm(String txtObj)
                    {
                        Dictionary<Object, Object> toReturn = null;
                        String frmText = Request.Form[txtObj];
                        if (frmText != null || frmText != "")
                        {
                            try
                            {
                                toReturn = this._jss.Deserialize<Dictionary<Object, Object>>(frmText);
                            }
                            catch (Exception x)
                            {
                                throw x;
                            }
                        }
                        return toReturn;
                    }
            
                #endregion

                public string getUserLang()
                {
                    String[] lng = Request.UserLanguages;
                    string lang = lng[0];
                    HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["IUSidioma"];
                    if (cookie != null)
                    {
                        lang = cookie.Value;
                    }
                    return lang;
                }
                public void setTraduccion(List<LlaveIdioma> traducciones)
                {
                    if (traducciones != null)
                    {
                        foreach (LlaveIdioma traduccion in traducciones)
                        {
                            ViewData[traduccion._llave._llave] = traduccion._traduccion;
                        }
                    }
                }
            #endregion
            #region "conversiones"
                #region "arrays"
                    public int[] convertArrAjaxToInt(Object[] frm)
                    {
                        int[] toReturn = new int[frm.Length];
                        int cn = 0;
                        foreach (object obj in frm)
                        {
                            toReturn[cn] = Convert.ToInt32(obj);
                            cn++;
                        }
                        return toReturn;
                    }
                    public string[] converArrAajaxToString(Object[] frm)
                    {
                        string[] toReturn;
                        if (frm.Length > 0)
                        {
                            toReturn = new string[frm.Length];
                            int cn = 0;
                            foreach (object obj in frm)
                            {
                                toReturn[cn] = Convert.ToString(obj);
                                cn++;
                            }
                        }
                        else
                        {
                            toReturn = null;
                        }
                        return toReturn;
                    }
                #endregion
                #region "simples"
                    public DateTime convertObjAjaxToDateTime(string date, string hora)
                    {
                        string fechaCompleto = date + " " + hora;
                        DateTime toReturn;
                        try
                        {
                            toReturn = Convert.ToDateTime(Convert.ToDateTime(fechaCompleto).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                        return toReturn;
                    }
                    public int convertObjAjaxToInt(object obj)
                    {
                        return Convert.ToInt32(obj.ToString());
                    }
                #endregion
            #endregion
            
        #endregion
        #region "constructores"
            public PadreController()
            {
                this._jss = new JavaScriptSerializer();
                this._model = new ModeloPadre();
                ViewBag.idiomas = this._model.getIdiomas();
            }
        #endregion
    }
}
