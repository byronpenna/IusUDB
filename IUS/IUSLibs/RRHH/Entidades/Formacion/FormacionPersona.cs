using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class FormacionPersona
    {
        #region "propiedades"
            public int              _idFormacionPersona;
            public int              _yearInicio;
            public int              _yearFin;
            public string           _observaciones;
            public Persona          _persona;
            public Carrera          _carrera;
            public EstadoCarrera    _estado;
        #endregion
        #region "Constructores"
            
        #endregion
    }
}
