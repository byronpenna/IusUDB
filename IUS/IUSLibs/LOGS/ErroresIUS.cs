using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.LOGS
{
    public class ErroresIUS : Exception
    {
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
        #endregion
        #region "propiedades"
            private int _codigoError;
            private tipoError _tipoError;
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
        #endregion
    }
}
