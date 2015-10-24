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
    using IUSBack.Models.Page.GestionRolSubmenu.Acciones;
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
                Usuario usuarioSession = this.getUsuarioSesion();
                ViewBag.titleModulo = "Gestión de permisos";
                ViewBag.usuario = usuarioSession;
                if (usuarioSession != null)
                {

                    ViewBag.selectedMenu = 2; // menu seleccionado 
                    GestionUsuarioModel usuariosModel = new GestionUsuarioModel((int)paginas.usuarios);
                    // traer data
                        Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                        List<Usuario> usuarios = usuariosModel.getUsuarios(usuarioSession._idUsuario);
                        List<Rol> roles = this._model.getAllRoles(usuarioSession._idUsuario,this._idPagina);
                        List<Rol> rolesTabla = this._model.getAllRoles(usuarioSession._idUsuario, this._idPagina,0);
                    // fill viewbag
                        if (permisos != null && permisos._ver)
                        {
                            ViewBag.menus = this._model.sp_sec_getMenu(usuarioSession._idUsuario);
                            ViewBag.roles = roles;
                            ViewBag.rolesTabla = rolesTabla;
                            ViewBag.usuarios = usuarios;
                            ViewBag.permisos = permisos;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("NotAllowed", "Errors");
                        }
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
                    
                    respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        int idRol = Convert.ToInt32(frm["idRol"].ToString());
                        List<Submenu> submenusFaltandes = this._model.getSubMenuFaltantesRol(idRol, usuarioSession._idUsuario, this._idPagina);
                        List<Submenu> submenuActuales = this._model.getSubmenuRol(idRol,usuarioSession._idUsuario,this._idPagina);
                        respuesta.Add("estado", true);
                        respuesta.Add("subMenusFaltantes", submenusFaltandes);
                        respuesta.Add("subMenusActuales", submenuActuales);
                    }
                    return Json(respuesta);
                }
                [HttpPost]
                public ActionResult getJSONRolesFaltantes()
                {
                    Dictionary<Object, Object> frm,respuesta;
                    Usuario usuario = this.getUsuarioSesion();
                    frm = this.getAjaxFrm("form");

                    respuesta = this.seguridadInicialAjax(usuario, frm);
                    if (respuesta == null)
                    {
                        respuesta = new Dictionary<Object, Object>();
                        List<Rol> roles = this._model.getRolesFaltantes(Convert.ToInt32(frm["idUsuario"].ToString()), usuario._idUsuario, this._idPagina);
                        respuesta.Add("roles", roles);
                        respuesta.Add("estado", true);
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
                    respuesta = this.seguridadInicialAjax(usuarioSesion, frm);
                    if (respuesta == null)
                    {
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
                    return Json(respuesta);
                }
            #endregion
            #region "acciones"
                #region "eliminar" 
                    [HttpPost]
                    public ActionResult sp_sec_eliminarRol()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = new Dictionary<object, object>();
                            try
                            {
                                bool elimino = this._model.sp_sec_eliminarRol(Convert.ToInt32(frm["txtHdIdRol"].ToString()), usuarioSession._idUsuario, this._idPagina);
                                if (elimino)
                                {
                                    respuesta.Add("estado", elimino);
                                }
                                else
                                {
                                    this.errorTryControlador(3, "Ocurrio un error inesperado");
                                }
                            }
                            catch (ErroresIUS x) {
                                ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                                respuesta = this.errorTryControlador(1, error);
                            }
                            catch (Exception x)
                            {
                                ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                respuesta = this.errorTryControlador(2, x);
                            }
                        }
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult eliminarRolSubmenu()
                    {
                        Dictionary<Object, Object> frm, respuesta = null;
                        GestionRolSubmenuModel control = new GestionRolSubmenuModel(this._idPagina);
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = new Dictionary<Object,Object>();
                            // vars
                            int idRol = Convert.ToInt32(frm["idRol"].ToString());
                            int idSubmenu = Convert.ToInt32(frm["idSubMenu"].ToString());
                            // do it
                            bool elimino = control.eliminarRolSubmenu(idSubmenu, idRol, usuarioSession._idUsuario);
                            List<Submenu> submenuFaltante;
                            if(elimino)
                            {
                                submenuFaltante = this._model.getSubMenuFaltantesRol(idRol, usuarioSession._idUsuario, this._idPagina);
                                respuesta.Add("estado", true);
                                respuesta.Add("submenuFaltante", submenuFaltante);
                            }
                            else
                            {
                                respuesta.Add("estado", false);
                            }
                        }
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult eliminarPermisoSubmenuRol()
                    {
                        Dictionary<Object,Object> frm,respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuariosesion = this.getUsuarioSesion();
                        GestionPermisosModel control = new GestionPermisosModel();

                        respuesta = this.seguridadInicialAjax(usuariosesion, frm);
                        if (respuesta == null)
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
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult desasociarRolUsuario()
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        Dictionary<Object, Object> respuesta, frm;
                        String frmText = Request.Form["form"];
                        Usuario usuarioSession = this.getUsuarioSesion();
                        if (frmText != null)
                        {
                            respuesta = new Dictionary<Object, Object>();
                            frm = jss.Deserialize<Dictionary<Object, Object>>(frmText);
                            Boolean estado = this._model.desasociarRol(Convert.ToInt32(frm["idRol"].ToString()), Convert.ToInt32(frm["idUsuario"].ToString()), usuarioSession._idUsuario,this._idPagina);
                            List<Rol> rolesFaltantes = this._model.getRolesFaltantes(Convert.ToInt32(frm["idUsuario"].ToString()), usuarioSession._idUsuario, this._idPagina);
                            respuesta.Add("estado", estado);
                            respuesta.Add("rolesFaltantes", rolesFaltantes);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                #endregion
                #region "agregar"
                    [HttpPost]
                    public ActionResult agregarRolSubMenu()
                    {
                        Dictionary<Object, Object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            // vars 
                                int[] idSubMenus = this.convertArrAjaxToInt((Object[])frm["idSubMenu"]);
                                int idRol = Convert.ToInt32(frm["idRol"].ToString());
                            // do it 
                                GestionRolSubmenuModel control = new GestionRolSubmenuModel(this._idPagina);
                                bool agrego = control.agregarRolSubMenu(idRol,idSubMenus,usuarioSession._idUsuario);
                                List<Submenu> submenus;
                                List<Submenu> submenuFaltante;
                                respuesta = new Dictionary<Object, Object>();
                                if (agrego)
                                {
                                    
                                    submenus = this._model.getSubmenuRol(idRol, usuarioSession._idUsuario, this._idPagina);
                                    submenuFaltante = this._model.getSubMenuFaltantesRol(idRol, usuarioSession._idUsuario, this._idPagina);
                                    respuesta.Add("estado", true);
                                    respuesta.Add("submenus", submenus);
                                    respuesta.Add("submenuFaltante", submenuFaltante);
                                }
                                else
                                {
                                    respuesta.Add("estado", false);
                                }
                        }
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult agregarRoles()
                    {
                        Dictionary<Object, Object> frm, respuesta;
                        Usuario usuarioSession = this.getUsuarioSesion();
                        bool agrego = false;
                        frm = this.getAjaxFrm();
                        if (frm != null)
                        {
                            respuesta = new Dictionary<Object, Object>();
                            int[] roles = this.convertArrAjaxToInt((Object[])frm["rolesAgregar"]);
                            int idUsuario = Convert.ToInt32(frm["idUsuario"].ToString());
                            agrego = this._model.agregarRoles(roles, idUsuario, usuarioSession._idUsuario, this._idPagina);
                            //List<Rol> roles = this._model.getRolesFaltantes(Convert.ToInt32(frm["idUsuario"].ToString()), usuario._idUsuario, this._idPagina);
                            List<Rol> rolesFaltantes = this._model.getRolesFaltantes(Convert.ToInt32(frm["idUsuario"].ToString()), usuarioSession._idUsuario, this._idPagina);
                            if (agrego)
                            {
                                List<Rol> rolesUsuario = this._model.getRoles(idUsuario);
                                respuesta.Add("estado", true);
                                respuesta.Add("roles", rolesUsuario);
                                respuesta.Add("rolesFaltantes", rolesFaltantes);
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
                    [HttpPost]
                    public ActionResult agregarPermisoSubmenuRol()
                    {
                        Dictionary<Object, Object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            int[] idPermisos = this.convertArrAjaxToInt((Object[])frm["idPermisos"]);
                            GestionPermisosModel controlLocal = new GestionPermisosModel();
                            respuesta = new Dictionary<Object, Object>();
                            // vars 
                                int idRol = Convert.ToInt32(frm["idRol"].ToString()); int idSubMenu = Convert.ToInt32(frm["idSubMenu"].ToString());
                            // do it
                                bool agrego = controlLocal.agregarPermisoSubmenuRol(idRol,idSubMenu,idPermisos, usuarioSession._idUsuario, this._idPagina);
                                List<RolSubMenuPermiso> rolSubMenuPermiso = controlLocal.getPermisosSubmenuRol(idSubMenu, idRol, usuarioSession._idUsuario, this._idPagina);
                                List<PermisoRol> permisosFaltantes = controlLocal.getPermisosSubmenuRolFaltantes(idSubMenu, idRol, usuarioSession._idUsuario, this._idPagina);
                                if (agrego)
                                {
                                    respuesta.Add("estado", true);
                                    respuesta.Add("rolSubMenuPermiso", rolSubMenuPermiso);
                                    respuesta.Add("permisosFaltantes", permisosFaltantes);
                                }
                                else
                                {
                                    respuesta.Add("estado", false);
                                }
                        }
                        return Json(respuesta);
                    }
                    [HttpPost]
                    public ActionResult sp_sec_addRol()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        try
                        {
                            Usuario usuarioSession = this.getUsuarioSesion();
                            frm = this.getAjaxFrm();
                            
                            respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                            if (respuesta == null)
                            {
                                Rol rol, rolAgregar = new Rol(frm["txtRol"].ToString(), true); // Que sentido tendra agregar un rol inactivo D: 
                                respuesta = new Dictionary<object, object>();
                                rol = this._model.sp_sec_addRol(rolAgregar, usuarioSession._idUsuario, this._idPagina);
                                Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSession._idUsuario, this._idPagina);
                                if (rol != null)
                                {
                                    respuesta.Add("estado", true);
                                    respuesta.Add("rol", rol);
                                    respuesta.Add("permisos", permisos);
                                }
                                else
                                {
                                    respuesta.Add("estado", false);
                                    respuesta.Add("errorType", 3);
                                    respuesta.Add("error", "Ocurrio un error inesperado");
                                }
                            }
                            
                        }
                        catch (ErroresIUS x)
                        {
                            /*respuesta.Add("estado", false);
                            respuesta.Add("errorType", 1);
                            respuesta.Add("error", x);*/
                            ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                            respuesta = this.errorTryControlador(1, error);
                        }
                        catch (Exception x)
                        {
                            /*
                            respuesta.Add("estado", false);
                            respuesta.Add("errorType", 2);
                            respuesta.Add("error", x);*/
                            ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                            respuesta = this.errorTryControlador(2, error);
                        }
                        
                        return Json(respuesta);
                    }
                #endregion
                #region "editar"
                    public ActionResult sp_sec_editarRol()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Rol rol,rolAgregar;
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            try{
                                rolAgregar = new Rol(this.convertObjAjaxToInt(frm["txtHdIdRol"]),frm["txtRol"].ToString());
                                rol = this._model.sp_sec_editarRol(rolAgregar, usuarioSession._idUsuario, this._idPagina);
                                if (rol != null)
                                {
                                    respuesta = new Dictionary<object,object>();
                                    respuesta.Add("estado", true);
                                    respuesta.Add("rol", rol);
                                }
                                else
                                {
                                    respuesta = this.errorTryControlador(3, "Error no controlado");
                                }
                            }catch(ErroresIUS x){
                                //respuesta = this.errorTryControlador(1,x);
                                ErroresIUS error = new ErroresIUS(x.Message, x.errorType, x.errorNumber, x._errorSql, x._mostrar);
                                respuesta = this.errorTryControlador(1, error);
                            }
                            catch(Exception x){
                                //respuesta = this.errorTryControlador(2,x);
                                ErroresIUS error = new ErroresIUS(x.Message, ErroresIUS.tipoError.generico, x.HResult);
                                respuesta = this.errorTryControlador(2, error);
                            }
                            
                        }
                        return Json(respuesta);
                    }
                    public ActionResult sp_sec_cambiarEstadoRol()
                    {
                        Dictionary<object, object> frm, respuesta = null;
                        frm = this.getAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        
                        respuesta = this.seguridadInicialAjax(usuarioSession, frm);
                        if (respuesta == null)
                        {
                            respuesta = new Dictionary<object, object>();
                            try
                            {
                                Rol rol = this._model.sp_sec_cambiarEstadoRol(Convert.ToInt32(frm["txtHdIdRol"].ToString()), usuarioSession._idUsuario, this._idPagina);
                                if (rol != null)
                                {
                                    respuesta.Add("estado", true);
                                    respuesta.Add("rol", rol);
                                }
                                else
                                {
                                    respuesta = this.errorTryControlador(3, "Error no controlado");
                                }
                            }
                            catch (ErroresIUS x)
                            {
                                respuesta = this.errorTryControlador(1, x);
                            }
                            catch (Exception x)
                            {
                                respuesta = this.errorTryControlador(2, x);
                            }
                        }
                        return Json(respuesta);
                    }
                #endregion
            #endregion
        #endregion
    }
}
