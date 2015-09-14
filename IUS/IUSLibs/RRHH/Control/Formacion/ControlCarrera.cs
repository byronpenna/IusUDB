using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    //-------------------
        using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlCarrera:PadreLib
    {
        #region "funciones"
            #region "do"
                public Carrera sp_rrhh_ingresarCarrera(Carrera carreraIngresar,int idUsuarioEjecutor,int idPagina)
                {
                    Carrera carreraIngresada = null;
                    SPIUS sp = new SPIUS("sp_rrhh_ingresarCarrera");
                    
                    sp.agregarParametro("carrera", carreraIngresar._carrera);
                    sp.agregarParametro("idNivel", carreraIngresar._nivelTitulo._idNivel);
                    sp.agregarParametro("idInstitucion", carreraIngresar._institucion._idInstitucion);
                    
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                carreraIngresada = new Carrera((int)row["idCarrera"], row["carrera"].ToString(), (int)row["id_nivel_fk"], (int)row["id_institucion_fk"]);

                            }
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return carreraIngresada;
                }
            #endregion
            #region "get"
            #endregion
        #endregion
    }
}
