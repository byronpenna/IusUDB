using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.TRL.Entidades
{
    public class Idioma
    {
        #region "propiedades"
            public int _idIdioma;
            public string _idioma;
            public string _lang;
            public string _charset;
        #endregion 
        #region "constructores"
            // las locales comienzan con _
            public Idioma(int idIdioma)
            {
                this._idIdioma = idIdioma;
            }
            public Idioma(int idIdioma, string idioma)
            {
                this._idIdioma = idIdioma;
                this._idioma = idioma;
            }
            public Idioma(int idIdioma,string idioma,string lang,string charset)
            {
                this._idIdioma = idIdioma;
                this._idioma = idioma;
                this._lang = lang;
                this._charset = charset;
            }
            public Idioma()
            {

            }
        #endregion

    }
}
