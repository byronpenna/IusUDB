using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.RRHH.Entidades.Formacion
{
    public class FormacionPersona
    {
        #region "propiedades"
            public int              _idFormacionPersona;
            //public int              _yearInicio;
            public int              _yearFin;
            public string           _observaciones;
            public Persona          _persona;
            public string           _carrera;
            public EstadoCarrera    _estado;
            public NivelTitulo      _nivelTitulo;
            public AreaCarrera      _areaCarrera;
            public string           _institucion;
            public Pais             _paisInstitucion;
        #endregion
        #region "Constructores"
            public FormacionPersona(int idFormacionPersona)
            {
                this._idFormacionPersona = idFormacionPersona;
            }
            // full atributos
                public FormacionPersona(    int     idFormacionPersona, int     yearFin,
                                            string  observaciones,      int     idPersona,
                                            int     idEstadoCarrera,    string  carrera,            
                                            int     idNivelTitulo,      int     idAreaCarrera,
                                            string  institucion,        int     idPais
                                        )
                {
                    // creacion de objetos
                        Persona         persona         = new Persona(idPersona);
                        //Carrera carrera               = new Carrera(idCarrera);
                        NivelTitulo     nivelTitulo     = new NivelTitulo(idNivelTitulo);
                        AreaCarrera     areaCarrera     = new AreaCarrera(idAreaCarrera);
                        EstadoCarrera   estadoCarrera   = new EstadoCarrera(idEstadoCarrera);
                        Pais            paisInstitucion = new Pais(idPais);
                    // asignacion
                        this._idFormacionPersona    = idFormacionPersona;
                        //this._yearInicio            = yearInicio;
                        this._yearFin               = yearFin;
                        this._observaciones         = observaciones;
                        this._persona               = persona;
                        this._carrera               = carrera;
                        this._estado                = estadoCarrera;
                        this._areaCarrera           = areaCarrera;
                        this._nivelTitulo           = nivelTitulo;
                        this._estado                = estadoCarrera;
                        this._institucion           = institucion;
                        this._paisInstitucion       = paisInstitucion;
                }
            // para agregar 
                public FormacionPersona(    int     yearFin,
                                            string  observaciones,      int     idPersona,
                                            int     idEstadoCarrera,    string  carrera,
                                            int     idNivelTitulo,      int     idAreaCarrera,
                                            string  institucion,        int     idPais
                                        )
                {
                    // creacion de objetos
                    Persona         persona         = new Persona(idPersona);
                    //Carrera carrera               = new Carrera(idCarrera);
                    NivelTitulo     nivelTitulo     = new NivelTitulo(idNivelTitulo);
                    AreaCarrera     areaCarrera     = new AreaCarrera(idAreaCarrera);
                    EstadoCarrera   estadoCarrera   = new EstadoCarrera(idEstadoCarrera);
                    Pais            paisInstitucion = new Pais(idPais);
                    // asignacion
                    this._yearFin           = yearFin;
                    this._observaciones     = observaciones;
                    this._persona           = persona;
                    this._carrera           = carrera;
                    this._estado            = estadoCarrera;
                    this._areaCarrera       = areaCarrera;
                    this._estado            = estadoCarrera;
                    this._nivelTitulo       = nivelTitulo;
                    this._institucion       = institucion;
                    this._paisInstitucion   = paisInstitucion;
                }
        #endregion
    }
}
