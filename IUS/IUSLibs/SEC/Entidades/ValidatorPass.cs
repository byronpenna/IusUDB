using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class ValidatorPass
    {
        #region "propiedades"
            public int      _idValidatorPass;
            public int      _codigo;
            public DateTime _vencimiento;
            public int      _intentos;
            public Usuario  _usuario;
        #endregion
        #region "constructores"
            // full propiedades 
            public ValidatorPass(int idValidatorPass,int codigo,DateTime vencimiento,int intentos,Usuario usuario)
            {
                this._idValidatorPass   = idValidatorPass;
                this._codigo            = codigo;
                this._vencimiento       = vencimiento;
                this._intentos          = intentos;
                this._usuario           = usuario;
            }
        #endregion
    }
}
