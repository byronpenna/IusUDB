using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas 
    using IUS.Models.general;
// externas 
    using IUSLibs.LOGS;
    // repositorio
    using IUSLibs.REPO.Control;
    using IUSLibs.REPO.Entidades;
    using IUSLibs.REPO.Control.Publico;
    using IUSLibs.REPO.Entidades.Publico;
namespace IUS.Models.page.Repositorio.Acciones
{
    public class RepositorioModel:ModeloPadre
    {
        #region "propiedades"
            private ControlCarpetaPublica _controlCarpetaPublica;
            private ControlArchivoPublico _controlArchivoPublico;
        #endregion
        #region "funciones"
                #region "get"
                    public List<ArchivoPublico> sp_repo_front_getAllFilesByType(int idTipo, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlArchivoPublico.sp_repo_front_getAllFilesByType(idTipo, ip, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    // antiguos 
                    public List<ArchivoPublico> sp_repo_searchArchivoPublico(int idTipoArchivo,string nombreBuscar,string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlArchivoPublico.sp_repo_searchArchivoPublico(idTipoArchivo, nombreBuscar, ip, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    public CarpetaPublica sp_repo_front_getCarpetaPublicaByRuta(string ruta,string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlCarpetaPublica.sp_repo_front_getCarpetaPublicaByRuta(ruta, ip, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    public Archivo sp_repo_front_getDownloadFilePublic(int idArchivoPublico, string ip, int idPagina)
                    {
                        try
                        {
                            return this._controlArchivoPublico.sp_repo_front_getDownloadFilePublic(idArchivoPublico, ip, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    public List<TipoArchivo> sp_repo_front_getTiposArchivos(string lang,string ip, int idPagina)
                    {
                        try
                        {
                            ControlTipoArchivo control = new ControlTipoArchivo();
                            lang = this.getStandarLang(lang);
                            return control.sp_repo_front_getTiposArchivos(lang,ip,idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    public Dictionary<object, object> sp_repo_front_getArchivosPublicosByType(int idCarpeta, int idTipoArchivo, string ip, int idPagina)
                    {
                        try
                        {
                            Dictionary<object, object> retorno = new Dictionary<object, object>();
                            retorno.Add("carpetas", this._controlCarpetaPublica.sp_repo_front_GetAllCarpetasPublica(idCarpeta, ip, idPagina));
                            retorno.Add("archivos", this._controlArchivoPublico.sp_repo_front_getArchivosPublicosByType(idCarpeta, idTipoArchivo, ip, idPagina));
                            retorno.Add("carpetaPadre", this._controlCarpetaPublica.sp_repo_front_getCarpetaPublicaFromId(idCarpeta, ip, idPagina));
                            return retorno;
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                    public Dictionary<object,object> sp_repo_front_GetAllCarpetasPublica(int idCarpetaPadre, string ip, int idPagina)
                    {
                        try
                        {
                            Dictionary<object,object> retorno = new Dictionary<object,object>();
                            retorno.Add("carpetas",this._controlCarpetaPublica.sp_repo_front_GetAllCarpetasPublica(idCarpetaPadre, ip, idPagina));
                            retorno.Add("archivos",this._controlArchivoPublico.sp_repo_front_getAllArchivosPublicos(idCarpetaPadre,ip,idPagina));
                            retorno.Add("carpetaPadre", this._controlCarpetaPublica.sp_repo_front_getCarpetaPublicaFromId(idCarpetaPadre, ip, idPagina));
                            return retorno;
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                #endregion
                #region "set"
                #endregion
            #endregion
        #region "constructores"
            public RepositorioModel()
            {
                this._controlCarpetaPublica = new ControlCarpetaPublica();
                this._controlArchivoPublico = new ControlArchivoPublico();
            }
        #endregion
    }
}