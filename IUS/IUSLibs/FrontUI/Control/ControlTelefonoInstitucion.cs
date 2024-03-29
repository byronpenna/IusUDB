﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.FrontUI.Control
{
    public class ControlTelefonoInstitucion:PadreLib
    {
        #region "get"
            
            #region "front end"
                public List<TelefonoInstitucion> sp_frontui_spFront_getTelByInstitucion(int idInstitucion,string ip,int idPagina)
                {
                    List<TelefonoInstitucion> telefonos = null; TelefonoInstitucion telefono;
                    SPIUS sp = new SPIUS("sp_frontui_spFront_getTelByInstitucion");
                    sp.agregarParametro("idInstitucion", idInstitucion);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                telefonos = new List<TelefonoInstitucion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    telefono = new TelefonoInstitucion((int)row["idTelefono"], row["telefono"].ToString(), row["texto_telefono"].ToString(), (int)row["id_institucion_fk"]);
                                    telefonos.Add(telefono);
                                }
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
                    return telefonos;
                }
            #endregion
            #region "backend"
                public List<TelefonoInstitucion> sp_frontui_getTelInstitucionByInstitucion(int idInstitucion, int idUsuarioEjecutor,int idPagina)
            {
                List<TelefonoInstitucion> telefonos = null; TelefonoInstitucion telefono;
                SPIUS sp = new SPIUS("sp_frontui_getTelInstitucionByInstitucion");
                sp.agregarParametro("idInstitucion", idInstitucion);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            telefonos = new List<TelefonoInstitucion>();
                            foreach (DataRow row in tb[0].Rows)
                            {
                                telefono = new TelefonoInstitucion((int)row["idTelefono"], row["telefono"].ToString(), row["texto_telefono"].ToString(), (int)row["id_institucion_fk"]);
                                telefonos.Add(telefono);
                            }
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
                return telefonos;
            }
            #endregion
        #endregion
        #region "do"
            #region "backend"
                public TelefonoInstitucion sp_frontui_editTelInstitucion(TelefonoInstitucion telefonoEditar,int idUsuarioEjecutor,int idPagina)
                {
                    TelefonoInstitucion telefonoEditado = null;
                    SPIUS sp = new SPIUS("sp_frontui_editTelInstitucion");
                    
                    sp.agregarParametro("telefono", telefonoEditar._telefono);
                    sp.agregarParametro("texto_telefono", telefonoEditar._textoTelefono);
                    sp.agregarParametro("idTelefono", telefonoEditar._idTelefono);
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
                                telefonoEditado = new TelefonoInstitucion((int)row["idTelefono"], row["telefono"].ToString(), row["texto_telefono"].ToString(), (int)row["id_institucion_fk"]);
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
                    return telefonoEditado;
                }
                public bool sp_frontui_deleteTelInstitucion(int idTel,int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_deleteTelInstitucion");
                    sp.agregarParametro("idTel", idTel);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
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
                public TelefonoInstitucion sp_frontui_insertTelInstitucion(TelefonoInstitucion telefonoIngresar,int idUsuarioEjecutor,int idPagina)
                {
                    TelefonoInstitucion telefono = null;
                    SPIUS sp = new SPIUS("sp_frontui_insertTelInstitucion");
                    sp.agregarParametro("telefono", telefonoIngresar._telefono);
                    sp.agregarParametro("texto_telefono", telefonoIngresar._textoTelefono);
                    sp.agregarParametro("idInstitucion", telefonoIngresar._institucion._idInstitucion);
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
                                telefono = new TelefonoInstitucion((int)row["idTelefono"], row["telefono"].ToString(), row["texto_telefono"].ToString(), (int)row["id_institucion_fk"]);
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            bool mostrar = false;
                            if (tb[0].Columns.Contains("mostrar"))
                            {
                                mostrar = true;
                            }
                            ErroresIUS x = this.getErrorFromExecProcedure(row,mostrar);
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
                    return telefono;
                }
            #endregion
        #endregion
    }
}
