using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.TRL.Entidades
{
    public class Pagina
    {
        #region "propiedades"
            public int _idPagina;
            public string _pagina;
            public bool _estado;
        #endregion 
        #region "constructores"
            public Pagina()
            {

            }
            public Pagina(int idPagina,string pagina,bool estado)
            {
                this._idPagina = idPagina;
                this._pagina = pagina;
                this._estado = estado;
            }
        #endregion
        
    }
}
