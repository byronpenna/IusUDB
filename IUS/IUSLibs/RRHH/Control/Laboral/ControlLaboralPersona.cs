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
                    sp.agregarParametro("idEmpresa", laboralAgregar._empresa._idEmpresa);
                    sp.agregarParametro("inicio", laboralAgregar._inicio);
                    sp.agregarParametro("fin", laboralAgregar._fin);
                    sp.agregarParametro("idPersona", laboralAgregar._persona._idPersona);
                    sp.agregarParametro("observaciones", laboralAgregar._observaciones);
                    sp.agregarParametro("idCargo", laboralAgregar._cargo._idCargoEmpresa);

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
                                laboralAgregado = new LaboralPersona((int)row["idLaboralPersona"], (int)row["id_empresa_fk"], (int)row["inicio"], (int)row["fin"], (int)row["id_persona_fk"], row["observaciones"].ToString(), (int)row["id_cargo_fk"]);

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
            #endregion
            #region "get"
                public Dictionary<object, object> sp_rrhh_getInfoInicialLaboralPersona(int idPersona, int idUsuarioEjecutor, int idPagina)
                {
                    // variables 
                    List<Empresa> empresas = null; Empresa empresa;
                    List<CargoEmpresa> cargos = null; CargoEmpresa cargo;
                    List<LaboralPersona> laboralesPersona = null; LaboralPersona laboralPersona;
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
                                empresas = new List<Empresa>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    empresa = new Empresa((int)row["idEmpresa"], row["nombre"].ToString(), row["direccion"].ToString(), (int)row["id_rubro_fk"]);
                                    empresas.Add(empresa);
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
                                    laboralPersona = new LaboralPersona((int)row["idLaboralPersona"], (int)row["id_empresa_fk"], (int)row["inicio"], (int)row["fin"],(int)row["id_persona_fk"] ,row["observaciones"].ToString(), (int)row["id_cargo_fk"]);
                                    laboralPersona._empresa._nombre = row["nombreEmpresa"].ToString();
                                    laboralPersona._cargo._cargo = row["cargo"].ToString();
                                    laboralesPersona.Add(laboralPersona);
                                }
                            }
                            retorno.Add("cargos", cargos);
                            retorno.Add("empresas", empresas);
                            retorno.Add("laboralesPersonas", laboralesPersona);
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
