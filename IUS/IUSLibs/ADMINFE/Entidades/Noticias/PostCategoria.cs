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
            /* Extras a entidad */
            public bool     _selected = false;
        #endregion
        #region "constructor"
            public PostCategoria(int idPostCategoria, string categoria,bool selected)
            {
                this._idPostCategoria   = idPostCategoria;
                this._categoria         = categoria;
                this._selected = selected;
            }
            public PostCategoria(int idPostCategoria,string categoria)
            {
                this._idPostCategoria   = idPostCategoria;
                this._categoria         = categoria;
            }
            public PostCategoria(int idPostCategoria)
            {
                this._idPostCategoria = idPostCategoria;
            }
        #endregion
    }
}
