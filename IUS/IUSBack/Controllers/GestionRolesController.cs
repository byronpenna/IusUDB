using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionRoles.acciones;
// librerias externas    
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers
{
    public class GestionRolesController : PadreController
    {
        #region "propiedades"
        public GestionRolesModel _model;
        #endregion
        #region "constructores"
            public GestionRolesController(){
                this._model = new GestionRolesModel();
            }
        #endregion
        #region "ajax functions"
            [HttpPost]
            public ActionResult getJSONroles()
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<Object, Object> respuesta,frm;
                String frmText = Request.Form["form"];
                if (frmText != null)
                {
                    frm = jss.Deserialize<Dictionary<Object, Object>>(frmText);
                    respuesta = new Dictionary<Object, Object>();
                    List<Rol> roles = this._model.getRoles(Convert.ToInt32((string)frm["idUsuario"]));
                    respuesta.Add("estado", true);
                    respuesta.Add("roles", roles);
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                //List<Rol> roles = this._model.getRoles()
                return Json(respuesta);
            }
        #endregion
    }
}
