using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using IUSLibs.LOGS;
namespace IUSLibs.BaseDatos
{
    class SPIUS
    {
        #region "Propiedades"
            private String nombre;// nombre de procedimiento
            private List<Parametro> parametros; // parametro generico para iterar antes de ejecutar procedimiento
        #endregion
        #region "Funciones"
            #region "funciones privadas"
            public void parametrosAcommand(ref SqlCommand command)
            {
                foreach (Parametro parametro in this.parametros)
                {
                    SqlParameter param = new SqlParameter(parametro.variable, parametro.getTypeProperty);
                    param.Direction = ParameterDirection.Input;
                    param.Value = parametro.valor;
                    command.Parameters.Add(param);
                }
            }
            #endregion
            #region "funciones a utilizar externo"
            public DataSet EjecutarProcedimiento()
            {
                DataSet ds;
                ConexionIUS cn = new ConexionIUS();
                try{
                    SqlCommand command = new SqlCommand(this.nombre, cn.cn);
                    command.CommandType = CommandType.StoredProcedure;
                    // se omitio el timeout
                    this.parametrosAcommand(ref command);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    cn.cn.Close();
                }
                catch (SqlException x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message);
                    error.errorType = ErroresIUS.tipoError.sql;
                    error.errorNumber = x.Number;
                    throw error;
                }
                catch (Exception x)
                {
                    ErroresIUS error = new ErroresIUS(x.Message);
                    error.errorType = ErroresIUS.tipoError.generico;
                    error.errorNumber = -1;
                    throw x;
                }
                return ds;
            }
            public void limpiarParametros()
            {
                this.parametros.Clear();
            }
            public void agregarParametro(string pVariable,Object pValor){
                Parametro parametroGenerico = new Parametro("@" + pVariable,pValor);
                this.parametros.Add(parametroGenerico);
            }
            #endregion
        #endregion
        #region "Constructores"
            public SPIUS(string nombre)
            {
                this.nombre = nombre;
                this.parametros = new List<Parametro>();
            }
        #endregion
    }
}
