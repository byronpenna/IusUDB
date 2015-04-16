using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// json 
using System.Web.Script.Serialization;
// librerias internas
    using IUSBack.Models.Page.GestionUsuarios.Acciones;
// librerias externas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Controllers
{
    public class GestionUsuariosController : PadreController
    {
        //
        // GET: /GestionUsuarios/
        #region "Propiedades"
            private GestionUsuarioModel _model;
            private int _idPagina = (int)paginas.usuarios;
        #endregion
        #region "constructores"
            public GestionUsuariosController()
            {
                this._model = new GestionUsuarioModel(this._idPagina);
            }
        #endregion
        #region "Resultados url"
            public ActionResult Index()
            {
                if (Session["usuario"] != null)
                {
                    List<Usuario> usuarios;
                    Usuario usuarioSession = (Usuario)Session["usuario"];
                    ViewBag.subMenus = this._model.getMenuUsuario(usuarioSession._idUsuario);
                    bool permiso = this._model.tienePermiso(usuarioSession._idUsuario,this._idPagina,Models.General.PadreModel.permisos.Ver);
                    if (permiso)
                    {
                        Permiso objPermiso;
                        try
                        {
                            usuarios = this._model.getUsuarios(usuarioSession._idUsuario);
                            objPermiso = this._model.permisoGestion;

                        }
                        catch (ErroresIUS)
                        {
                            objPermiso = null;
                            usuarios = null;
                        }
                        catch (Exception)
                        {
                            usuarios = null;
                            objPermiso = null;
                        }
                        ViewBag.permiso = objPermiso;
                        ViewBag.usuarios = usuarios;
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
        #region "funciones privadas"
            private Usuario getUsuarioFromForm(Dictionary<Object,Object> frm)
            {
                Persona persona = new Persona(Convert.ToInt32((string)frm["cbPersona"]));
                Usuario usu = new Usuario(Convert.ToInt32((String)frm["txtHdIdUser"]), frm["txtEditUsuario"].ToString(), persona);
                return usu;
            }
            public List<Usuario> getUsuarioFromForm(List<Dictionary<Object,Object>> frms)
            {
                List<Usuario> usuarios = new List<Usuario>();
                Usuario usu;
                foreach (Dictionary<Object, Object> frm in frms)
                {
                    usu = this.getUsuarioFromForm(frm);
                    usuarios.Add(usu);
                }
                return usuarios;
            }
        #endregion
        #region "Resultados ajax"
            #region "acciones"
                [HttpPost]
                public ActionResult cambiarEstadoUsuario()
                {
                    Usuario usuarioSession = (Usuario)Session["usuario"];
                    int idUsuario = Convert.ToInt32(Request.Form["usuarioId"]);
                    var resp = (Dictionary<Object,Object>)this._model.cambiarEstadoUsuario(idUsuario,usuarioSession._idUsuario);
                    return Json(resp);
                }
                #region "actualizar"
                    [HttpPost]
                    public ActionResult actualizarUsuario()
                    {
                        Usuario usuarioSession = this.getUsuarioSesion();
                        Dictionary<Object,Object> frm,toReturn;
                        frm = this.getAjaxFrm();
                        if (frm != null && usuarioSession != null)
                        {   
                            toReturn = this._model.actualizarUsuario(frm,usuarioSession._idUsuario);
                        }
                        else
                        {
                            toReturn = this.errorEnvioFrmJSON();
                        }
                        return Json(toReturn);
                    }
                    [HttpPost]
                    public ActionResult sp_sec_actualizarUsuariosGeneral()
                    {
                        List<Dictionary<Object, Object>> frm;
                        Dictionary<Object, Object> respuesta = null;
                        frm = this.getListAjaxFrm();
                        Usuario usuarioSession = this.getUsuarioSesion();
                        if (usuarioSession != null && frm != null) // manejor error usuario D: 
                        {
                            List<Usuario> usuariosActualizar = this.getUsuarioFromForm(frm);
                            respuesta = this._model.actualizarUsuario(usuariosActualizar, usuarioSession._idUsuario,this._idPagina);
                        }
                        else
                        {
                            respuesta = this.errorEnvioFrmJSON();
                        }
                        return Json(respuesta);
                    }
                #endregion
            #endregion
        #endregion

    }
}

