using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class DocumentoOficial
    {
        #region "propiedades"
            //idDocumentoOficial,nombre
            public int _idDocumentoOficial;
            public int _nombre;
        #endregion
        #region "constructores"
            public DocumentoOficial(int idDocumentoOficial)
            {
                this._idDocumentoOficial = idDocumentoOficial; 
            }
            public DocumentoOficial(int idDocumentoOficial,int nombre)
            {
                this._idDocumentoOficial = idDocumentoOficial;
                this._nombre = nombre;
            }
        #endregion
    }
}
