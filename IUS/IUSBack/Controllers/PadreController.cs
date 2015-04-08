using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// sistema 
    using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class PadreController : Controller
    {
        //
        // GET: /Padre/
        #region "propiedades"
        public enum paginas
        {
            usuarios = 3,gestionRoles = 5,gestionPersonas=4
        }
        protected JavaScriptSerializer _jss;
        #endregion
        #region "funciones"
        public Dictionary<Object,Object> errorEnvioFrmJSON(){
            Dictionary<Object,Object> toReturn = new Dictionary<Object, Object>();
            toReturn.Add("estado", false);
            toReturn.Add("error", "Formulario no se envio correctamente");
            return toReturn;
        }
        public Usuario getUsuarioSesion()
        {
            Usuario usuario = null;
            if (Session["usuario"] != null)
            {
                usuario = (Usuario)Session["usuario"];
            }
            return usuario;
        }
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
                toReturn = this._jss.Deserialize<Dictionary<Object, Object>>(frmText);
            }
            return toReturn;
        }
        #endregion
        #region "contructores"
            public PadreController()
            {
                this._jss = new JavaScriptSerializer();
            }
        #endregion
        
    }
}
