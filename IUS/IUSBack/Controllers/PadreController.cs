using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// manejo de archivos
    using System.IO;
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
        #region "propiedades"
            public enum paginas
            {
                usuarios = 3,gestionRoles = 5,
                gestionPersonas=4,gestionIdiomaWebsite = 7,configuracionFront = 8,
                Eventos = 9, Noticias = 10, Repositorio = 11, Instituciones = 12
            }
            protected JavaScriptSerializer _jss;
            protected JavaScriptSerializer _jssmax;
            private PadreModel _model;
            public Dictionary<string, string> _RUTASGLOBALES;
            public GestionFileServerModel gestionArchivosServer;
        #endregion
        #region "funciones"
            #region "iniciales"
                public ActionResult seguridadInicial(int idPagina)
                {
                    Usuario usuarioSesion = this.getUsuarioSesion();
                    ActionResult retorno = null;
                    if (usuarioSesion != null)
                    {
                        Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSesion._idUsuario, idPagina);
                        if ( !(permisos != null && permisos._ver) )
                        {
                            retorno = RedirectToAction("NotAllowed", "Errors");
                        }
                    }
                    else
                    {
                        return RedirectToAction("index", "login");
                    }
                    return retorno;
                }
                public Dictionary<string,string> setRutasGlobales()
                {
                    Dictionary<string, string> rutas = new Dictionary<string, string>();
                    //this._RUTASGLOBALES.Add("REPOSITORIO_DIGITAL", "~/RepositorioDigital/Usuarios/");
                    rutas.Add("REPOSITORIO_DIGITAL", "~/RepositorioDigital/Usuarios/");
                    return rutas;
                }
            #endregion
            #region "manejo de archivos"
            /*public string getPath(string path,string fileName){
                    string retorno = "";
                    try
                    {
                        path = Server.MapPath(path);
                        //retorno = Path.Combine(, fileName);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        retorno = Path.Combine(path, fileName);
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return retorno;
                }*/
                public Byte[] getBytesFromFile(HttpPostedFileBase archivo)
                {
                    Stream fileStream = archivo.InputStream;
                    var mStreamer = new MemoryStream();
                    mStreamer.SetLength(fileStream.Length);
                    fileStream.Read(mStreamer.GetBuffer(), 0, (int)fileStream.Length);
                    mStreamer.Seek(0, SeekOrigin.Begin);
                    byte[] fileBytes = mStreamer.GetBuffer();
                    return fileBytes;
                }
                public List<HttpPostedFileBase> getBaseFileFromRequest(HttpRequestBase request)
                {
                    List<HttpPostedFileBase> archivos = new List<HttpPostedFileBase>();
                    HttpPostedFileBase archivo;
                    foreach (string file in request.Files)
                    {
                        var fileContent = Request.Files[file];
                        archivo = fileContent;
                        archivos.Add(archivo);
                    }
                    return archivos;
                }
            #endregion 
            #region "manejo de errores"
                public Dictionary<Object, Object> errorTryControlador(int errorType,object obj)
                {
                    Dictionary<Object, Object> retorno = new Dictionary<object, object>();
                    retorno.Add("estado", false);
                    retorno.Add("errorType", errorType);
                    retorno.Add("error", obj);
                    return retorno;
                }
                public Dictionary<Object,Object> errorEnvioFrmJSON(){
                    Dictionary<Object,Object> toReturn = new Dictionary<Object, Object>();
                    toReturn.Add("estado", false);
                    toReturn.Add("errorType",4);
                    toReturn.Add("error", "Formulario no se envio correctamente");
                    return toReturn;
                }
            #endregion
            public Usuario getUsuarioSesion()
            {
                Usuario usuario = null;
                if (Session["usuario"] != null)
                {
                    usuario = (Usuario)Session["usuario"];
                }
                return usuario;
            }
            #region "getAjaxfrm"
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
            [ValidateInput(false)]
            public Dictionary<Object, Object> getAjaxFrmWithOutValidate()
            {
                Dictionary<Object, Object> toReturn = null;
                String frmText = "";
                try
                {
                    frmText = Request.Unvalidated.Form["form"];
                }
                catch (HttpException)
                {

                }
                catch (Exception)
                {

                }
                if (frmText != null || frmText != "")
                {
                    try
                    {
                        toReturn = this._jssmax.Deserialize<Dictionary<Object, Object>>(frmText);
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                return toReturn;
            }
            public List<Dictionary<Object, Object>> getListAjaxFrm()
            {
                return this.getListAjaxFrm("form");
            }
            public List<Dictionary<Object, Object>> getListAjaxFrm(String txtObj)
            {
                List<Dictionary<Object, Object>> toReturn = null;
                String frmText = Request.Form[txtObj];
                if (frmText != null || frmText != "")
                {
                    try
                    {
                        toReturn = this._jss.Deserialize<List<Dictionary<Object, Object>>>(frmText);
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                return toReturn;
            }
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
                        string fechaCompleto = date+" "+hora;
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
        #region "contructores"
            public PadreController()
            {
                this._jss = new JavaScriptSerializer();
                this._jssmax = new JavaScriptSerializer();
                this._jssmax.MaxJsonLength = Int32.MaxValue;
                this._model = new PadreModel();
                this._RUTASGLOBALES = this.setRutasGlobales();
                this.gestionArchivosServer = new GestionFileServerModel();
            }
        #endregion
        
    }
}
