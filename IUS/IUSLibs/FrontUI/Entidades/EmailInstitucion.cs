using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class EmailInstitucion
    {
        #region "propiedades"
            public int          _idEmailInstitucion;
            public Institucion  _institucion;
            public string       _email;
        #endregion
        #region "constructores"
            // full atributos
                public EmailInstitucion(int idEmailInstitucion,int idInstitucion,string email)
                {
                    this._idEmailInstitucion    = idEmailInstitucion;
                    this._institucion           = new Institucion(idInstitucion);
                    this._email                 = email;
                }
            // para agregar
                public EmailInstitucion(string email,int idInstitucion)
                {
                    this._email = email;
                    this._institucion = new Institucion(idInstitucion);
                }
            // basico 
                public EmailInstitucion(int idEmailInstitucion)
                {
                    this._idEmailInstitucion = idEmailInstitucion;
                }
        #endregion
    }
}
