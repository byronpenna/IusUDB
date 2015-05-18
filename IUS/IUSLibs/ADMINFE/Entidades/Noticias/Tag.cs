using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class Tag
    {
        #region "propiedades"
            public int      _idTag;
            public string   _strTag;
        #endregion
        #region "contructores"
            public Tag(int idTag) {
                this._idTag = idTag;
            }
            public Tag(int idTag, string tag) {
                this._idTag = idTag;
                this._strTag   = tag;
            }
        #endregion
    }
}
