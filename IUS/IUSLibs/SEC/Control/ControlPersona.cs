using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.GENERALS;
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
    // recursos humanos
    using IUSLibs.RRHH.Entidades.Formacion;
    using IUSLibs.RRHH.Entidades.Laboral;
    using IUSLibs.RRHH.Entidades;
    // ----
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.SEC.Control
{
    public class ControlPersona:PadreLib
    {
        #region "funciones"
            #region "acciones"
                
                public bool sp_hm_eliminarPersona(int idPersona,int idUsuarioEjecutor,int idPagina)
                {
                    SPIUS sp = new SPIUS("sp_hm_eliminarPersona");
                    sp.agregarParametro("idPersona",idPersona);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    bool estado = false;
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                public Persona sp_hm_agregarPersona(Persona persona,int idUsuarioEjecutor,int idPagina)
                {
                    Persona personaAgregada = null; Sexo sexo;
                    SPIUS sp = new SPIUS("sp_hm_agregarPersona");
                    sp.agregarParametro("nombres",persona._nombres);
                    sp.agregarParametro("apellidos", persona._apellidos);
                    sp.agregarParametro("fechaNacimiento", persona._fechaNacimiento);
                    sp.agregarParametro("idSexo", persona._sexo._idSexo);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb)) {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow rowPersona = tb[1].Rows[0];
                                personaAgregada = new Persona((int)rowPersona["idPersona"], rowPersona["nombres"].ToString(), rowPersona["apellidos"].ToString(), (DateTime)rowPersona["fecha_nacimiento"]);
                                sexo = new Sexo((int)rowPersona["id_sexo_fk"], rowPersona["sexo"].ToString());
                                personaAgregada._sexo = sexo;
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
                    return personaAgregada;
                }
                public Persona actualizarPersona(Persona persona,int idUsuario,int idPagina)
                {
                    Persona personaReturn = null;
                    SPIUS sp = new SPIUS("sp_hm_editarPersona");
                    // para actualizar
                        sp.agregarParametro("nombres", persona._nombres);
                        sp.agregarParametro("apellidos", persona._apellidos);
                        sp.agregarParametro("fecha", persona._fechaNacimiento);
                        sp.agregarParametro("idPersona", persona._idPersona);
                        sp.agregarParametro("idSexo", persona._sexo._idSexo);
                    // para permisos
                        sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrecto(tb))
                            {
                                if (tb[1].Rows.Count > 0)
                                {
                                    DataRow rowResult = tb[1].Rows[0];
                                    personaReturn = new Persona((int)rowResult["idPersona"], rowResult["nombres"].ToString(), rowResult["apellidos"].ToString(), (DateTime)rowResult["fecha_nacimiento"]);
                                    Sexo sexo = new Sexo((int)rowResult["id_sexo_fk"], rowResult["sexo"].ToString());
                                    personaReturn._sexo = sexo;
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("Error desconocido");
                                    throw x;
                                }
                            }
                            else
                            {
                                DataRow rowResult = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(rowResult);
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
                    return personaReturn;
                }
            #endregion
            #region "get"
                public List<Persona> getPersonas(int idUsuarioEjecutor)
                {
                    List<Persona> personas = null;
                    Persona persona; Sexo sexo;
                    SPIUS sp = new SPIUS("sp_sec_getPersonas");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    try
                    {
                        DataSet ds = sp.EjecutarProcedimiento();
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                personas = new List<Persona>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString(), (DateTime)row["fecha_nacimiento"]);
                                    sexo = new Sexo((int)row["id_sexo_fk"], row["sexo"].ToString());
                                    persona._sexo = sexo;
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
                    /*
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        DataTable table = ds.Tables[0];
                        foreach (DataRow row in table.Rows)
                        {
                            persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString(), (DateTime)row["fecha_nacimiento"]);
                            sexo = new Sexo((int)row["id_sexo_fk"], row["sexo"].ToString());
                            persona._sexo = sexo;
                            personas.Add(persona);
                        }
                    }*/
                    return personas;
                }
                // recursos humanos 
                public List<Persona> sp_rrhh_controlPersona_getPersonasByInstitucion(int idInstitucion,string idioma,string ip,int idPagina)
                {
                    List<Persona> personas = new List<Persona>();
                    Persona persona = null;
                    SPIUS sp = new SPIUS("sp_rrhh_controlPersona_getPersonasByInstitucion");
                    
                    sp.agregarParametro("idInstitucion", idInstitucion);
                    sp.agregarParametro("idioma", idioma);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);

                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                personas = new List<Persona>();
                                foreach(DataRow row in tb[0].Rows){
                                    persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString());
                                    persona._fechaNacimiento = (DateTime)row["fecha_nacimiento"];
                                    persona._sexo = new Sexo((int)row["id_sexo_fk"]);
                                    personas.Add(persona);
                                }
                                
                            }
                        }
                        return personas;

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
                    // variables 
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                        List<FormacionPersona> formaciones=null; List<LaboralPersona> laborales=null;
                        FormacionPersona formacion; LaboralPersona laboral; Persona persona = null;
                        InformacionPersona infoPersona = null;
                    // do it 
                        SPIUS sp = new SPIUS("sp_rrhh_detallePesona");
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
                                    //Personas
                                    DataRow row = tb[0].Rows[0];
                                    persona         = new Persona((int)row["idPersona"],row["nombres"].ToString(),row["apellidos"].ToString(),(DateTime)row["fecha_nacimiento"]);
                                    persona._sexo   = new Sexo((int)row["id_sexo_fk"], row["sexo"].ToString());
                                    
                                }
                                if (tb[1].Rows.Count > 0)
                                {
                                    //Formacion
                                    formaciones = new List<FormacionPersona>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {
                                        formacion = new FormacionPersona((int)row["idFormacionPersona"], (int)row["id_persona_fk"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_area_fk"], row["institucion"].ToString(), (int)row["id_paisinstitucion_fk"]);
                                        formacion._nivelTitulo._nombreNivel = row["nombre_nivel"].ToString();
                                        formacion._paisInstitucion._pais = row["pais"].ToString();
                                        formaciones.Add(formacion);
                                    }
                                }
                                if (tb[2].Rows.Count > 0)
                                {
                                    //Laboral
                                    laborales = new List<LaboralPersona>();
                                    foreach (DataRow row in tb[2].Rows)
                                    {
                                        laboral = new LaboralPersona((int)row["idLaboralPersona"]);
                                        //, , (int)row["id_persona_fk"],  row["cargo"].ToString()
                                        laboral._inicio = (int)row["inicio"];
                                        laboral._fin = (int)row["fin"];

                                        laboral._cargo = row["cargo"].ToString();
                                        //laboral._empresa._nombre = row["empresa"].ToString();
                                        laborales.Add(laboral);
                                    }
                                }
                                if (tb[3].Rows.Count > 0)
                                {
                                    DataRow row = tb[3].Rows[0];
                                    // tomar en cuenta los nulos
                                    infoPersona = new InformacionPersona((int)row["idInformacionPersona"]);
                                    if (row["numero_identificacion"] != DBNull.Value)
                                    {
                                        infoPersona._numeroIdentificacion = row["numero_identificacion"].ToString();
                                    }
                                    if (row["id_pais_fk"] != DBNull.Value)
                                    {
                                        infoPersona._pais = new Pais((int)row["id_pais_fk"],row["pais"].ToString());
                                    }
                                    if (row["id_estadocivil_fk"] != DBNull.Value)
                                    {
                                        infoPersona._estadoCivil = new EstadoCivil((int)row["id_estadocivil_fk"], row["estado_civil"].ToString());
                                    }
                                    if (row["foto"] != DBNull.Value)
                                    {
                                        infoPersona._fotoRuta = row["foto"].ToString();
                                    }
                                    //, (int), (int), (int)row["id_persona_fk"], row["foto"].ToString()
                                }
                                retorno.Add("persona", persona);
                                retorno.Add("formaciones", formaciones);
                                retorno.Add("laborales", laborales);
                                retorno.Add("infoPersona", infoPersona);
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
                public Dictionary<object, object> sp_rrhh_getMediosPersonas(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    // vas
                        List<EmailPersona> emails = null; List<TelefonoPersona> telefonos = null;
                        EmailPersona email; TelefonoPersona telefono;
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                    // do it 
                        SPIUS sp = new SPIUS("sp_rrhh_getMediosPersonas");
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
                                    emails = new List<EmailPersona>();
                                    foreach(DataRow row in tb[0].Rows){
                                        email = new EmailPersona((int)row["idMailPersona"], row["email"].ToString(), row["descripcion"].ToString());
                                        emails.Add(email);
                                    }
                                    
                                }
                                if (tb[1].Rows.Count > 0)
                                {
                                    telefonos = new List<TelefonoPersona>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {
                                        telefono                = new TelefonoPersona((int)row["idTelefonoPersona"]);
                                        telefono._telefono      = row["telefono"].ToString();
                                        telefono._descripcion   = row["descripcion"].ToString();
                                        telefonos.Add(telefono);
                                    }
                                }
                                retorno.Add("telefonos", telefonos);
                                retorno.Add("emails", emails);
                                return retorno;
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
                }
            #endregion
        #endregion
    }
}
