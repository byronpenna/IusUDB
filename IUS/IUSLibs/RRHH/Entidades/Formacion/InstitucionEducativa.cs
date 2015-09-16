using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class InstitucionEducativa
    {
        #region "propiedades"
            public int      _idInstitucion;
            public string   _nombre;
            public Pais     _pais;
        #endregion
        #region "constructores"
            public InstitucionEducativa(int idInstitucion)
            {
                this._idInstitucion = idInstitucion;
            }
            public InstitucionEducativa(int idInstitucion,string nombre,int idPais)
            {
                Pais pais = new Pais(idPais);
                this._nombre        = nombre;
                this._idInstitucion = idInstitucion;
                this._pais          = pais;
            }
            // para agregar
            public InstitucionEducativa(string nombre, int idPais)
            {
                Pais pais = new Pais(idPais);
                this._nombre = nombre;
                this._pais = pais;
            }
        #endregion
    }
}
