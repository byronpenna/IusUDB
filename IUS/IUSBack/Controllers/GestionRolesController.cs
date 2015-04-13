using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionRoles.acciones;
    using IUSBack.Models.Page.GestionUsuarios.Acciones;
    using IUSBack.Models.Page.GestionPermisos.Acciones;
// librerias externas    
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class GestionRolesController : PadreController
    {
        #region "propiedades"
        public GestionRolesModel _model;
        public int _idPagina = (int)paginas.gestionRoles;
        //public JavaScriptSerializer _jss;
        #endregion
        #region "constructores"
            public GestionRolesController(){
                this._model = new GestionRolesModel();
                this._jss = new JavaScriptSerializer();
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
                        List<Rol> roles = this._model.getAllRoles(usuarioSession._idUsuario,this._idPagina);
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
            
            #region "traer"
                [HttpPost]
                public ActionResult getJSONSubmenuFaltanteYactuales()
                {
                    Dictionary<Object,Object> frm,respuesta;
                    Usuario usuarioSession = this.getUsuarioSesion();
                    frm = this.getAjaxFrm();
                    if (frm != null && usuarioSession != null ) // el error de manejo de session deberia ser aparte
                    {
                        respuesta = new Dictionary<Object, Object>();
                        int idRol = Convert.ToInt32(frm["idRol"].ToString());
                        List<Submenu> submenusFaltandes = this._model.getSubMenuFaltantesRol(idRol, usuarioSession._idUsuario, this._idPagina);
                        List<Submenu> submenuActuales = this._model.getSubmenuRol(idRol,usuarioSession._idUsuario,this._idPagina);
                        respuesta.Add("estado", true);
                        respuesta.Add("subMenusFaltantes", submenusFaltandes);
                        respuesta.Add("subMenusActuales", submenuActuales);
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult getJSONRolesFaltantes()
                {
                    Dictionary<Object, Object> frm,respuesta;
                    Usuario usuario = this.getUsuarioSesion();
                    frm = this.getAjaxFrm("form");
                    if (frm != null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        List<Rol> roles = this._model.getRolesFaltantes(Convert.ToInt32(frm["idUsuario"].ToString()), usuario._idUsuario, this._idPagina);
                        respuesta.Add("roles", roles);
                        respuesta.Add("estado", true);
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult getJSONroles()
                {
                    Dictionary<Object, Object> respuesta,frm;
                    frm = this.getAjaxFrm("form");
                    if (frm != null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        List<Rol> roles = this._model.getRoles(Convert.ToInt32((string)frm["idUsuario"]));
                        respuesta.Add("estado", true);
                        respuesta.Add("roles", roles);
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult getJSONPermisos()
                {
                    Dictionary<Object, Object> frm, respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuarioSesion = this.getUsuarioSesion();
                    if(frm != null && usuarioSesion != null){// manejar alguna vez mas sofisticado para usuario sesion
                        respuesta = new Dictionary<Object, Object>();
                        GestionPermisosModel controlLocal = new GestionPermisosModel();
                        // vars 
                        int idSubMenu = Convert.ToInt32(frm["idSubMenu"].ToString());int idRol = Convert.ToInt32(frm["idRol"].ToString());
                        // do it 
                        List<RolSubMenuPermiso> permisos   = controlLocal.getPermisosSubmenuRol(idSubMenu,idRol , usuarioSesion._idUsuario, this._idPagina);
                        List<PermisoRol> permisosFaltantes = controlLocal.getPermisosSubmenuRolFaltantes(idSubMenu, idRol, usuarioSesion._idUsuario, this._idPagina);
                        //Permiso permisos = new Permiso();
                        respuesta.Add("estado", true);
                        respuesta.Add("permisos", permisos);
                        respuesta.Add("permisosFaltantes",permisosFaltantes);
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
            #endregion
            #region "acciones"
                [HttpPost]
                public ActionResult eliminarPermisoSubmenuRol()
                {
                    Dictionary<Object,Object> frm,respuesta = null;
                    frm = this.getAjaxFrm();
                    Usuario usuariosesion = this.getUsuarioSesion();
                    GestionPermisosModel control = new GestionPermisosModel();
                    if (frm != null && usuariosesion != null) // pensar en algo bueno para usuariosession
                    {
                        respuesta = new Dictionary<Object, Object>(); // meter esto en lo comentado
                        try
                        {
                            bool elimino = control.eliminarPermisoSubmenuRol(Convert.ToInt32(frm["idRolSubmenuPermiso"].ToString()), usuariosesion._idUsuario, this._idPagina);
                            respuesta.Add("estado", elimino);
                        }
                        catch (ErroresIUS)
                        {
                            respuesta.Add("estado", false);
                        }
                        catch (Exception) {
                            respuesta.Add("estado", false);
                        }
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }      
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
                [HttpPost]
                public ActionResult agregarRoles()
                {
                    Dictionary<Object, Object> frm, respuesta;
                    Usuario usuario = this.getUsuarioSesion();
                    bool agrego = false;
                    frm = this.getAjaxFrm();
                    if (frm != null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        Object[] rolesobject = (Object[])frm["rolesAgregar"];
                        int[] roles = new int[rolesobject.Length];
                        int cn = 0;
                        foreach (Object obj in rolesobject)
                        {
                            roles[cn] = Convert.ToInt32(obj);
                            cn++;
                        }
                        int idUsuario = Convert.ToInt32(frm["idUsuario"].ToString());
                        agrego = this._model.agregarRoles(roles, idUsuario, usuario._idUsuario, this._idPagina);
                        if (agrego)
                        {
                            List<Rol> rolesUsuario = this._model.getRoles(idUsuario);
                            respuesta.Add("estado", true);
                            respuesta.Add("roles", rolesUsuario);
                        }
                        else
                        {
                            respuesta.Add("estado", false);
                        }
                    }
                    else
                    {
                        respuesta = this.errorEnvioFrmJSON();
                    }
                    return Json(respuesta);
                }
            #endregion
        #endregion
    }
}
