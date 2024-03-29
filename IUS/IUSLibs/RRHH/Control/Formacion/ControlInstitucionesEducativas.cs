﻿using System;
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
    // --------------
        using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlInstitucionesEducativas:PadreLib
    {
        #region "funciones"
            #region "do"
                public InstitucionEducativa sp_rrhh_editarInstitucionEducativa(InstitucionEducativa institucionEditar, int idUsuarioEjecutor, int idPagina)
                {
                    InstitucionEducativa institucionEditada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_editarInstitucionEducativa");
                    sp.agregarParametro("nombre", institucionEditar._nombre);
                    sp.agregarParametro("idPais", institucionEditar._pais._idPais);
                    sp.agregarParametro("idInstitucionEducativa", institucionEditar._idInstitucion);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                institucionEditada = new InstitucionEducativa((int)row["idInstitucion"], row["nombre"].ToString(), (int)row["id_pais_fk"]);
                                institucionEditada._pais._pais = row["pais"].ToString();
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
                    return institucionEditada;
                }
                public bool sp_rrhh_eliminarInstitucionEducativa(int idInstitucionFormacion, int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_rrhh_eliminarInstitucionEducativa");
                    sp.agregarParametro("idInstitucionFormacion", idInstitucionFormacion);
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
                public InstitucionEducativa sp_rrhh_ingresarInstitucionEducativa(InstitucionEducativa institucionAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    InstitucionEducativa agregada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_ingresarInstitucionEducativa");
                    
                    sp.agregarParametro("nombreInstitucion", institucionAgregar._nombre);
                    sp.agregarParametro("idPais", institucionAgregar._pais._idPais);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if(tb[1].Rows.Count > 0){
                                DataRow row = tb[1].Rows[0];
                                agregada = new InstitucionEducativa((int)row["idInstitucion"], row["nombre"].ToString(), (int)row["id_pais_fk"]);
                                agregada._pais._pais = row["pais"].ToString();
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
                    return agregada;
                }
                
            #endregion
            #region "get"
                public List<InstitucionEducativa> sp_rrhh_getInstitucionesEducativas(int idUsuarioEjecutor,int idPagina)
                {
                    List<InstitucionEducativa> institucionesEducativas = null; InstitucionEducativa institucion;
                    SPIUS sp = new SPIUS("sp_rrhh_getInstitucionesEducativas");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                institucionesEducativas = new List<InstitucionEducativa>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    institucion = new InstitucionEducativa((int)row["idInstitucion"], row["nombre"].ToString(), (int)row["id_pais_fk"]);
                                    institucionesEducativas.Add(institucion);
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
                    return institucionesEducativas;
                }
            #endregion
        #endregion
    }
}
