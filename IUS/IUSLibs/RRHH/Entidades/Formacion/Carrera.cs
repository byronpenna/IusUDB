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
            public AreaCarrera          _area;
        #endregion
        #region "constructores"
            public Carrera(int idCarrera)
            {
                this._idCarrera = idCarrera;
            }
            public Carrera(int idCarrera,string carrera,int idNivelTitulo,int idInstitucion,int idArea)
            {
                InstitucionEducativa    institucion = new InstitucionEducativa(idInstitucion);
                NivelTitulo             nivelTitulo = new NivelTitulo(idNivelTitulo);
                AreaCarrera             area        = new AreaCarrera(idArea);
                this._idCarrera     = idCarrera;
                this._carrera       = carrera; 
                this._nivelTitulo   = nivelTitulo;
                this._institucion   = institucion;
                this._area          = area;
            }
            // para agregar 
                public Carrera(string carrera, int idNivelTitulo, int idInstitucion,int idArea)
                {
                    NivelTitulo             nivelTitulo = new NivelTitulo(idNivelTitulo);
                    InstitucionEducativa    institucion = new InstitucionEducativa(idInstitucion);
                    AreaCarrera             area        = new AreaCarrera(idArea);
                    this._carrera       = carrera;
                    this._nivelTitulo   = nivelTitulo;
                    this._institucion   = institucion;
                    this._area          = area;
                }
        #endregion
    }
}
