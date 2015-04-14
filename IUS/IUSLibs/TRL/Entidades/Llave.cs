using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.TRL.Entidades
{
    public class Llave
    {
        #region propiedades
            public int _idLlave;
            public string _llave;
            public Pagina _pagina;
        #endregion
        #region "constructores"
            public Llave(int idLlave,string llave,Pagina pagina)
            {
                this._idLlave = idLlave;
                this._llave = llave;
                this._pagina = pagina;
            }
            public Llave()
            {

            }
        #endregion
    }
}
