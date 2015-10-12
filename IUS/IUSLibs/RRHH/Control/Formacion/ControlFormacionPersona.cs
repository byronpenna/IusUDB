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
        using IUSLibs.RRHH.Entidades;
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
                    FormacionPersona formacionEditada=null;
                    SPIUS sp = new SPIUS("sp_rrhh_editarFormacionPersona");
                    sp.agregarParametro("yearFin", formacionEditar._yearFin);
                    sp.agregarParametro("observaciones", formacionEditar._observaciones);
                    //sp.agregarParametro("idEstadoCarrera", formacionEditar._estado._idEstadoCarrera);
                    sp.agregarParametro("carrera", formacionEditar._carrera);
                    sp.agregarParametro("idNivel", formacionEditar._nivelTitulo._idNivel);
                    sp.agregarParametro("idArea", formacionEditar._areaCarrera._idArea);
                    sp.agregarParametro("idFormacionPersona", formacionEditar._idFormacionPersona);
                    sp.agregarParametro("idPais", formacionEditar._paisInstitucion._idPais);
                    sp.agregarParametro("institucion", formacionEditar._institucion);

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
                                //formacionEditada
                                formacionEditada = new FormacionPersona(
                                                        (int)row["idFormacionPersona"],     (int)row["year_fin"], 
                                                        row["observaciones"].ToString(),    (int)row["id_persona_fk"], 
                                                        row["carrera"].ToString(),          (int)row["id_nivel_fk"],            
                                                        (int)row["id_area_fk"],             row["institucion"].ToString(),      
                                                        (int)row["id_paisinstitucion_fk"]
                                                    );
                                formacionEditada.
                                    _carrera                    = row["carrera"].ToString();
                                formacionEditada.
                                    _areaCarrera._area          = row["area"].ToString();
                                formacionEditada.
                                    _nivelTitulo._nombreNivel   = row["nombre_nivel"].ToString();
                                formacionEditada.
                                    _paisInstitucion._pais      = row["pais"].ToString();
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
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
                    return formacionEditada;
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
                    sp.agregarParametro("yearFin", formacionAgregar._yearFin);
                    sp.agregarParametro("observaciones", formacionAgregar._observaciones);
                    sp.agregarParametro("idPersona", formacionAgregar._persona._idPersona);
                    //sp.agregarParametro("idEstadoCarrera", formacionAgregar._estado._idEstadoCarrera);
                    sp.agregarParametro("carrera", formacionAgregar._carrera);
                    sp.agregarParametro("idNivel", formacionAgregar._nivelTitulo._idNivel);
                    sp.agregarParametro("idArea", formacionAgregar._areaCarrera._idArea);
                    /*
                        				varchar(250),
		                	int
                     */
                    sp.agregarParametro("institucion", formacionAgregar._institucion);
                    sp.agregarParametro("idPaisInstitucion", formacionAgregar._paisInstitucion._idPais);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if(tb[1].Rows.Count >0){
                                DataRow row = tb[1].Rows[0];
                                formacionAgregada       = new FormacionPersona(
                                                                (int)row["idFormacionPersona"],     (int)row["year_fin"], 
                                                                row["observaciones"].ToString(),    (int)row["id_persona_fk"],
                                                                row["carrera"].ToString(),          (int)row["id_nivel_fk"],            
                                                                (int)row["id_area_fk"],             row["institucion"].ToString(),      
                                                                (int)row["id_paisinstitucion_fk"]
                                                            );
                                formacionAgregada.
                                    _carrera                    = row["carrera"].ToString();
                                
                                formacionAgregada.
                                    _areaCarrera._area          = row["area"].ToString();
                                formacionAgregada.
                                    _nivelTitulo._nombreNivel   = row["nombre_nivel"].ToString();
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
                    List<Pais> paises = null; Pais pais;/**/ List<InstitucionEducativa> institucionesEducativas=null; InstitucionEducativa institucionEducativa;
                    List<NivelTitulo> nivelesTitulo = null; NivelTitulo nivelTitulo;
                    
                    List<FormacionPersona> formacionesPersonas = null; FormacionPersona formacionPersona;
                    InformacionPersona informacionPersona = null; List<AreaCarrera> areasCarreras = null; AreaCarrera areaCarrera;
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
                                // persona
                                DataRow row = tb[0].Rows[0];
                                persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString());

                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                paises = new List<Pais>();
                                // paises 
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    pais = new Pais((int)row["idPais"], row["pais"].ToString());
                                    paises.Add(pais);
                                }
                            }
                            if (tb[2].Rows.Count > 0)
                            {
                                institucionesEducativas = new List<InstitucionEducativa>();
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    institucionEducativa = new InstitucionEducativa((int)row["idInstitucion"],row["nombre"].ToString(),(int)row["id_pais_fk"]);
                                    institucionEducativa._pais._pais = row["pais"].ToString();
                                    institucionesEducativas.Add(institucionEducativa);
                                }
                            }
                            if (tb[3].Rows.Count > 0)
                            {
                                nivelesTitulo = new List<NivelTitulo>();
                                foreach (DataRow row in tb[3].Rows)
                                {
                                    nivelTitulo = new NivelTitulo((int)row["idNivel"], row["nombre_nivel"].ToString());
                                    nivelesTitulo.Add(nivelTitulo);
                                }
                            }
                            
                            if (tb[4].Rows.Count > 0)
                            {
                                formacionesPersonas = new List<FormacionPersona>();
                                foreach (DataRow row in tb[4].Rows)
                                {
                                    
                                    formacionPersona = new FormacionPersona(
                                                            (int)row["idFormacionPersona"],     (int)row["year_fin"], 
                                                            row["observaciones"].ToString(),    (int)row["id_persona_fk"], 
                                                            row["carrera"].ToString(),          (int)row["id_nivel_fk"],            
                                                            (int)row["id_area_fk"],             row["institucion"].ToString(),      
                                                            (int)row["id_paisinstitucion_fk"]
                                                       );
                                    formacionPersona.
                                        _carrera = row["carrera"].ToString();
                                    formacionPersona.
                                        _paisInstitucion._pais = row["pais"].ToString();
                                    formacionPersona.
                                        _areaCarrera._area = row["area"].ToString();
                                    formacionPersona.
                                        _nivelTitulo._nombreNivel = row["nombre_nivel"].ToString();
                                    formacionesPersonas.Add(formacionPersona);
                                }
                            }
                            if (tb[5].Rows.Count > 0)
                            {
                                DataRow row = tb[5].Rows[0];
                                informacionPersona = new InformacionPersona((int)row["idInformacionPersona"],row["foto"].ToString());
                            }
                            if(tb[6].Rows.Count >0)
                            {
                                areasCarreras = new List<AreaCarrera>();
                                foreach (DataRow row in tb[6].Rows)
                                {
                                    areaCarrera = new AreaCarrera((int)row["idArea"], row["area"].ToString());
                                    areasCarreras.Add(areaCarrera);
                                }
                                
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
                        }
                        
                        retorno.Add("persona", persona);
                        retorno.Add("paises", paises);
                        retorno.Add("instituciones", institucionesEducativas);
                        retorno.Add("nivelesTitulo", nivelesTitulo);
                        retorno.Add("formacionesPersonas", formacionesPersonas);
                        retorno.Add("informacionPersona", informacionPersona);
                        retorno.Add("areasCarreras", areasCarreras);
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
