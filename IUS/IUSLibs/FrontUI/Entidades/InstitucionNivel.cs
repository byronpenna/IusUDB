using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.FrontUI.Entidades
{
    public class InstitucionNivel
    {
        #region "propiedades"
            public int              _idInstitucionNivel;
            public Institucion      _institucion;
            public NivelEducacion   _nivelEducacion;
            public int              _numAlumnos;
        #endregion
        #region "constructores"
            // full atributos
                public InstitucionNivel(int idInstitucionNivel,int idInstitucion,int idNivelEducacion,int numAlumnos)
                {
                    this._idInstitucionNivel = idInstitucionNivel;
                    this._institucion = new Institucion(idInstitucion);
                    this._nivelEducacion = new NivelEducacion(idNivelEducacion);
                    this._numAlumnos = numAlumnos;
                }
            // basico
                public InstitucionNivel(int idInstitucionNivel)
                {
                    this._idInstitucionNivel = idInstitucionNivel;
                }
            // para agregar
            
        #endregion
    }
}
