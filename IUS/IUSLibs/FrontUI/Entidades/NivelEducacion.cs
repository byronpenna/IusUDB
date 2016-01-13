using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class NivelEducacion
    {
        #region "propiedades"
            public int      _idNivelEducacion;
            public string   _codigo;
            public string   _descripcion;
            // diferentes a tablas 
            public bool     _selected = false;
        #endregion
        #region "constructores"
            // Para agregar
                public NivelEducacion(string codigo, string descripcion)
                {
                    this._codigo = codigo;
                    this._descripcion = descripcion;
                }
            // full atributos
                public NivelEducacion(int idNivelEduacion,string codigo,string descripcion)
                {
                    this._idNivelEducacion  = idNivelEduacion;
                    this._codigo            = codigo;
                    this._descripcion       = descripcion;
                }
            /*public NivelEducacion()
            {

            }*/
        #endregion
    }
}
