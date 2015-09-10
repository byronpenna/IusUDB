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
    public class ControlTelefonoPersona:PadreLib
    {
        #region "funciones"
            #region "do"
                public TelefonoPersona sp_rrhh_editarTelefonoPersona(TelefonoPersona telefonoActualizar,int idUsuarioEjecutor,int idPagina)
                {

                    TelefonoPersona telefonoActualizado = null;
                    SPIUS sp = new SPIUS("sp_rrhh_editarTelefonoPersona");
                    
                    sp.agregarParametro("telefono", telefonoActualizar._telefono);
                    sp.agregarParametro("descripcion", telefonoActualizar._descripcion);
                    sp.agregarParametro("idPais", telefonoActualizar._pais._idPais);
                    sp.agregarParametro("idTelefonoPersona", telefonoActualizar._idTelefonoPersona);
                    
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if(tb[1].Rows.Count >0){
                                DataRow row = tb[1].Rows[0];
                                telefonoActualizado = new TelefonoPersona((int)row["idTelefonoPersona"], row["telefono"].ToString(), row["descripcion"].ToString(), (int)row["id_pais_fk"], (int)row["id_persona_fk"]);

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
                    return telefonoActualizado;
                }
                public bool sp_rrhh_eliminarTel(int idTelefonoPersona, int idUsuarioEjecutor,int idPagina)
                {
                    bool estado=false;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarTel");
                    sp.agregarParametro("idTelefonoPersona", idTelefonoPersona);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try {
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
                public TelefonoPersona sp_rrhh_guardarTelefonoPersona(TelefonoPersona telefonoAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    TelefonoPersona telefonoAgregado = null;
                    SPIUS sp = new SPIUS("sp_rrhh_guardarTelefonoPersona");
                    
                    sp.agregarParametro("telefono", telefonoAgregar._telefono);
                    sp.agregarParametro("descripcion", telefonoAgregar._descripcion);
                    sp.agregarParametro("id_pais_fk", telefonoAgregar._pais._idPais);

                    sp.agregarParametro("idPersona", telefonoAgregar._persona._idPersona);
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
                                telefonoAgregado = new TelefonoPersona((int)row["idTelefonoPersona"], row["telefono"].ToString(), row["descripcion"].ToString(), (int)row["id_pais_fk"], (int)row["id_persona_fk"]);
                                telefonoAgregado._pais._pais = row["pais"].ToString();
                            }

                        }
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x) {
                        throw x;
                    }
                    return telefonoAgregado;
                }
            #endregion
            #region "get"
            
            #endregion
        #endregion
    }
}
