﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// 
    using System.IO;
    using CrystalDecisions.CrystalReports.Engine;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
using CrystalDecisions.Shared;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class GestionPersonaModel:PadreModel
    {
        
        #region "propiedades"
            private ControlPersona _control;
        #endregion 
        #region "gets"
            public List<Persona> getPersonas()
            {
                List<Persona> personas = this._control.getPersonas();
                if (personas.Count != 0)
                {
                    return personas;
                }
                else
                {
                    return null;
                }
            }
            public Dictionary<object, object> sp_rrhh_getInformacionPersonas(int idPersona, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    IUSLibs.RRHH.Control.ControlInformacionPersona informacionPersona = new IUSLibs.RRHH.Control.ControlInformacionPersona();
                    Dictionary<object,object> varInformacionPersona = informacionPersona.sp_rrhh_getInformacionPersonas(idPersona,idUsuarioEjecutor,idPagina);
                    varInformacionPersona.Add("personas", this._control.getPersonas());
                    return varInformacionPersona;
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
            public Dictionary<object, object> sp_rrhh_getMediosPersonas(int idPersona, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    return this._control.sp_rrhh_getMediosPersonas(idPersona, idUsuarioEjecutor, idPagina);
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
            public Dictionary<object, object> sp_rrhh_detallePesona(int idPersona,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._control.sp_rrhh_detallePesona(idPersona, idUsuarioEjecutor, idPagina);
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
            public Stream getFichaStream(string ruta,int idPersona)
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(ruta);
                DataSet1 ds = new DataSet1();
                rd.SetDatabaseLogon("sa", "123456", "PROGRAMADOR", "ius");
                rd.SetParameterValue("idPersona", idPersona);
                Stream retorno = null;
                try
                {
                    retorno = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    retorno.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception x)
                {
                    throw x;
                }
                return retorno;
            }
            private ExportFormatType getFormatReport(int numFormato)
            {
                ExportFormatType retorno;
                switch (numFormato)
                {
                    case 1:
                        {
                            // pdf 
                            retorno = ExportFormatType.PortableDocFormat;
                            break;
                        }
                    case 2:
                        {
                            // word 
                            retorno = ExportFormatType.WordForWindows;
                            break;
                        }
                    case 3:{
                        retorno = ExportFormatType.Excel;
                        break;
                    }
                    default:{
                        retorno = ExportFormatType.PortableDocFormat;
                        break;
                    }
                }
                return retorno;
            }
            public void getFicha(string ruta, int idPersona,HttpResponse response,int numFormato)
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(ruta);
                DataSet1 ds = new DataSet1();
                rd.SetDatabaseLogon("sa", "123456", "PROGRAMADOR", "ius");
                rd.SetParameterValue("idPersona", idPersona);
                try
                {
                    ExportFormatType format = this.getFormatReport(numFormato);
                    rd.ExportToHttpResponse(format,response  , true, "Ficha");
                }
                catch (Exception x)
                {
                    throw x;
                }
                //return rd;
            }
        #endregion
        #region "Acciones"
            public bool sp_hm_eliminarPersona(int idPersona,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._control.sp_hm_eliminarPersona(idPersona,idUsuarioEjecutor,idPagina);
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
            public Dictionary<object,object> sp_hm_agregarPersona(Persona persona, int idUsuarioEjecutor,int idPagina)
            {
                Dictionary<object, object> retorno = new Dictionary<object, object>();
                try
                {
                    Persona personaAgregada;
                    Permiso permisos = this.sp_trl_getAllPermisoPagina(idUsuarioEjecutor, idPagina);
                    personaAgregada = this._control.sp_hm_agregarPersona(persona, idUsuarioEjecutor, idPagina);
                    retorno.Add("persona", personaAgregada);
                    retorno.Add("permisos", permisos);
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
            #region "actualizarPersona"
                public Persona actualizarPersona(Persona persona,int idUsuario,int idPagina)
                {
                    try
                    {
                        return this._control.actualizarPersona(persona, idUsuario, idPagina);
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
                public Dictionary<Object, Object> actualizarPersona(List<Persona> personas, int idUsuarioEjecutor, int idPagina)
                {
                    Dictionary<object, object> respuesta = new Dictionary<object,object>();
                    List<Persona> personasActualizadas = new List<Persona>();
                    bool estadoIndividual = true; bool estadoUniversal;
                    Persona perso;
                    foreach (Persona persona in personas)
                    {
                        try
                        {
                            perso = this._control.actualizarPersona(persona, idUsuarioEjecutor, idPagina);
                            if (perso == null)
                            {
                                estadoIndividual = false;
                            }
                            personasActualizadas.Add(perso);
                        }
                        catch (ErroresIUS)
                        {
                            estadoIndividual = false;
                        }
                        catch (Exception)
                        {
                            estadoIndividual = false;
                        }
                    }
                    if (personasActualizadas.Count == 0)
                    {
                        estadoUniversal = false;
                    }
                    else
                    {
                        estadoUniversal = true;
                    }
                    respuesta.Add("estado", estadoUniversal);
                    respuesta.Add("estadoIndividual", estadoIndividual);
                    respuesta.Add("personas", personasActualizadas);
                    return respuesta;
                }
            #endregion
        #endregion
        #region "contructores"
            public GestionPersonaModel()
            {
                this._control = new ControlPersona();
            }
        #endregion
    }
}