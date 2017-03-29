using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias 
    using IUSLibs.RRHH.Entidades;
namespace IUSLibs.SEC.Entidades
{
    public class Persona
    {
        #region "propiedades"
            public int _idPersona;
            public String _nombres;
            public String _apellidos;
            public DateTime _fechaNacimiento; // por alguna extraña razon no hay solo date
            public Sexo _sexo;

            #region "calculados"
                public String nombreCompleto
                {
                    get
                    {
                        return this._nombres + " " + this._apellidos;
                    }
                }
                public String getFechaNac
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy}", this._fechaNacimiento);
                    }
                }
                public int getEdad { 
                    get{
                        //return Convert.ToInt32(DateTime.Now - this._fechaNacimiento);
                        int diff = DateTime.Now.Year - this._fechaNacimiento.Year;
                        return diff;
                    }
                }
            #endregion
            // fuera de tabla 
                public InformacionPersona _adicionales;
                public List<EmailPersona> emailsContacto;
        #endregion
        #region "constructores"
            public Persona(int idPersona, String nombres, String apellidos)
            {
                this._idPersona = idPersona;
                this._nombres = nombres;
                this._apellidos = apellidos;
            }
            public Persona(int idPersona,String nombres,String apellidos,DateTime fechaNac)
            {
                this._idPersona = idPersona;
                this._nombres = nombres;
                this._apellidos = apellidos;
                this._fechaNacimiento = fechaNac;
            }
            public Persona(int idPersona)
            {
                this._idPersona = idPersona;
            }
            // creado para poder agregar
            public Persona(string nombres, string apellidos, DateTime fechaNac)
            {
                this._nombres = nombres;
                this._apellidos = apellidos;
                this._fechaNacimiento = fechaNac;
            }
            public Persona()
            {

            }
        #endregion
            
    }
}
