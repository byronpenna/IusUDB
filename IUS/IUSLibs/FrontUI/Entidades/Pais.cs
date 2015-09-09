using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class Pais
    {
        #region "propiedades"
            public int _idPais;
            public string _pais;
            public Continente _continente;
        #endregion
        #region "constructores"
            public Pais(int idPais)
            {
                this._idPais = idPais;
            }
            public Pais(int idPais,string pais)
            {
                this._idPais = idPais;
                this._pais = pais;
            }    
            public Pais(int idPais, string pais, int idContinente)
            {
                this._idPais = idPais;
                this._pais = pais;
                Continente continente = new Continente(idContinente);
                this._continente = continente;
            }
            public Pais(int idPais,string pais,Continente continente)
            {
                this._idPais = idPais;
                this._pais = pais;
                this._continente = continente;
            }
            public Pais(string pais)
            {
                this._pais = pais;
            }
        #endregion
    }
}
