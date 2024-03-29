﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    // --------------
        using IUSLibs.RRHH.Entidades;
        using IUSLibs.FrontUI.Entidades;
        using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Control
{
    public class ControlInformacionPersona:PadreLib
    {
        #region "get"
            public Dictionary<object, object> sp_rrhh_getInformacionPersonas(int idPersona, int idUsuarioEjecutor, int idPagina)
            {
                Dictionary<object, object> retorno = null;
                // variables
                List<Pais> paises = null; Pais pais;/**/ List<EstadoCivil> estadosCiviles=null;EstadoCivil estadoCivil;
                InformacionPersona informacionPersona = null; Persona persona = null;
                List<EmailPersona> emails=null; EmailPersona email;
                List<TelefonoPersona> telefonos= null; TelefonoPersona telefono;
                // trayendo
                SPIUS sp = new SPIUS("sp_rrhh_getInformacionPersonas");
                sp.agregarParametro("idPersona", idPersona);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            paises = new List<Pais>();
                            // paises 
                            foreach(DataRow row in tb[0].Rows ){
                                pais = new Pais((int)row["idPais"], row["pais"].ToString());
                                paises.Add(pais);
                            }
                        }
                        if (tb[1].Rows.Count > 0)
                        {
                            // estados civiles 
                            estadosCiviles = new List<EstadoCivil>();
                            foreach (DataRow row in tb[1].Rows) {
                                estadoCivil = new EstadoCivil((int)row["idEstadoCivil"],row["estado_civil"].ToString());
                                estadosCiviles.Add(estadoCivil);
                            }
                        }
                        if (tb[2].Rows.Count > 0)
                        {
                            // Informacion personas 
                            DataRow row = tb[2].Rows[0];
                            informacionPersona = new InformacionPersona((int)row["idInformacionPersona"], (int)row["id_pais_fk"], row["numero_identificacion"].ToString(), (int)row["id_estadocivil_fk"], (int)row["id_persona_fk"], row["foto"].ToString());
                        }
                        if (tb[3].Rows.Count > 0)
                        {
                            // mails 
                            emails = new List<EmailPersona>();
                            foreach (DataRow row in tb[3].Rows)
                            {
                                email               = new EmailPersona((int)row["idMailPersona"], row["email"].ToString(), row["descripcion"].ToString(), (int)row["id_persona_fk"]);
                                email._principal    = (bool)row["principal"];
                                emails.Add(email);
                            }
                        }
                        if (tb[4].Rows.Count > 0)
                        {
                            // telefonos 
                            telefonos = new List<TelefonoPersona>();
                            foreach (DataRow row in tb[4].Rows)
                            {
                                telefono = new TelefonoPersona((int)row["idTelefonoPersona"],row["telefono"].ToString(),row["descripcion"].ToString(),(int)row["id_pais_fk"],(int)row["id_persona_fk"]);
                                telefono._pais._pais = row["pais"].ToString();
                                telefonos.Add(telefono);
                            }
                        }
                        if (tb[5].Rows.Count > 0)
                        {
                            DataRow row = tb[5].Rows[0];
                            persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString());
                        }
                        retorno = new Dictionary<object, object>();
                        retorno.Add("paises", paises);
                        retorno.Add("estadosCiviles", estadosCiviles);
                        retorno.Add("informacionPersona", informacionPersona);
                        retorno.Add("emails", emails);
                        retorno.Add("telefonos", telefonos);
                        retorno.Add("persona", persona);
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
                    }

                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return retorno;
            }
            // la pondremos aqui porq no hay otro lugar para ubicarla
            public List<Persona> sp_rrhh_buscarPersonas(
                    /*string niveles, string areas,
                    string carrera, string rubros,
                    string cargos,  int idUsuarioEjecutor,
                    int idPagina*/
                    Dictionary<object, object> strArrElements, string carrera,
                    int tipoBusqueda,int idUsuarioEjecutor, int idPagina
            )
            {
                List<Persona> personas = null;
                Persona persona;
                SPIUS sp = new SPIUS("sp_rrhh_buscarPersonas");
                string niveles = null; string areas = null; string rubros = null;
                string cargos = null; string estadosCiviles = null; string paises = null;
                if(strArrElements["niveles"] != null){
                    niveles = strArrElements["niveles"].ToString();
                }
                if(strArrElements["areas"] != null){
                    areas = strArrElements["areas"].ToString();
                }
                if (strArrElements["rubros"] != null)
                {
                    rubros = strArrElements["rubros"].ToString();
                }
                if (strArrElements["cargos"] != null)
                {
                    cargos = strArrElements["cargos"].ToString();
                }
                if (strArrElements["estadosCiviles"] != null)
                {
                    estadosCiviles = strArrElements["estadosCiviles"].ToString();
                }
                if (strArrElements["paises"] != null)
                {
                    paises = strArrElements["paises"].ToString();
                }
                
                sp.agregarParametro("niveles", niveles);
                sp.agregarParametro("areas", areas);
                sp.agregarParametro("carrera", carrera);

                sp.agregarParametro("rubros", rubros);
                sp.agregarParametro("cargos", cargos);

                sp.agregarParametro("paises", paises);
                sp.agregarParametro("estadosCiviles", estadosCiviles);

                sp.agregarParametro("tipoBusqueda", tipoBusqueda);

                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            personas = new List<Persona>();
                            foreach (DataRow row in tb[0].Rows)
                            {
                                persona         = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString(), (DateTime)row["fecha_nacimiento"]);
                                persona._sexo   = new Sexo((int)row["id_sexo_fk"]);
                                personas.Add(persona);
                            }
                        }
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return personas;
            }
        #endregion
        #region "do"
            public InformacionPersona sp_rrhh_setCurriculumnPersona(string rutaCurriculumn, int idPersona,int idUsuarioEjecutor,int idPagina)
            {
                InformacionPersona infoPersona = null;
                SPIUS sp = new SPIUS("sp_rrhh_setCurriculumnPersona");
                sp.agregarParametro("curriculumn", rutaCurriculumn);
                sp.agregarParametro("idPersona", idPersona);

                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        DataRow row                 = tb[1].Rows[0];
                        infoPersona                 = new InformacionPersona((int)row["idInformacionPersona"]);
                        infoPersona._curriculumn    = row["curriculumn"].ToString();
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return infoPersona;
            }
            public InformacionPersona sp_rrhh_setFotoInformacionPersona(InformacionPersona info,int idUsuarioEjecutor,int idPagina)
            {
                InformacionPersona infoRegresar = null;
                SPIUS sp = new SPIUS("sp_rrhh_setFotoInformacionPersona");
                
                sp.agregarParametro("foto", info._fotoRuta);
                sp.agregarParametro("idPersona", info._persona._idPersona);

                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            infoRegresar = new InformacionPersona((int)row["idInformacionPersona"], row["foto"].ToString());
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
                    }
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return infoRegresar;
            }
            public InformacionPersona sp_rrhh_guardarInformacionPersona(InformacionPersona infoAgregar,int idUsuarioEjecutor, int idPagina)
            {
                InformacionPersona informacionPersona = null;
                SPIUS sp = new SPIUS("sp_rrhh_guardarInformacionPersona");
                sp.agregarParametro("numeroIdentificacion", infoAgregar._numeroIdentificacion);
                sp.agregarParametro("idPais", infoAgregar._pais._idPais);
                sp.agregarParametro("idEstadoCivil", infoAgregar._estadoCivil._idEstadoCivil);
                sp.agregarParametro("idPersona", infoAgregar._persona._idPersona);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            infoAgregar = new InformacionPersona((int)row["idInformacionPersona"], (int)row["id_pais_fk"], row["numero_identificacion"].ToString(), (int)row["id_estadocivil_fk"], (int)row["id_persona_fk"], row["foto"].ToString());
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
                    }

                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return informacionPersona;
            }
        #endregion
    }
}
