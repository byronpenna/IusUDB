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
            private int _idPagina = 3;
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
                        catch (ErroresIUS x)
                        {
                            objPermiso = null;
                            usuarios = null;
                        }
                        catch (Exception x)
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
        #region "Resultados ajax"
        [HttpPost]
        public ActionResult cambiarEstadoUsuario()
        {
            Usuario usuarioSession = (Usuario)Session["usuario"];
            int idUsuario = Convert.ToInt32(Request.Form["usuarioId"]);
            var resp = (Dictionary<Object,Object>)this._model.cambiarEstadoUsuario(idUsuario,usuarioSession._idUsuario);
            return Json(resp);
        }
        [HttpPost]
        public ActionResult actualizarUsuario()
        {
            Usuario usuarioSession = (Usuario)Session["usuario"];
            var jss     = new JavaScriptSerializer();
            Dictionary<Object,Object> frm,toReturn;
            String frmText = Request.Form["form"];
            if (frmText != null)
            {
                frm = jss.Deserialize<Dictionary<Object, Object>>(frmText);

                toReturn = this._model.actualizarUsuario(frm,usuarioSession._idUsuario);
            }
            else
            {
                toReturn = this.errorEnvioFrmJSON();
            }
            return Json(toReturn);
        }
        #endregion

    }
}

