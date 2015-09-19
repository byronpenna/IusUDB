using System;
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
    // ---------------------
        using IUSLibs.RRHH.Entidades.Formacion;
        using IUSLibs.SEC.Entidades;
        using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlFormacionPersona:PadreLib
    {
        #region "funciones"
            #region "do"
                public FormacionPersona sp_rrhh_editarFormacionPersona(FormacionPersona formacionEditar,int idUsuarioEjecutor,int idPagina)
                {
                    SPIUS sp = new SPIUS("sp_rrhh_editarFormacionPersona+");

                }
                public bool sp_rrhh_eliminarTituloPersona(int idFormacionPersona,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarTituloPersona");
                    sp.agregarParametro("idFormacionPersona", idFormacionPersona);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                public FormacionPersona sp_rrhh_ingresarFormacionPersona(FormacionPersona formacionAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    FormacionPersona formacionAgregada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_ingresarFormacionPersona");
                    sp.agregarParametro("yearInicio", formacionAgregar._yearInicio);
                    sp.agregarParametro("yearFin", formacionAgregar._yearFin);
                    sp.agregarParametro("observaciones", formacionAgregar._observaciones);
                    sp.agregarParametro("idPersona", formacionAgregar._persona._idPersona);
                    sp.agregarParametro("idCarrera", formacionAgregar._carrera._idCarrera);
                    sp.agregarParametro("idEstadoCarrera", formacionAgregar._estado._idEstadoCarrera);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if(tb[0].Rows.Count >0){
                                DataRow row = tb[0].Rows[0];
                                formacionAgregada = new FormacionPersona((int)row["idFormacionPersona"], (int)row["year_inicio"], (int)row["year_fin"], row["observaciones"].ToString(), (int)row["id_persona_fk"], (int)row["id_carrera_fk"], (int)row["id_estadocarrera_fk"]);

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
                    return formacionAgregada;
                }
            #endregion
            #region "get"
                public Dictionary<object, object> sp_rrhh_getInfoInicialFormacion(int idPersona,int idUsuarioEjecutor, int idPagina)
                {
                    List<EstadoCarrera> estadosCarrera = null; EstadoCarrera estadoCarrera;
                    List<Pais> paises = null; Pais pais;/**/ List<InstitucionEducativa> institucionesEducativas=null; InstitucionEducativa institucionEducativa;
                    List<NivelTitulo> nivelesTitulo = null; NivelTitulo nivelTitulo;
                    List<Carrera> carreras = null; Carrera carrera;
                    List<FormacionPersona> formacionesPersonas = null; FormacionPersona formacionPersona;
                    Persona persona = null; 
                    Dictionary<object, object> retorno = new Dictionary<object, object>();
                    SPIUS sp = new SPIUS("sp_rrhh_getInfoInicialFormacion");
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
                                // estados de carrera
                                estadosCarrera = new List<EstadoCarrera>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    estadoCarrera = new EstadoCarrera((int)row["idEstadoCarrera"], row["estado"].ToString());
                                    estadosCarrera.Add(estadoCarrera);
                                }
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                // persona
                                DataRow row = tb[1].Rows[0];
                                persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString());

                            }
                            if (tb[2].Rows.Count > 0)
                            {
                                paises = new List<Pais>();
                                // paises 
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    pais = new Pais((int)row["idPais"], row["pais"].ToString());
                                    paises.Add(pais);
                                }
                            }
                            if (tb[3].Rows.Count > 0)
                            {
                                institucionesEducativas = new List<InstitucionEducativa>();
                                foreach (DataRow row in tb[3].Rows)
                                {
                                    institucionEducativa = new InstitucionEducativa((int)row["idInstitucion"],row["nombre"].ToString(),(int)row["id_pais_fk"]);
                                    institucionEducativa._pais._pais = row["pais"].ToString();
                                    institucionesEducativas.Add(institucionEducativa);
                                }
                            }
                            if (tb[4].Rows.Count > 0)
                            {
                                nivelesTitulo = new List<NivelTitulo>();
                                foreach (DataRow row in tb[4].Rows)
                                {
                                    nivelTitulo = new NivelTitulo((int)row["idNivel"], row["nombre_nivel"].ToString());
                                    nivelesTitulo.Add(nivelTitulo);
                                }
                            }
                            if (tb[5].Rows.Count > 0)
                            {
                                carreras = new List<Carrera>();
                                foreach (DataRow row in tb[5].Rows)
                                {
                                    carrera = new Carrera((int)row["idCarrera"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_institucion_fk"]);
                                    carrera._nivelTitulo._nombreNivel = row["nombre_nivel"].ToString();
                                    carrera._institucion._nombre = row["nombreInstitucion"].ToString();
                                    carreras.Add(carrera);
                                }
                            }
                            if (tb[6].Rows.Count > 0)
                            {
                                formacionesPersonas = new List<FormacionPersona>();
                                foreach (DataRow row in tb[6].Rows)
                                {
                                    formacionPersona = new FormacionPersona((int)row["idFormacionPersona"], (int)row["year_inicio"], (int)row["year_fin"], row["observaciones"].ToString(), (int)row["id_persona_fk"], (int)row["id_carrera_fk"], (int)row["id_estadocarrera_fk"]);
                                    formacionPersona._carrera._carrera = row["carrera"].ToString();
                                    formacionPersona._estado._estado = row["estado"].ToString();
                                    formacionesPersonas.Add(formacionPersona);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
                        }
                        retorno.Add("estadosCarrera", estadosCarrera);
                        retorno.Add("persona", persona);
                        retorno.Add("paises", paises);
                        retorno.Add("instituciones", institucionesEducativas);
                        retorno.Add("nivelesTitulo", nivelesTitulo);
                        retorno.Add("carreras", carreras);
                        retorno.Add("formacionesPersonas", formacionesPersonas);
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
            #endregion
        #endregion
    }
}
