using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.RRHH.Entidades.Laboral
{
    public class LaboralPersona
    {
        #region "Propiedades"
            public int              _idLaboralPersona;
            //public Empresa          _empresa;
            public Institucion      _institucion; // institucion en la que laboro la persona
            public int              _inicio;
            public int              _fin;
            public Persona          _persona;
            /*public string           _observaciones;*/
            public CargoEmpresa     _cargo;
        #endregion
        #region "Constructores"
            // basico
                public LaboralPersona(int idLaboralPersona)
                {
                    this._idLaboralPersona = idLaboralPersona;
                }
            // full
                public LaboralPersona(int idLaboralPersona,int idInstitucion,int inicio,int fin,int idPersona,/*string observaciones*/ int idCargo)
                {
                    // do 
                        //Empresa empresa     = new Empresa(idEmpresa);
                        Institucion institucion = new Institucion(idInstitucion);
                        Persona persona         = new Persona(idPersona);
                        CargoEmpresa cargo      = new CargoEmpresa(idCargo);
                    // set 
                        this._idLaboralPersona  = idLaboralPersona;
                        //this._empresa           = empresa;
                        this._institucion       = institucion;
                        this._inicio            = inicio;
                        this._fin               = fin;
                        this._persona           = persona;
                        /*this._observaciones     = observaciones;*/
                        this._cargo             = cargo;
                }
            
            // Pata agregar
                public LaboralPersona(int idInstitucion, int inicio, int fin, int idPersona, /*string observaciones*/ int idCargo)
                {
                    // do 
                    //Empresa empresa = new Empresa(idEmpresa);
                    Institucion institucion = new Institucion(idInstitucion);
                    Persona persona = new Persona(idPersona);
                    CargoEmpresa cargo = new CargoEmpresa(idCargo);
                    // set 
                    //this._empresa = empresa;
                    this._institucion = institucion;
                    this._inicio = inicio;
                    this._fin = fin;
                    this._persona = persona;
                    /*this._observaciones = observaciones;*/
                    this._cargo = cargo;
                }
        #endregion
    }
}
