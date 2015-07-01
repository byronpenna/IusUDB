using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.FrontUI.Control;
namespace IUSBack.Models.Page.GestionInstituciones.Acciones
{
    public class GestionInstitucionesModel:PadreModel
    {
        #region "propiedades"
            private ControlPais         _controlPais;
            private ControlInstitucion  _controlInstitucion;
        #endregion
        #region "funciones"
            #region "get"
                public List<Pais> sp_frontui_getPaises()
                {
                    try
                    {
                        return this._controlPais.sp_frontui_getPaises();
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
                public Institucion sp_frontui_getInstitucionById(int idInstitucion, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_getInstitucionById(idInstitucion, idUsuarioEjecutor, idPagina);
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
                public Dictionary<object, object> cargaInicialIndex(int idUsuarioEjecutor,int idPagina)
                {
                    
                    try
                    {
                        List<Pais> paises = this._controlPais.sp_frontui_getPaises();
                        List<Institucion> instituciones = this._controlInstitucion.sp_frontui_getInstituciones(idUsuarioEjecutor, idPagina);

                        Dictionary<object, object> retorno = new Dictionary<object,object>();
                        retorno.Add("instituciones", instituciones);
                        retorno.Add("paises", paises);
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
            #region "acciones"
                public Institucion sp_frontui_editInstitucion(Institucion institucionEditar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_editInstitucion(institucionEditar, idUsuarioEjecutor, idPagina);
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
                public bool sp_frontui_setLogoInstitucion(Institucion institucionActualizar,int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_setLogoInstitucion(institucionActualizar, idUsuarioEjecutor, idPagina);
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
                public Institucion sp_frontui_insertInstitucion(Institucion institucionAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_insertInstitucion(institucionAgregar, idUsuarioEjecutor, idPagina);
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
                public bool sp_frontui_deleteInstitucion(int idInstitucion, int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        return this._controlInstitucion.sp_frontui_deleteInstitucion(idInstitucion, idUsuarioEjecutor, idPagina);
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
        #endregion
        #region "constructores"
            public GestionInstitucionesModel()
            {
                this._controlPais           = new ControlPais();
                this._controlInstitucion    = new ControlInstitucion();
            }
        #endregion
    }
}