using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class Continente
    {
        #region "propiedades"
            public int      _idContinente;
            public string   _continente;
        #endregion
        #region "constructores"
            public Continente(int idContinente,string continente)
            {
                
                this._idContinente = idContinente;
                this._continente = continente;

            }
            public Continente(int idContinente)
            {
                this._idContinente = idContinente;
            }
        #endregion
    }
}
