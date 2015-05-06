using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.TRL.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class Configuracion
    {
        #region "propiedades"
            public int      _idConfiguracion;
            public Idioma   _idioma;
            public string   _vision;
            public string   _mision;
            public string   _historia;
            // raiz y estado no se sintieron necesarias por lo que no se pusieron
        #endregion
        #region "constructores"
            public Configuracion(int idConfiguracion)
            {
                this._idConfiguracion = idConfiguracion;
            }
            public Configuracion(int idConfiguracion,Idioma idioma,string vision,string mision,string historia)
            {
                this._idConfiguracion   = idConfiguracion;
                this._idioma            = idioma;
                this._vision            = vision;
                this._mision            = mision;
                this._historia          = historia;
            }
            public Configuracion(int idConfiguracion, int idIdioma, string vision, string mision, string historia)
            {
                this._idConfiguracion = idConfiguracion;
                Idioma idioma = new Idioma(idIdioma);
                this._idioma = idioma;
                this._vision = vision;
                this._mision = mision;
                this._historia = historia;
            }
            // para agregar
            public Configuracion(string vision,string mision,string historia)
            {
                this._vision = vision;
                this._mision = mision;
                this._historia = historia;
            }
        #endregion
    }
}
