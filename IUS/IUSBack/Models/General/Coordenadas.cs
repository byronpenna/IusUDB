using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUSBack.Models.General
{
    public class Coordenadas
    {
        #region "propiedades"
            public decimal _x;
            public decimal _y;
            public decimal _ancho;
            public decimal _alto;
        #endregion
        #region "constructores"
            public Coordenadas(decimal x=0,decimal y=0,decimal ancho=0,decimal alto=0)
            {
                this._x = x; this._y = y; this._ancho = ancho; this._alto = alto;
            }
        #endregion
    }
}