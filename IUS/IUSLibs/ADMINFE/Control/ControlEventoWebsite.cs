using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.LOGS;
    using IUSLibs.GENERALS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control
{
    public class ControlEventoWebsite:PadreLib
    {
        #region "propiedades"

        #endregion
        #region "funciones"
            public EventoWebsite sp_adminfe_publicarEventoWebsite(Evento eventoAgregar,int idUsuarioEjecutor,int idPagina)
            {
                EventoWebsite eventoPublicado = null; Evento evento;
                Usuario usu = null;
                SPIUS sp = new SPIUS("sp_adminfe_publicarEventoWebsite");
                sp.agregarParametro("idEvento", eventoAgregar._idEvento);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina",idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    DataRow rowResultado = tb[1].Rows[0];
                    if (this.resultadoCorrecto(tb))
                    {
                        evento = new Evento((int)rowResultado["id_evento_fk"]);
                        usu = new Usuario((int)rowResultado["usuario_publicacion_fk"]);
                        eventoPublicado = new EventoWebsite((int)rowResultado["idEventoWeb"],(DateTime)rowResultado["fecha_publicacion"], usu, evento, (bool)rowResultado["estado"]);
                    }
                    else
                    {
                        ErroresIUS x = new ErroresIUS(rowResultado["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowResultado["errorCode"]);
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
                return eventoPublicado;
            }
        #endregion
    }
}
