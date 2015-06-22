using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.REPO.Entidades
{
    public class ExtensionArchivo
    {
        #region "propiedades"
            public int _idExtension;
            public string _extension;
            public TipoArchivo _tipoArchivo; 
        #endregion
        #region "constructores"
            public ExtensionArchivo(int idExtension)
            {
                this._idExtension = idExtension;
            }
            public ExtensionArchivo(string extension)
            {
                this._extension = extension;
            }
            public ExtensionArchivo(int idExtension,string extension, TipoArchivo tipoArchivo)
            {
                this._idExtension = idExtension;
                this._extension = extension;
                this._tipoArchivo = tipoArchivo;
            }
            public ExtensionArchivo(int idExtension,TipoArchivo tipoArchivo)
            {
                this._idExtension = idExtension;
                this._tipoArchivo = tipoArchivo;
            }
        #endregion
    }
}
