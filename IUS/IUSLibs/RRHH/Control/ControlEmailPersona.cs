using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// internas 
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    // --------------
        using IUSLibs.RRHH.Entidades;
namespace IUSLibs.RRHH.Control
{
    public class ControlEmailPersona:PadreLib
    {
        #region "funciones"
            #region "do"
                public EmailPersona sp_rrhh_actualizarCorreoPersona(EmailPersona emailActualizar,int idUsuarioEjecutor, int idPagina)
                {
                    EmailPersona emailActualizado=null;
                    SPIUS sp = new SPIUS("sp_rrhh_actualizarCorreoPersona");

                    sp.agregarParametro("email", emailActualizar._email);
                    sp.agregarParametro("descripcion", emailActualizar._descripcion);
                    sp.agregarParametro("idMailPersona", emailActualizar._idEmail);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if(tb[1].Rows.Count >0){
                                DataRow row = tb[1].Rows[0];
                                emailActualizado = new EmailPersona((int)row["idMailPersona"],row["email"].ToString(),row["descripcion"].ToString(),(int)row["id_persona_fk"]);
                                
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
                    return emailActualizado;
                }
                public bool sp_rrhh_eliminarCorreoPersona(int idEmailPersona,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = true;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarCorreoPersona");
                    sp.agregarParametro("idMailPersona", idEmailPersona);
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
                public EmailPersona sp_rrhh_guardarCorreoPersona(EmailPersona emailAgregar,int idUsuarioEjecutor, int idPagina)
                {
                    EmailPersona emailAgregado = null;
                    SPIUS sp = new SPIUS("sp_rrhh_guardarCorreoPersona");
                    sp.agregarParametro("email", emailAgregar._email);
                    sp.agregarParametro("descripcion", emailAgregar._descripcion);
                    sp.agregarParametro("idPersona", emailAgregar._persona._idPersona);

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
                                emailAgregado = new EmailPersona((int)row["idMailPersona"], row["email"].ToString(), row["descripcion"].ToString(), (int)row["id_persona_fk"]);

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
                    return emailAgregado;
                }
            #endregion
        #endregion
    }
}
