using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SECPU.Entidades
{
    public class ValidadorPassPublico
    {
        #region "propiedades" 
            public int              _idValidadorPassPublico;
            public int              _codigo;
            public DateTime         _vencimiento;
            public int              _intentos;
            public UsuarioPublico   _usuarioPublico;
        #endregion
        #region "constructores"
            // full atributos
            public ValidadorPassPublico(int idValidadorPassPublico,int codigo,DateTime vencimiento,int intentos,int idUsuarioPublico)
            {
                this._idValidadorPassPublico    = idValidadorPassPublico;
                this._codigo                    = codigo;
                this._vencimiento               = vencimiento;
                this._intentos                  = intentos;
                this._usuarioPublico            = new UsuarioPublico(idUsuarioPublico);
            }
        #endregion
    }
}
