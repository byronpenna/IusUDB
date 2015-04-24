using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
namespace IUSLibs.BaseDatos
{
    public class Parametro
    {
        #region "Propiedades"
            public string variable;
            public Object valor;
            public enum direction { input, output }
            public direction direccion;
            public DbType _tipo;
        #endregion

        #region "Gets and set"
            public SqlDbType getTypeProperty{
                get
                {
                    string fullname = this.valor.GetType().FullName;
                    SqlDbType tipoDato;
                    switch (fullname)
                    {
                        case "System.String":
                        case "System.Data.SqlTypes.SqlString":
                            {
                                tipoDato = SqlDbType.VarChar;
                                break;
                            }
                        case "System.Decimal":
                            {
                                tipoDato = SqlDbType.Decimal;
                                break;
                            }
                        case "System.Double":
                        case "System.Data.SqlTypes.SqlDouble":
                            {
                                tipoDato = SqlDbType.Float;
                                break;
                            }
                        case "System.DateTime":
                        case "System.Data.SqlTypes.SqlDateTime":
                            {
                                tipoDato = SqlDbType.DateTime;
                                break;
                            }
                        case "System.Data.SqlTypes.SqlDecimal":
                            {
                                tipoDato = SqlDbType.Decimal;
                                break;
                            }
                        case "System.Data.SqlTypes.SqlBoolean":
                            {
                                tipoDato = SqlDbType.Bit;
                                break;
                            }
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Data.SqlTypes.SqlInt16":
                        case "System.Data.SqlTypes.SqlInt32":
                        case "System.Data.SqlTypes.SqlInt64":
                            {
                                tipoDato = SqlDbType.Int;
                                break;
                            }
                        case "System.Byte":
                        case "System.Data.SqlTypes.SqlBytes":
                            {
                                tipoDato = SqlDbType.Image;
                                break;
                            }
                        default:
                            {
                                // En caso no haya sido identificado
                                tipoDato = SqlDbType.VarChar;
                                break;
                            }
                    }
                    return tipoDato;
                    
                }
            }
        #endregion 

        #region "Constructores"
            public Parametro(string pvariable,DbType tipo,direction x = direction.input)
            {
                this.variable = pvariable;
                this.direccion = x;
                this.valor = null;
                this._tipo = tipo;
            }
            public Parametro(string pvariable,Object pvalor,direction x = direction.input)
            {
                // el try catch es inecesario solo es una simple asignacion
                this.variable = pvariable;
                this.valor = pvalor;
                this.direccion = x;
            }
        #endregion
    }
}
