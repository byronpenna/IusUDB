﻿using System;
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
            public FormacionPersona(int idFormacionPersona)
            {
                this._idFormacionPersona = idFormacionPersona;
            }
            // full atributos
                public FormacionPersona(int idFormacionPersona,int yearInicio,int yearFin,string observaciones,int idPersona,int idCarrera,int idEstadoCarrera)
                {
                    // creacion de objetos
                        Persona persona             = new Persona(idPersona);
                        Carrera carrera             = new Carrera(idCarrera);
                        EstadoCarrera estadoCarrera = new EstadoCarrera(idEstadoCarrera);
                    // asignacion
                        this._idFormacionPersona    = idFormacionPersona;
                        this._yearInicio            = yearInicio;
                        this._yearFin               = yearFin;
                        this._observaciones         = observaciones;
                        this._persona               = persona;
                        this._carrera               = carrera;
                        this._estado                = estadoCarrera;
                }
            // para agregar 
                public FormacionPersona(int yearInicio, int yearFin, string observaciones, int idPersona, int idCarrera, int idEstadoCarrera)
                {
                    // creacion de objetos
                        Persona persona = new Persona(idPersona);
                        Carrera carrera = new Carrera(idCarrera);
                        EstadoCarrera estadoCarrera = new EstadoCarrera(idEstadoCarrera);
                    // asignacion
                        this._yearInicio = yearInicio;
                        this._yearFin = yearFin;
                        this._observaciones = observaciones;
                        this._persona = persona;
                        this._carrera = carrera;
                        this._estado = estadoCarrera;
                }
        #endregion
    }
}
