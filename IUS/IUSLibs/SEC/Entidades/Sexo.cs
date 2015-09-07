using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Sexo
    {
        #region "propiedades"
            public int      _idSexo;
            public string   _sexo;
        #endregion
        #region "Constructores"
            public Sexo(int idSexo)
            {
                this._idSexo = idSexo;
            }
            public Sexo(int idSexo,string sexo)
            {
                this._idSexo    = idSexo;
                this._sexo      = sexo;
            }
        #endregion
    }
}
