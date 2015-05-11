using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class Pagina
    {
        #region "propiedades"
            //idPagina,pagina,estado
            public int      _idPagina;
            public string   _pagina;
            public bool     _estado;
        #endregion
        #region "constructores"
            public Pagina(int idPagina,string pagina,bool estado)
            {
                this._idPagina  = idPagina;
                this._pagina    = pagina;
                this._estado    = estado;
            }
            public Pagina(int idPagina)
            {
                this._idPagina  = idPagina;

            }
        #endregion
    }
}
