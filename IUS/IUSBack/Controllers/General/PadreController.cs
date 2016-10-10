using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// manejo de archivos
    using System.IO;
    using System.Drawing;
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
                Eventos = 9, Noticias = 10, Repositorio = 11, Instituciones = 12,
                RepositorioPublico = 13, Home = 14, formacionAcademica=17,
                RecursosHumanos = 18,forgetPass=20,
                ConfigRepo = 21
            }
            #region "espacio de configuracion"
                public string URL_IUS = IUSLibs.GENERALS.Rutas.IUS; //"http://localhost:7196/";
                //public string URL_IUS = "http://168.243.3.62/ius/";
                //public string URL_IUS = "http://admacad.udb.edu.sv/IUS/";
                public string IMG_GENERALES = "~/Content/themes/iusback_theme/img/general/";
            #endregion
            protected JavaScriptSerializer _jss;
            protected JavaScriptSerializer _jssmax;
            private PadreModel _model;
            public Dictionary<string, string> _RUTASGLOBALES;
            public GestionFileServerModel gestionArchivosServer;
            
        #endregion
        #region "funciones"
            public int CustomIndexOf(string source, char toFind, int position)
            {
                int index = -1;
                for (int i = 0; i < position; i++)
                {
                    index = source.IndexOf(toFind, index + 1);

                    if (index == -1)
                        break;
                }

                return index;
            }
            #region "iniciales"
                public ActionResult seguridadInicial(int idPagina,int selectedMenu=-1)
                {
                    Usuario usuarioSesion = this.getUsuarioSesion();
                    ActionResult retorno = null;
                    if (usuarioSesion != null)
                    {

                        if (idPagina != -1)
                        {
                            Permiso permisos = this._model.sp_trl_getAllPermisoPagina(usuarioSesion._idUsuario, idPagina);
                            
                            if (permisos != null && permisos._ver)
                            {
                                
                                ViewBag.permiso = permisos;   
                            }
                            else
                            {
                                retorno = RedirectToAction("NotAllowed", "Errors");
                            }
                        }
                        string atras = Session["backControl"].ToString(); 
                        string enmedio = Session["neutroControl"].ToString();
                        string adelante = Session["fowardControl"].ToString();
                        if (!(bool)Session["flagNav"])
                        {
                            
                            Session["backControl"] = Session["neutroControl"];
                            if (Request.Url.AbsoluteUri != Session["neutroControl"].ToString())
                            {
                                Session["neutroControl"] = Request.Url.AbsoluteUri;
                            }
                        }
                        else
                        {
                            Session["flagNav"] = false;
                        }
                        //ViewBag.menus = this._model.sp_sec_getMenu(usuarioSesion._idUsuario);
                        IUSLibs.RRHH.Entidades.InformacionPersona informacionPersona =this._model.detalleLogin(usuarioSesion._idUsuario);
                        if (informacionPersona._fotoRuta != null && informacionPersona._fotoRuta != "")
                        {
                            informacionPersona._fotoRuta = this.getRelativePathFromAbsolute(informacionPersona._fotoRuta);
                            informacionPersona._tieneFoto = true;
                        }
                        informacionPersona._persona = usuarioSesion._persona;
                        ViewBag.informacionPersonaGlobal = informacionPersona;
                        ViewBag.selectedMenu = selectedMenu;
                        ViewBag.currentUrl = Request.Url.AbsoluteUri;
                        ViewBag.cnEventos = this._model.sp_adminfe_countTodayEvents(idPagina, usuarioSesion._idUsuario);
                    }
                    else
                    {
                        return Redirect(IUSLibs.GENERALS.Rutas.IUS + "Login/Index");
                    }
                    return retorno;
                }
                public Dictionary<object, object> seguridadInicialAjax(Dictionary<object, object> frm)
                {
                    Dictionary<object, object> retorno = null;
                    if (frm == null)
                    {
                        retorno = this.errorEnvioFrmJSON();
                    }
                    return retorno;
                }
                public Dictionary<object, object> seguridadInicialAjax(Usuario usuarioSession,Dictionary<object,object> frm)
                {
                    Dictionary<object, object> retorno = null;
                    if (usuarioSession == null)
                    {
                        retorno = new Dictionary<object, object>();
                        retorno.Add("estado", false);
                        retorno.Add("errorType", 0);
                        //retorno.Add("")
                    }
                    else if(frm == null)
                    {
                        retorno = this.errorEnvioFrmJSON();
                    }
                    return retorno;
                }
                public Dictionary<string,string> setRutasGlobales()
                {
                    Dictionary<string, string> rutas = new Dictionary<string, string>();
                    //this._RUTASGLOBALES.Add("REPOSITORIO_DIGITAL", "~/RepositorioDigital/Usuarios/");
                    rutas.Add("REPOSITORIO_DIGITAL", "~/RepositorioDigital/Usuarios/");
                    rutas.Add("LOGOS_INSTITUCIONES", "~/Content/Views/Instituciones/Logos/");
                    rutas.Add("FOTOS_PERSONAL", "~/Recursos/Personal/");
                    rutas.Add("IMAGEN_EVENTO", "~/Recursos/Eventos/Imagen/");
                    rutas.Add("IUS_URL", URL_IUS);
                    return rutas;
                }
            #endregion
            #region "manejo de archivos"
                public Byte[] getBytesRecortadosFromFile(HttpPostedFileBase file,Coordenadas coordenadas,bool isCuadrada=true)
                {
                    MemoryStream memory = new MemoryStream();
                    byte[] fileBytes = this.getBytesFromFile(file);
                    using (Image image = Image.FromStream(file.InputStream))
                    {
                        if (image.Width != image.Height || (coordenadas._ancho > 0 && coordenadas._alto > 0 && coordenadas._ancho > 0))
                        {
                            int x = decimal.ToInt32(image.Width * coordenadas._x);
                            int y = decimal.ToInt32(image.Height * coordenadas._y);

                            int ancho = 0;int alto = 0;
                            if (isCuadrada)
                            {
                                ancho = decimal.ToInt32(image.Height * coordenadas._ancho);
                                alto = ancho;
                            }
                            else
                            {
                                ancho   = decimal.ToInt32(image.Width * coordenadas._ancho);
                                alto    = decimal.ToInt32(image.Height * coordenadas._alto);
                            }
                            
                            Rectangle cropArea = new Rectangle(x, y, ancho,alto);
                            try
                            {
                                using (Bitmap bitMap = new Bitmap(cropArea.Width, cropArea.Height))
                                {
                                    using (Graphics g = Graphics.FromImage(bitMap))
                                    {
                                        g.DrawImage(image, new Rectangle(0, 0, bitMap.Width, bitMap.Height), cropArea, GraphicsUnit.Pixel);
                                    }
                                    bitMap.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                fileBytes = memory.ToArray();
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    return fileBytes;
                }
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
            public string getRelativePathFromAbsolute(string absoultePath)
            {
                string appPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                return absoultePath.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
            }
            public Usuario getUsuarioSesion()
            {
                Usuario usuario = null;
                try
                {
                    if (Session["usuario"] != null)
                    {
                        usuario = (Usuario)Session["usuario"];
                    }
                }
                catch (Exception) { 
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
                    public int[] convertArrAjaxToInt(Object frm)
                    {
                        int[] toReturn = null;
                        int ln;
                        object[] x = null;
                        try
                        {
                            x  = (object[])frm;
                            ln = x.Length;
                        }
                        catch (Exception) {
                            ln = 1;
                        }
                        toReturn = new int[ln];
                        if (x != null)
                        {
                            int cn = 0;
                            foreach (object obj in x)
                            {
                                toReturn[cn] = Convert.ToInt32(obj);
                                cn++;
                            }
                        }
                        else
                        {
                            try
                            {
                                toReturn[0] = this.convertObjAjaxToInt(frm);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        return toReturn;
                    }
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
                    public decimal convertObjAjaxToDecimal(object obj)
                    {
                        return Convert.ToDecimal(obj.ToString());
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
                ViewBag.IMG_GENERALES = this.IMG_GENERALES;
                ViewBag.urlIUS = this.URL_IUS;
            }
        #endregion
        
    }
}
