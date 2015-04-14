using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.TRL.Entidades
{
    public class LlaveIdioma
    {
        #region 
            public int _idLlaveIdioma;
            public Idioma _idioma;
            public Llave _llave;
            public string _traduccion;
        #endregion
        #region "constructores"
            public LlaveIdioma(int idLlaveIdioma,Idioma idioma,Llave llave,string traduccion)
            {
                this._idLlaveIdioma = idLlaveIdioma;
                this._idioma = idioma;
                this._llave = llave;
                this._traduccion = traduccion;
            }
            public LlaveIdioma()
            {

            }
        #endregion
    }
}
