using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.LOGS;
    using IUSLibs.TRL.Control;
namespace IUSBack.Models.Page.GestionIdiomaWebsite.Acciones
{
    public class GestionIdiomaWebsiteModel:PadreModel
    {
        #region "propiedades"
            
        #endregion
        #region "funciones publicas"
            public List<Idioma> sp_trl_getAllIdiomas(int idUsuarioEjecutor,int idPagina)
            {
                ControlIdioma control = new ControlIdioma();
                List<Idioma> idiomas = null;
                try{
                     idiomas = control.sp_trl_getAllIdiomas(idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return idiomas;
            }
            public List<Pagina> sp_trl_getAllPaginas(int idUsuarioEjecutor,int idPagina)
            {
                ControlPagina control = new ControlPagina();
                List<Pagina> paginas = null ;
                try
                {
                    paginas = control.sp_trl_getAllPaginas(idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return paginas;
            }
            public List<Llave> sp_trl_getLlaveFromPage(int idPaginaFront, int idUsuarioEjecutor, int idPagina)
            {
                List<Llave> llaves =null;
                ControlLlave control = new ControlLlave();
                try
                {
                    llaves = control.sp_trl_getLlaveFromPage(idPaginaFront, idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return llaves;
            }
            public List<LlaveIdioma> sp_trl_tablitaGestionTraduccion(int idUsuarioEjecutor,int idPagina)
            {
                ControlLlaveIdioma control = new ControlLlaveIdioma();
                List<LlaveIdioma> llavesIdioma = null;
                try
                {
                    llavesIdioma = control.sp_trl_tablitaGestionTraduccion(idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return llavesIdioma;
            }
            public bool sp_trl_actualizarLlaveIdioma(int idLlaveIdioma,int idLlave,int idIdioma,string traduccion,int idUsuarioEjecutor,int idPagina)
            {
                bool toReturn = false;
                ControlLlaveIdioma control = new ControlLlaveIdioma();
                try
                {
                    toReturn = control.sp_trl_actualizarLlaveIdioma(idLlaveIdioma,idLlave,idIdioma,traduccion,idUsuarioEjecutor, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return toReturn;
            }
        #endregion 
        #region
            public GestionIdiomaWebsiteModel()
            {

            }
        #endregion 

    }
}
