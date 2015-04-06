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
    using IUSLibs.LOGS;
namespace IUSLibs.SEC.Control
{
    public class ControlPersona:PadreLib
    {
        public Persona actualizarPersona(Persona persona,int idUsuario,int idPagina)
        {
            Persona personaReturn = null;
            ErroresIUS errorIus; // manejo de errores
            SPIUS sp = new SPIUS("sp_hm_editarPersona");
            // para actualizar
                sp.agregarParametro("nombres", persona._nombres);
                sp.agregarParametro("apellidos", persona._apellidos);
                sp.agregarParametro("fecha", persona._fechaNacimiento);
                sp.agregarParametro("idPersona", persona._idPersona);
            // para permisos
                sp.agregarParametro("idUsuarioAccion", idUsuario);
                sp.agregarParametro("idPagina", idPagina);
            DataSet ds = sp.EjecutarProcedimiento();
            if (!this.DataSetDontHaveTable(ds))
            {
                DataTable table = ds.Tables[0];
                if ((int)table.Rows[0]["estadoUpdate"] == 1)
                {
                    if(ds.Tables.Count > 1 ){
                        DataRow rowResult = ds.Tables[1].Rows[0];
                        personaReturn = new Persona((int)rowResult["idPersona"], rowResult["nombres"].ToString(), rowResult["apellidos"].ToString(), (DateTime)rowResult["fecha_nacimiento"]);
                    }else{
                        errorIus = new ErroresIUS("Error no controlado sql", ErroresIUS.tipoError.sql, -2);
                        throw errorIus;
                    }
                }
                else
                {
                    if (ds.Tables.Count > 1)
                    {
                        DataRow rowError = ds.Tables[1].Rows[0];
                        errorIus = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"]);
                        throw errorIus;
                    }
                    else
                    {
                        errorIus = new ErroresIUS("Error no controlado sql", ErroresIUS.tipoError.sql, -2);
                        throw errorIus;
                    }
                    
                }
            }
            return personaReturn;
        }
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
