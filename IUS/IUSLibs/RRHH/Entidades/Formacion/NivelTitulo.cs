using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class NivelTitulo
    {
        #region "propiedades"
            public int      _idNivel;
            public string   _nombreNivel;
        #endregion
        #region "constructores"
            public NivelTitulo(int idNivel)
            {
                this._idNivel = idNivel;
            }
            public NivelTitulo(int idNivel,string nombreNivel)
            {
                this._idNivel       = idNivel;
                this._nombreNivel   = nombreNivel;
            }
        #endregion
    }
}
