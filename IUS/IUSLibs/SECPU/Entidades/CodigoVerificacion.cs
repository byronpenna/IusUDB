using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SECPU.Entidades
{
    public class CodigoVerificacion
    {
        #region "propiedades"
            public int              _idCodigoVerificacion;
            public int              _numero;
            public DateTime         _fechaVencimiento;
            public UsuarioPublico   _usuarioPublico;
        #endregion
        #region "constructores"
            public CodigoVerificacion(int idCodigoVerificacion,int numero,DateTime fechaVencimiento,int idUsuarioPublico)
            {
                this._idCodigoVerificacion  = idCodigoVerificacion;
                this._numero                = numero;
                this._fechaVencimiento      = fechaVencimiento;
                this._usuarioPublico        = new UsuarioPublico(idUsuarioPublico);
            }
        #endregion
    }
}
