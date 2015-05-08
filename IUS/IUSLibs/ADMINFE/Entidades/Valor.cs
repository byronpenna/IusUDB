using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class Valor
    {
        #region "propiedades"
            public int              _idValor;
            public string           _valor;
            public Configuracion    _configuracion;
        #endregion
        #region "constructores"
            public Valor(string valor)
            {
                this._valor = valor;
            }
            public Valor(int idValor,string valor,int idConfiguracion)
            {
                this._idValor = idValor;
                this._valor = valor;
                Configuracion config = new Configuracion(idValor);
                this._configuracion = config;
            }
        #endregion
    }
}
