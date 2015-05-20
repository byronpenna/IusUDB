using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
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
            #region "Resultado Correcto"
                public bool resultadoCorrecto(DataSet ds)
                {
                    string columnaEstado = "estadoProc";
                    return this.resultadoCorrecto(ds.Tables, columnaEstado);
                }
                public bool resultadoCorrecto(DataSet ds,string columnaEstado)
                {
                    return this.resultadoCorrecto(ds.Tables,columnaEstado);
                }
                protected bool resultadoCorrecto(DataTableCollection tb)
                {
                    return this.resultadoCorrecto(tb, "estadoProc");
                }
                protected bool resultadoCorrecto(DataTableCollection tb,string columnaEstado)
                {
                    try
                    {
                        if (tb != null && (int)tb[0].Rows[0][columnaEstado] == 1)
                        {
                            return true;
                        }
                        else
                        {
                            //ErroresIUS x = new ErroresIUS()
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow rowError = tb[0].Rows[0];
                                ErroresIUS errores = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"], rowError["errorMessage"].ToString());
                                throw errores;
                            }
                            return false;
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    
                }
            #endregion
        #endregion
    }
}
