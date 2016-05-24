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
            private List<List<Parametro>> _arregloDeParametros;
        #endregion
        #region "Funciones"
            #region "funciones privadas"
                #region "parametros a command"
                    public void parametrosAcommand(ref SqlCommand command)
                    {
                        this.parametrosAcommand(ref command, this.parametros);
                    }
                    public void parametrosAcommand(ref SqlCommand command,List<Parametro> parametros)
                    {
                        foreach (Parametro parametro in parametros)
                        {
                            SqlParameter param;
                            if (parametro.valor != null) {
                                 param = new SqlParameter(parametro.variable, parametro.getTypeProperty);
                            }
                            else
                            {
                                param = new SqlParameter(parametro.variable, parametro._tipo);
                            }
                            param.Direction = ParameterDirection.Input;
                            if (parametro.valor != null) {
                                param.Value = parametro.valor;
                            }
                            else
                            {
                                param.Value = DBNull.Value;
                            }
                            command.Parameters.Add(param);
                        }
                    }
                #endregion
            #endregion
            #region "funciones a utilizar externo"
                public SqlDataAdapter pruebaAdapter()
                {
                    
                    ConexionIUS cn = new ConexionIUS();
                    
                    try{
                        
                        SqlCommand command = new SqlCommand(this.nombre, cn.cn);
                        
                        command.CommandType = CommandType.StoredProcedure;
                        // se omitio el timeout
                        this.parametrosAcommand(ref command);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        return adapter;
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
                }
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
                public bool ejecutarInsertMultiple()
                {
                    bool toReturn = false;
                    ConexionIUS cn = new ConexionIUS();
                    SqlTransaction trans = null;
                    try
                    {
                        cn.cn.Open();
                        trans = cn.cn.BeginTransaction();
                        SqlCommand command = new SqlCommand(this.nombre, cn.cn);
                        command.Transaction = trans;
                        foreach(List<Parametro> parametro in this._arregloDeParametros){
                            command.Parameters.Clear();
                            command.CommandText = this.nombre;
                            this.parametrosAcommand(ref command, parametro);
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                        }
                        trans.Commit();
                        cn.cn.Close();
                        toReturn = true;
                    }
                    catch (SqlException x)
                    {
                        trans.Rollback();
                        cn.cn.Close();
                        throw x;
                    }
                    catch (Exception x)
                    {
                        trans.Rollback();
                        cn.cn.Close();
                        throw x;
                    }
                    
                    return toReturn;
                }
                public void limpiarParametros()
                {
                    this.parametros.Clear();
                }
                #region "agregar parametros"
                    public void agregarParametro(Dictionary<String,Object> parametros)
                    {
                        Parametro parametroGenerico;
                        List<Parametro> listParametro = new List<Parametro>();
                        foreach (var item in parametros)
                        {
                            parametroGenerico = new Parametro("@" + item.Key, item.Value);
                            listParametro.Add(parametroGenerico);
                        }
                        this._arregloDeParametros.Add(listParametro);
                    }
                    public void agregarParametro(string pVariable,DbType tipo)
                    {
                        Parametro parametroGenerico = new Parametro("@" + pVariable,tipo);
                        this.parametros.Add(parametroGenerico);
                    }
                    public void agregarParametro(string pVariable,Object pValor){
                        Parametro parametroGenerico = new Parametro("@" + pVariable,pValor);
                        this.parametros.Add(parametroGenerico);
                    }
                #endregion
            #endregion
        #endregion
        #region "Constructores"
            public SPIUS(string nombre)
            {
                this.nombre = nombre;
                this.parametros = new List<Parametro>();
                this._arregloDeParametros = new List<List<Parametro>>();
            }
        #endregion
    }
}
