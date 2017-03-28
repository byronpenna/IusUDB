using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// librerias
    using IUSLibs.TRL.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class VersionDocumentoOficial
    {
        #region "propiedades"
            public int              _idVersion;
            public string           _traduccion;
            public DocumentoOficial _documento;
            public string           _ruta;
            public Idioma           _idioma;

        #endregion
        #region "constructores"
            public VersionDocumentoOficial(int idVersion)
            {
                this._idVersion = idVersion;
            }
            
        #endregion
        //idVersion,traduccion,id_documento_fk,ruta,id_idioma_fk
    }
}
