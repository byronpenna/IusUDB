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
    //-------------------
        using IUSLibs.FrontUI.Entidades;
        using IUSLibs.SEC.Entidades;
        using IUSLibs.RRHH.Entidades;
        using IUSLibs.RRHH.Entidades.Laboral;
namespace IUSLibs.RRHH.Control.Laboral
{
    public class ControlLaboralPersona:PadreLib
    {
        #region "funciones"
            #region "do"
                public LaboralPersona sp_rrhh_insertLaboralPersonas(LaboralPersona laboralAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    LaboralPersona laboralAgregado = null;
                    SPIUS sp = new SPIUS("sp_rrhh_insertLaboralPersonas");
                    //sp.agregarParametro("idEmpresa", laboralAgregar._empresa._idEmpresa);
                    sp.agregarParametro("institucion", laboralAgregar._institucion);
                    sp.agregarParametro("inicio", laboralAgregar._inicio);
                    sp.agregarParametro("fin", laboralAgregar._fin);
                    sp.agregarParametro("idPersona", laboralAgregar._persona._idPersona);
                    //sp.agregarParametro("observaciones", laboralAgregar._observaciones);
                    sp.agregarParametro("cargo", laboralAgregar._cargo);

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
                                laboralAgregado                 = new LaboralPersona((int)row["idLaboralPersona"], row["institucion"].ToString(), (int)row["inicio"], (int)row["fin"], (int)row["id_persona_fk"], /*row["observaciones"].ToString(),*/ row["cargo"].ToString());
                                //laboralAgregado._cargo          = row["cargo"].ToString();
                                //laboralAgregado._empresa._nombre    = row["nombreEmpresa"].ToString();
                                //laboralAgregado._institucion    = row["institucion"].ToString();
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
                    return laboralAgregado;
                }
                public bool sp_rrhh_eliminarLaboralPersonas(int idLaboralPersona, int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarLaboralPersonas");
                    sp.agregarParametro("idLaboralPersona", idLaboralPersona);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
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
                public LaboralPersona sp_rrhh_editarLaboralPersonas(LaboralPersona laboralEditar, int idUsuarioEjecutor, int idPagina)
                {

                    LaboralPersona laboralEditado = null;
                    SPIUS sp = new SPIUS("sp_rrhh_editarLaboralPersonas");
                    
                    //sp.agregarParametro("idEmpresa", laboralEditar._empresa._idEmpresa);
                    sp.agregarParametro("institucion", laboralEditar._institucion);
                    sp.agregarParametro("inicio", laboralEditar._inicio);
                    sp.agregarParametro("fin", laboralEditar._fin);
                    //sp.agregarParametro("observaciones", laboralEditar._observaciones);
                    sp.agregarParametro("cargo", laboralEditar._cargo);
                    sp.agregarParametro("idLaboralPersona", laboralEditar._idLaboralPersona);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);

                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    laboralEditado = new LaboralPersona((int)row["idLaboralPersona"],row["institucion"].ToString(),(int)row["inicio"],(int)row["fin"],(int)row["id_persona_fk"],/*row["observaciones"].ToString(),*/row["cargo"].ToString());
                                    laboralEditado._cargo = row["cargo"].ToString();
                                    //laboralEditado._empresa._nombre = row["nombreEmpresa"].ToString();
                                    laboralEditado._institucion = row["nombreInstitucion"].ToString();
                                }
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
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
                    return laboralEditado;
                }
            #endregion
            #region "get"
                public Dictionary<object, object> sp_rrhh_getInfoInicialLaboralPersona(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    // variables 
                    List<Institucion> instituciones = null; Institucion institucion;
                    //List<Empresa> empresas = null; Empresa empresa;
                    List<CargoEmpresa> cargos = null; CargoEmpresa cargo;
                    List<LaboralPersona> laboralesPersona = null; LaboralPersona laboralPersona;
                    InformacionPersona infoPersona = null;
                    Persona persona =null ; 
                    // do 
                    Dictionary<object, object> retorno = new Dictionary<object, object>();
                    SPIUS sp = new SPIUS("sp_rrhh_getInfoInicialLaboralPersona");
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
                                instituciones = new List<Institucion>();
                                foreach(DataRow row in tb[0].Rows){
                                    institucion = new Institucion((int)row["idInstitucion"], row["nombre"].ToString());
                                    instituciones.Add(institucion);
                                }
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                cargos = new List<CargoEmpresa>();
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    cargo = new CargoEmpresa((int)row["idCargoEmpresa"], row["cargo"].ToString());
                                    cargos.Add(cargo);
                                }
                            }
                            if (tb[2].Rows.Count > 0)
                            {
                                laboralesPersona = new List<LaboralPersona>();
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    laboralPersona = new LaboralPersona((int)row["idLaboralPersona"], row["institucion"].ToString(), (int)row["inicio"], (int)row["fin"], (int)row["id_persona_fk"],/*row["observaciones"].ToString(),*/ row["cargo"].ToString());
                                    //laboralPersona._empresa._nombre = row["nombreEmpresa"].ToString();
                                    //laboralPersona._cargo._cargo        = row["cargo"].ToString();
                                    //laboralPersona._institucion._nombre = row["nombreInstitucion"].ToString();
                                    laboralesPersona.Add(laboralPersona);
                                }
                            }
                            if (tb[3].Rows.Count > 0)
                            {
                                DataRow row = tb[3].Rows[0];
                                persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString());
                            }
                            if (tb[4].Rows.Count > 0)
                            {
                                DataRow row                 = tb[4].Rows[0];
                                infoPersona                 = new InformacionPersona((int)row["idInformacionPersona"],row["foto"].ToString());
                                infoPersona._curriculumn    = row["curriculumn"].ToString();
                            }
                            retorno.Add("cargos", cargos);
                            retorno.Add("instituciones", instituciones);
                            retorno.Add("laboralesPersonas", laboralesPersona);
                            retorno.Add("persona", persona);
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
            #endregion
        #endregion
    }
}
