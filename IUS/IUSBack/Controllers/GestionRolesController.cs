﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionRoles.acciones;
    using IUSBack.Models.Page.GestionUsuarios.Acciones;
// librerias externas    
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Controllers
{
    public class GestionRolesController : PadreController
    {
        #region "propiedades"
        public GestionRolesModel _model;
        public int _idPagina = (int)paginas.gestionRoles;
        #endregion
        #region "constructores"
            public GestionRolesController(){
                this._model = new GestionRolesModel();
            }
        #endregion
        #region "URL"
            public ActionResult Index()
            {
                
                if (Session["usuario"] != null)
                {

                    GestionUsuarioModel usuariosModel = new GestionUsuarioModel((int)paginas.usuarios);
                    Usuario usuarioSession = (Usuario)Session["usuario"];
                    // traer data
                        List<Usuario> usuarios = usuariosModel.getUsuarios(usuarioSession._idUsuario);
                        List<Rol> roles = this._model.getRoles(usuarioSession._idUsuario,this._idPagina);
                    // fill viewbag
                        ViewBag.subMenus    = this._model.getMenuUsuario(usuarioSession._idUsuario);
                        ViewBag.roles       = roles;
                        ViewBag.usuarios    = usuarios;
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
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
            [HttpPost]
            public ActionResult desasociarRolUsuario()
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<Object, Object> respuesta, frm;
                String frmText = Request.Form["form"];
                if (frmText != null)
                {
                    respuesta = new Dictionary<Object, Object>();
                    frm = jss.Deserialize<Dictionary<Object, Object>>(frmText);
                    Boolean estado = this._model.desasociarRol(Convert.ToInt32(frm["idRol"].ToString()), Convert.ToInt32(frm["idUsuario"].ToString()));
                    respuesta.Add("estado", estado);
                }
                else
                {
                    respuesta = this.errorEnvioFrmJSON();
                }
                return Json(respuesta);
            }
        #endregion
    }
}
