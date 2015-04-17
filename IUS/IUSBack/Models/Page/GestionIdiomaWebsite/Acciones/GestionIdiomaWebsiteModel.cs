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
            #region "gets
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
                public List<Llave> sp_trl_getLlaveFromLlaveIdioma(int idLlaveIdioma, int idUsuarioEjecutor, int idPagina)
                {
                    List<Llave> llaves = null;
                    ControlLlave control = new ControlLlave();
                    try
                    {
                        llaves = control.sp_trl_getLlaveFromLlaveIdioma(idLlaveIdioma, idUsuarioEjecutor, idPagina);
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
                public List<Llave> sp_trl_getLlaveFromPageAndIdioma(int idPaginaFront, int idIdioma, int idUsuarioEjecutor, int idPagina)
                {
                    List<Llave> llaves =null;
                    ControlLlave control = new ControlLlave();
                    try
                    {
                        llaves = control.sp_trl_getLlaveFromPageAndIdioma(idPaginaFront,idIdioma, idUsuarioEjecutor, idPagina);
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
            #endregion
            #region "acciones"
                public LlaveIdioma sp_trl_agregarLlaveIdioma(LlaveIdioma llaveIdioma,int idUsuarioEjecutor,int idPagina)
                {
                    LlaveIdioma llaveIdiomaActualizada = null;
                    ControlLlaveIdioma control = new ControlLlaveIdioma();
                    try
                    {
                        llaveIdiomaActualizada = control.sp_trl_agregarLlaveIdioma(llaveIdioma, idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return llaveIdiomaActualizada;
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
                public bool sp_trl_eliminarLlaveIdioma(int idLlaveIdioma, int idUsuario, int idPagina)
                {
                    bool toReturn = false;
                    ControlLlaveIdioma control = new ControlLlaveIdioma();
                    try
                    {
                        toReturn = control.sp_trl_eliminarLlaveIdioma(idLlaveIdioma, idUsuario, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }catch(Exception x)
                    {
                        throw x;
                    }
                    return toReturn;
                }
            #endregion
        #endregion
        #region "constructores"
            public GestionIdiomaWebsiteModel()
            {

            }
        #endregion 

    }
}
