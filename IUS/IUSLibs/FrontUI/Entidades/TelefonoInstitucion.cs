using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class TelefonoInstitucion
    {
        #region "propiedades"
            public int          _idTelefono;
            public string       _telefono;
            public string       _textoTelefono;
            public Institucion  _institucion;
        #endregion
        #region "constructores"
            public TelefonoInstitucion(int idTelefono, string telefono, string textoTelefono, int idInstitucion)
            {
                this._idTelefono = idTelefono;
                this._telefono = telefono;
                this._textoTelefono = textoTelefono;
                Institucion institucion = new Institucion(idInstitucion);
                this._institucion = institucion;
            }
            public TelefonoInstitucion(int idTelefono,string telefono,string textoTelefono,Institucion institucion)
            {
                this._idTelefono    = idTelefono;
                this._telefono      = telefono;
                this._textoTelefono = textoTelefono;
                this._institucion   = institucion;
            }
            // para agregar
            public TelefonoInstitucion(string telefono, string textoTelefono, int idInstitucion)
            {
                this._telefono          = telefono;
                this._textoTelefono     = textoTelefono;
                Institucion institucion = new Institucion(idInstitucion);
                this._institucion       = institucion;
            }
        #endregion
    }
}
