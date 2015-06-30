﻿using System;
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
    public class MediosInstitucionesModel:PadreModel
    {
        #region "propiedades"
            private ControlEnlaceInstitucion _controlEnlace;    
        #endregion
        #region "funciones"
            #region "get"
                
            #endregion
            #region "set"
                public EnlaceInstitucion sp_frontui_insertEnlaceInstituciones(EnlaceInstitucion enlaceAgregar, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlEnlace.sp_frontui_insertEnlaceInstituciones(enlaceAgregar, idUsuarioEjecutor, idPagina);
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
        #region "contructores"
            public MediosInstitucionesModel()
            {
                this._controlEnlace = new ControlEnlaceInstitucion();

            }
        #endregion
    }
}