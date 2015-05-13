using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class PostCategoria
    {
        #region "propiedades"
            public int      _idPostCategoria;
            public string   _categoria;
        #endregion
        #region "constructor"
            public PostCategoria(int idPostCategoria,string categoria)
            {
                this._idPostCategoria   = idPostCategoria;
                this._categoria         = categoria;
            }
        #endregion
    }
}
