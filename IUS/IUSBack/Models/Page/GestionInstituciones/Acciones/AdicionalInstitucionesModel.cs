﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    // generales
        using IUSLibs.LOGS;
    // otras
        //using IUSLibs.FrontUI.Entidades;
        using IUSLibs.FrontUI.Control;
        using IUSLibs.FrontUI.Entidades;
        using IUSLibs.RRHH.Control.Formacion;
        using IUSLibs.RRHH.Entidades.Formacion;
        
namespace IUSBack.Models.Page.GestionInstituciones.Acciones
{
    public class AdicionalInstitucionesModel:PadreModel
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
            #region "get"
                public Dictionary<object, object> getInfoInicialAdicionalInstituciones(int idUsuarioEjecutor,int idPagina,int idInstitucion)
                {
                    Dictionary<object, object> retorno = new Dictionary<object, object>();
                    try
                    {
                        ControlNivelesEducaion  controlNivel    = new ControlNivelesEducaion();
                        ControlAreaCarrera      controlArea     = new ControlAreaCarrera();
                        retorno.Add("nivelesEducacion", controlNivel.sp_frontui_getNivelesEducacion(idUsuarioEjecutor, idPagina,idInstitucion));
                        retorno.Add("areasConocimiento", controlArea.sp_frontui_getAreasConoInstituciones(idUsuarioEjecutor, idPagina,idInstitucion));
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
                public List<NivelEducacion> sp_frontui_insertNivelInstituciones(string strNiveles,int idInstitucion,int idUsuarioEjecutor,int idPagina)
                {
                    try
                    {
                        ControlInstitucion controlInstitucion = new ControlInstitucion();
                        return controlInstitucion.sp_frontui_insertNivelInstituciones(strNiveles, idInstitucion, idUsuarioEjecutor, idPagina);
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
                public List<AreaCarrera> sp_frontui_insertAreaConocimientoInstitucion(string strArea, int idInstitucion, int idUsuarioEjecutor, int idPagina)
                {
                    ControlInstitucion controlInstitucion = new ControlInstitucion();
                    try
                    {
                        return controlInstitucion.sp_frontui_insertAreaConocimientoInstitucion(strArea, idInstitucion, idUsuarioEjecutor, idPagina);
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

        #endregion
    }
}