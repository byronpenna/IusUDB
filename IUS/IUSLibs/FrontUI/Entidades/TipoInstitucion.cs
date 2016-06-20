using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class TipoInstitucion
    {
        #region "propiedades"
            public int      _idTipoInstitucion;
            public string   _tipoInstitucion;
        #endregion
        #region "constructores"
            // basico
                public TipoInstitucion(int idTipoInstitucion)
                {
                    this._idTipoInstitucion = idTipoInstitucion;
                }
            // full atributo
                public TipoInstitucion(int idTipoInstitucion,string tipoInstitucion){
                    this._idTipoInstitucion = idTipoInstitucion;
                    this._tipoInstitucion   = tipoInstitucion;
                }
        #endregion
    }
}
