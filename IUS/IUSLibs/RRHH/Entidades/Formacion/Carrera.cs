using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class Carrera
    {
        #region "propiedades"
            public int                  _idCarrera;
            public string               _carrera;
            public NivelTitulo          _nivelTitulo;
            public InstitucionEducativa _institucion;
            
        #endregion
        #region "constructores"
            public Carrera(int idCarrera)
            {
                this._idCarrera = idCarrera;
            }
            public Carrera(int idCarrera,string carrera,int idNivelTitulo,int idInstitucion)
            {
                this._idCarrera = idCarrera;
                this._carrera = carrera;
                NivelTitulo nivelTitulo = new NivelTitulo(idNivelTitulo);
                this._nivelTitulo = nivelTitulo;
                InstitucionEducativa institucion = new InstitucionEducativa(idInstitucion);
                this._institucion = institucion;
            }
            // para agregar 
                public Carrera(string carrera, int idNivelTitulo, int idInstitucion)
                {
                    this._carrera = carrera;
                    NivelTitulo nivelTitulo = new NivelTitulo(idNivelTitulo);
                    this._nivelTitulo = nivelTitulo;
                    InstitucionEducativa institucion = new InstitucionEducativa(idInstitucion);
                    this._institucion = institucion;
                }
        #endregion
    }
}
