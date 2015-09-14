﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// Externas
    using IUSLibs.LOGS;
    using IUSLibs.RRHH.Control.Formacion;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class FormacionPersonasModel:PadreModel
    {
        #region "propiedades"
            private ControlFormacionPersona _controlFormacion;
        #endregion
        #region "funciones"
            #region "do"
                
            #endregion
            #region "get"
                public Dictionary<object, object> sp_rrhh_getInfoInicialFormacion(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    try
                    {
                        return this._controlFormacion.sp_rrhh_getInfoInicialFormacion(idPersona, idUsuarioEjecutor, idPagina);
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
            public FormacionPersonasModel()
            {
                this._controlFormacion = new ControlFormacionPersona();
            }
        #endregion
    }
}