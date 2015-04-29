using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.LOGS
{
    public class ErroresIUS : Exception
    {
        /*
         * 2627 violacion de constraint unico.
         */
        #region "enums"
            public enum tipoError
            {
                sql,generico
            }
            
        #endregion
        #region "gets y sets"
            public tipoError errorType
            {
                get
                {
                    return this._tipoError;
                }
                set
                {
                    this._tipoError = value;
                }
            }
            public int errorNumber
            {
                get
                {
                    return this._codigoError;
                }
                set
                {
                    this._codigoError = value;
                }
            }
            public string _errorSql;
        #endregion
        #region "propiedades"
            private int _codigoError;
            private tipoError _tipoError;
            public bool _mostrar=false;
        #endregion
        #region "metodos"
            public void setTipoError(tipoError t)
            {
                this._tipoError = t;
            }
        #endregion
            #region "Constructores"
            public ErroresIUS()
            {

            }
            public ErroresIUS(string message):base(message)
            {

            }
            public ErroresIUS(string message,tipoError errorType,int codigoError): base(message)
            {
                this._tipoError = errorType;
                this._codigoError = codigoError;
                this._errorSql = "";
            }
            public ErroresIUS(string message, tipoError errorType, int codigoError,string errorSql)
                : base(message)
            {
                this._tipoError = errorType;
                this._codigoError = codigoError;
                this._errorSql = errorSql;
            }
            public ErroresIUS(string message, tipoError errorType, int codigoError, string errorSql,bool mostrar)
                : base(message)
            {
                this._tipoError = errorType;
                this._codigoError = codigoError;
                this._errorSql = errorSql;
                this._mostrar = mostrar;
            }
        #endregion
    }
}
