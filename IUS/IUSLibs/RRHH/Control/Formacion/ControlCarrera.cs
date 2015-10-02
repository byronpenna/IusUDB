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
        using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlCarrera:PadreLib
    {
        #region "funciones"
            #region "do"
                public Carrera sp_rrhh_editarCarrera(Carrera carreraEditar, int idUsuarioEjecutor, int idPagina)
                {
                    Carrera carreraEditada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_editarCarrera");
                    sp.agregarParametro("carrera", carreraEditar._carrera);
                    sp.agregarParametro("idNivel", carreraEditar._nivelTitulo._idNivel);
                    sp.agregarParametro("idInstitucion", carreraEditar._institucion._idInstitucion);
                    sp.agregarParametro("idArea", carreraEditar._area._idArea);
                    sp.agregarParametro("idCarrera", carreraEditar._idCarrera);

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
                                carreraEditada = new Carrera((int)row["idCarrera"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_institucion_fk"], (int)row["id_area_fk"]);
                                carreraEditada._nivelTitulo._nombreNivel = row["nombre_nivel"].ToString();
                                carreraEditada._institucion._nombre = row["nombreInstitucion"].ToString();
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
                    return carreraEditada;
                }
                public bool sp_rrhh_eliminarCarrera(int idCarrera, int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarCarrera");

                    sp.agregarParametro("idCarrera", idCarrera);
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
                public Carrera sp_rrhh_ingresarCarrera(Carrera carreraIngresar,int idUsuarioEjecutor,int idPagina)
                {
                    Carrera carreraIngresada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_ingresarCarrera");
                    
                    sp.agregarParametro("carrera", carreraIngresar._carrera);
                    sp.agregarParametro("idNivel", carreraIngresar._nivelTitulo._idNivel);
                    sp.agregarParametro("idInstitucion", carreraIngresar._institucion._idInstitucion);
                    sp.agregarParametro("idArea", carreraIngresar._area._idArea);

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
                                carreraIngresada = new Carrera((int)row["idCarrera"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_institucion_fk"], (int)row["id_area_fk"]);
                                carreraIngresada._nivelTitulo._nombreNivel  = row["nombre_nivel"].ToString();
                                carreraIngresada._institucion._nombre       = row["nombreInstitucion"].ToString();
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
                    return carreraIngresada;
                }
            #endregion
            #region "get"
                public List<Carrera> sp_rrhh_getCarreras(int idUsuarioEjecutor, int idPagina)
                {
                    List<Carrera> carreras = null; Carrera carrera;
                    SPIUS sp = new SPIUS("sp_rrhh_getCarreras");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                carreras = new List<Carrera>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    carrera = new Carrera((int)row["idCarrera"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_institucion_fk"], (int)row["id_area_fk"]);
                                    carrera._nivelTitulo._nombreNivel   = row["nombre_nivel"].ToString();
                                    carrera._institucion._nombre        = row["nombreInstitucion"].ToString();
                                    carreras.Add(carrera);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
                        }
                        return carreras;
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
