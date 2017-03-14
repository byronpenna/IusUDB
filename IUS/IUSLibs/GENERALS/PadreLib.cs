using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
    using IUSLibs.LOGS;
// librerias externas
    using IUSLibs.BaseDatos;
namespace IUSLibs.GENERALS
{
    public class PadreLib
    {
        #region "propiedades"
            public enum Permisos
            {
                Crear=1,Editar=2,Eliminar=3,ver=4
            }
            
        #endregion
        #region "funciones heredadas"
            public string relativePathFromAbsolute(string appPath,string absoultePath) {
                return absoultePath.Substring(appPath.Length).Replace('\\', '/').Insert(0, "~/");
            }
            public RRHH.Entidades.InformacionPersona detallePersona(int idUsuarioEjecutor)
            {
                RRHH.Entidades.InformacionPersona informacionPersona = new RRHH.Entidades.InformacionPersona(-1);
                SPIUS sp = new SPIUS("sp_rrhh_detalleLogin");
                sp.agregarParametro("idUsuario", idUsuarioEjecutor);
                try
                {    
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            informacionPersona = new RRHH.Entidades.InformacionPersona((int)row["idInformacionPersona"]);
                            if (row["id_persona_fk"] != DBNull.Value)
                            {
                                informacionPersona._persona = new SEC.Entidades.Persona((int)row["id_persona_fk"]);

                            }
                            if(row["foto"] != DBNull.Value){
                                informacionPersona._fotoRuta = row["foto"].ToString();
                            }
                            
                        }
                    }
                    return informacionPersona;
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x) {
                    throw x;
                }
            }
        // contendra todas los metodos y atributos de uso general
            public bool sp_sec_registrarError(string mensaje,string detalle,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    bool respuesta = false;
                    SPIUS sp = new SPIUS("sp_sec_registrarError");
                    
                    sp.agregarParametro("mensaje", mensaje);
                    sp.agregarParametro("detalle", detalle);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);

                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        respuesta = true;
                    }
                    return respuesta;
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
            //############
            protected bool DataSetDontHaveTable(DataSet ds)
            {
                bool toReturn = false;
                if(ds.Tables.Count == 0)
                {
                    toReturn = true;
                }
                return toReturn;
            }
            protected DataTableCollection getTables(DataSet ds)
            {
                DataTableCollection tables = null;
                if (!this.DataSetDontHaveTable(ds))
                {
                    tables = ds.Tables;
                }
                return tables;
            }
            protected bool objToBool(string val)
            {
                int ival = Convert.ToInt32(val);
                if (ival <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            protected ErroresIUS getErrorFromExecProcedure(DataRow row,bool mostrar=false)
            {
                string message = row["errorMessage"].ToString();
                if(message == ""){
                    message = "Error no controlado";
                }
                ErroresIUS x = new ErroresIUS(message,ErroresIUS.tipoError.sql, (int)row["errorCode"], row["errorSql"].ToString());
                x.numeroError = (int)row["NumeroError"];
                if (!x._mostrar)
                {
                    if (row["mostrar"] == DBNull.Value)
                    {
                        x._mostrar = false;
                    }
                    else
                    {
                        x._mostrar = (bool)row["mostrar"];
                    }
                }
                else
                {
                    x._mostrar = true;
                }
                return x;
            }
            #region "resultadoCorrectoGet"
                protected bool resultadoCorrectoGet(DataTableCollection tb, int iTabla)
                {
                    return this.resultadoCorrecto(tb, "estadoProc", iTabla);
                }
                protected bool resultadoCorrectoGet(DataTableCollection tb)
                {
                    return this.resultadoCorrecto(tb, "estadoProc", tb.Count - 1);
                }
            #endregion
            #region "Resultado Correcto"
                protected bool resultadoCorrecto(DataSet ds)
                {
                    string columnaEstado = "estadoProc";
                    return this.resultadoCorrecto(ds.Tables, columnaEstado);
                }
                protected bool resultadoCorrecto(DataSet ds,string columnaEstado)
                {
                    return this.resultadoCorrecto(ds.Tables,columnaEstado);
                }
                protected bool resultadoCorrecto(DataTableCollection tb)
                {
                    return this.resultadoCorrecto(tb, "estadoProc");
                }
                protected bool resultadoCorrecto(DataTableCollection tb,string columnaEstado)
                {
                    return this.resultadoCorrecto(tb, columnaEstado, 0);
                }
                protected bool resultadoCorrecto(DataTableCollection tb, string columnaEstado, int iTabla)
                {
                    try
                    {
                        if (tb != null && (int)tb[iTabla].Rows[0][columnaEstado] == 1)
                        {
                            return true;
                        }
                        else
                        {
                            //ErroresIUS x = new ErroresIUS()
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow rowError = tb[0].Rows[0];
                                string message = rowError["errorMessage"].ToString();
                                /*bool mostrar = false;
                                if (tb[0].Columns.Contains("mostrar"))
                                {
                                    mostrar = (bool)rowError["mostrar"];
                                }
                                ErroresIUS errores = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"], rowError["errorSql"].ToString());
                                errores._mostrar = mostrar;*/
                                ErroresIUS x = this.getErrorFromExecProcedure(rowError);
                                throw x;
                            }
                            return false;
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        return false;
                    }
                }
            #endregion
        #endregion
    }
}
