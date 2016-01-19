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

    using IUSLibs.LOGS;
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
                if (!x._mostrar)
                {
                    x._mostrar = (bool)row["mostrar"];
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
