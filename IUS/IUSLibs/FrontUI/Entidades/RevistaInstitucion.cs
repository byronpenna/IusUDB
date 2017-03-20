using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class RevistaInstitucion
    {
        #region "propiedades"
            public int          _idRevistaInstitucion;
            public string       _revista;
            public string       _categoria;
            public int          _anioPublicacion;
            public Institucion  _institucion;
            //
            
        #endregion 
        #region "constructores"
            public RevistaInstitucion(int idRevistaInstitucion)
            {
                this._idRevistaInstitucion = idRevistaInstitucion;
            }
            public RevistaInstitucion(int idRevistaInstitucion, string revista, string categoria, int anioPublicacion)
            {
                this._idRevistaInstitucion = idRevistaInstitucion;
                this._revista = revista;
                this._categoria = categoria;
                this._anioPublicacion = anioPublicacion;
            }
        #endregion
    }
}
