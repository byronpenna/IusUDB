﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.FrontUI.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.RRHH.Entidades
{
    public class InformacionPersona
    {
        #region "propiedades"
            public int          _idInformacionPersona;
            public Pais         _pais;
            public string       _numeroIdentificacion;
            public EstadoCivil  _estadoCivil;
            public Persona      _persona;
            public string       _fotoRuta;
            public string       _curriculumn;
            // adicionales
            public bool         _tieneFoto          = false;
            public bool         _tieneCurriculumn   = false;
        #endregion
            public void instanciaPais()
            {
                this._pais = new Pais(0);
            }
        #region "Constructores"
            public InformacionPersona()
            {

            }
            public InformacionPersona(int idInformacionPersona)
            {
                this._idInformacionPersona = idInformacionPersona;
            }
            public InformacionPersona(int idInformacionPersona,int idPais,string numeroIdentificacion,int idEstadoCivil,int idPersona,string foto)
            {
                this._idInformacionPersona  = idInformacionPersona;
                Pais pais                   = new Pais(idPais);
                this._pais                  = pais;
                this._numeroIdentificacion  = numeroIdentificacion;
                EstadoCivil estadoCivil     = new EstadoCivil(idEstadoCivil);
                this._estadoCivil           = estadoCivil;
                Persona persona             = new Persona(idPersona);
                this._persona               = persona;
                this._fotoRuta              = foto;
            }
            // para foto
                public InformacionPersona(int idPersona, string foto)
                {
                    Persona persona = new Persona(idPersona);
                    this._persona = persona;
                    this._fotoRuta = foto;
                }
            // para agregar
                public InformacionPersona(int idPais, string numeroIdentificacion, int idEstadoCivil, int idPersona)
                {
                    Pais pais = new Pais(idPais);
                    this._pais = pais;
                    this._numeroIdentificacion = numeroIdentificacion;
                    EstadoCivil estadoCivil = new EstadoCivil(idEstadoCivil);
                    this._estadoCivil = estadoCivil;
                    Persona persona = new Persona(idPersona);
                    this._persona = persona;
                }
        #endregion
    }
}
