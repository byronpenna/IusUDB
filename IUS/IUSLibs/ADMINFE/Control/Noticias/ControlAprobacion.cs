using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlAprobacion:PadreLib
    {
        //sp_adminfe_cambiarEstadoPublicacion
        #region "backend"
            public NotiEvento sp_adminfe_cambiarEstadoPublicacion(NotiEvento notiEventoToChange,int idUsuarioEjecutor, int idPagina)
            {
                NotiEvento noticiaEvento = null;
                SPIUS sp = new SPIUS("sp_adminfe_cambiarEstadoPublicacion");
                /*
                    @					int,
		            @				date,
		            @			int,
                 */
                sp.agregarParametro("tipo", notiEventoToChange._idTipoEntrada);
                sp.agregarParametro("caducidad", notiEventoToChange._fechaCaducidad);
                sp.agregarParametro("idPublicacion", notiEventoToChange._id);    

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
                            noticiaEvento = new NotiEvento((int)row["id"]);

                            noticiaEvento._fecha = (DateTime)row["fecha"];
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
                        throw x;
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
                return noticiaEvento;
            }
        #endregion
    }
}
