using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SECPU.Entidades
{
    public class EstadoUsuarioPublico
    {
        #region "Propiedades"
            public int      _idEstado;
            public string   _estado;
        #endregion
        #region "constructores"
            public EstadoUsuarioPublico(int idEstado)
            {
                this._idEstado = idEstado;
            }
            public EstadoUsuarioPublico(string estado)
            {
                this._estado = estado;
            }
        #endregion
    }
}
