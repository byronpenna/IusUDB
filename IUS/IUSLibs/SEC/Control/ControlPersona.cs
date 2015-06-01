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
                    Persona personaAgregada = null;
                    SPIUS sp = new SPIUS("sp_hm_agregarPersona");
                    sp.agregarParametro("nombres",persona._nombres);
                    sp.agregarParametro("apellidos", persona._apellidos);
                    sp.agregarParametro("fechaNacimiento", persona._fechaNacimiento);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        //DataSet ds = sp.EjecutarProcedimiento();
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb)) {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow rowPersona = tb[1].Rows[0];
                                personaAgregada = new Persona((int)rowPersona["idPersona"], rowPersona["nombres"].ToString(), rowPersona["apellidos"].ToString(), (DateTime)rowPersona["fecha_nacimiento"]);
                            }
                        }
                        /*if (tables != null)
                        {
                            if ((int)tables[0].Rows[0]["estadoInsert"] == 1)
                            {
                                DataRow rowPersona = tables[1].Rows[0];
                                personaAgregada = new Persona((int)rowPersona["idPersona"], rowPersona["nombres"].ToString(), rowPersona["apellidos"].ToString(), (DateTime)rowPersona["fecha_nacimiento"]);
                            }
                        }*/
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
                    ErroresIUS errorIus; // manejo de errores
                    SPIUS sp = new SPIUS("sp_hm_editarPersona");
                    // para actualizar
                        sp.agregarParametro("nombres", persona._nombres);
                        sp.agregarParametro("apellidos", persona._apellidos);
                        sp.agregarParametro("fecha", persona._fechaNacimiento);
                        sp.agregarParametro("idPersona", persona._idPersona);
                    // para permisos
                        sp.agregarParametro("idUsuarioAccion", idUsuario);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrecto(tb) && tb[1].Rows.Count > 0)
                            {
                                DataRow rowResult = tb[1].Rows[0];
                                personaReturn = new Persona((int)rowResult["idPersona"], rowResult["nombres"].ToString(), rowResult["apellidos"].ToString(), (DateTime)rowResult["fecha_nacimiento"]);
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
                        
                    //DataSet ds = sp.EjecutarProcedimiento();
                    /*if (!this.DataSetDontHaveTable(ds))
                    {
                        DataTable table = ds.Tables[0];

                        if ((int)table.Rows[0]["estadoUpdate"] == 1)
                        {
                            if(ds.Tables.Count > 1 ){
                                DataRow rowResult = ds.Tables[1].Rows[0];
                                personaReturn = new Persona((int)rowResult["idPersona"], rowResult["nombres"].ToString(), rowResult["apellidos"].ToString(), (DateTime)rowResult["fecha_nacimiento"]);
                            }else{
                                errorIus = new ErroresIUS("Error no controlado sql", ErroresIUS.tipoError.sql, -2);
                                throw errorIus;
                            }
                        }
                        else
                        {
                            if (ds.Tables.Count > 1)
                            {
                                DataRow rowError = ds.Tables[1].Rows[0];
                                errorIus = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"]);
                                throw errorIus;
                            }
                            else
                            {
                                errorIus = new ErroresIUS("Error no controlado sql", ErroresIUS.tipoError.sql, -2);
                                throw errorIus;
                            }
                    
                        }
                    }*/
                    return personaReturn;
                }
            #endregion
            #region "get"
                public List<Persona> getPersonas()
            {
                List<Persona> personas = new List<Persona>();
                Persona persona;
                SPIUS sp = new SPIUS("sp_sec_getPersonas");
                DataSet ds = sp.EjecutarProcedimiento();
                if (!this.DataSetDontHaveTable(ds))
                {
                    DataTable table = ds.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString(), (DateTime)row["fecha_nacimiento"]);
                        personas.Add(persona);
                    }
                }
                return personas;
            }
            #endregion
        #endregion
    }
}
