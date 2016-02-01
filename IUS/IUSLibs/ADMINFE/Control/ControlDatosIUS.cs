using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas 
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;

namespace IUSLibs.ADMINFE.Control
{
    public class ControlDatosIUS:PadreLib
    {
        #region "propiedades"
            
        #endregion
        #region "constructores"
            public ControlDatosIUS()
            {

            }
        #endregion
        #region "funciones"
            public DatosIUS sp_adminfe_front_getDatosIUS(string ip, int idPagina)
            {
                try
                {
                    DatosIUS retorno = null;
                    SPIUS sp = new SPIUS("sp_adminfe_front_getDatosIUS");
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            retorno = new DatosIUS((int)row["idDatoIuss"],(int)row["continentes_presentes"],(int)row["paises_presentes"],(int)row["total_instituciones"],(int)row["total_estudiantes"],(int)row["total_salesianos"]);

                        }
                    }
                    return retorno;
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        #endregion
    }
}
