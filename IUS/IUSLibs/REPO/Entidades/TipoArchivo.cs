using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.REPO.Entidades
{
    public class TipoArchivo
    {
        #region "propiedades"
            public int _idTipoArchivo;
            public string _tipoArchivo;
            public string _icono;
        #endregion
        #region "constructores"
            public TipoArchivo(int _idTipoArchivo)
            {
                this._idTipoArchivo = _idTipoArchivo;
            }
            public TipoArchivo(int _idTipoArchivo,string tipoArchivo)
            {
                this._idTipoArchivo = _idTipoArchivo;
                this._tipoArchivo = tipoArchivo;
            }
            public TipoArchivo(int _idTipoArchivo,string tipoArchivo,string icono)
            {
                this._idTipoArchivo = _idTipoArchivo;
                this._tipoArchivo = tipoArchivo;
                this._icono = icono;
            }
        #endregion
    }
}
