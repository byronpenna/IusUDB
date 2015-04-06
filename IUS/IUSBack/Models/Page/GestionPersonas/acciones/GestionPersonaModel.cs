using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias externas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
namespace IUSBack.Models.Page.GestionPersonas.acciones
{
    public class GestionPersonaModel
    {
        public List<Persona> getPersonas()
        {
            ControlPersona control = new ControlPersona();
            List<Persona> personas = control.getPersonas();
            if (personas.Count != 0)
            {
                return personas;
            }
            else
            {
                return null;
            }
            
        }

    }
}