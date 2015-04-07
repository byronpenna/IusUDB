using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        #endregion
        #region "funciones"
        public Dictionary<Object,Object> errorEnvioFrmJSON(){
            Dictionary<Object,Object> toReturn = new Dictionary<Object, Object>();
            toReturn.Add("estado", false);
            toReturn.Add("error", "Formulario no se envio correctamente");
            return toReturn;
        }
        #endregion
        #region "contructores"
            public PadreController()
            {

            }
        #endregion
        
    }
}
