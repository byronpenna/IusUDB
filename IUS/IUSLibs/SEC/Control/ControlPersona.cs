using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
using IUSLibs.GENERALS;
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.SEC.Control
{
    public class ControlPersona:PadreLib
    {
        public List<Persona> getPersonas()
        {
            List<Persona> personas = new List<Persona>();
            Persona persona;
            SPIUS sp = new SPIUS("sp_sec_getPersonas");
            DataSet ds = sp.EjecutarProcedimiento();
            if (!this.DataSetDontHaveTable(ds))
            {
                DataTable table = ds.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    persona = new Persona((int)row["idPersona"], row["nombres"].ToString(), row["apellidos"].ToString(), (DateTime)row["fecha_nacimiento"]);
                    personas.Add(persona);
                }
            }
            return personas;
        }
    }
}
