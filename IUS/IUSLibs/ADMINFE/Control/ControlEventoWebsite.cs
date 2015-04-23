using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;    
    using System.Data;
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
            public EventoWebsite sp_adminfe_quitarEventoWebsite(int idEventoQuitar,string motivo,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    Evento eventoQuitar = new Evento(idEventoQuitar);
                    return this.sp_adminfe_publicarOquitarEventoWebsite(eventoQuitar, idUsuarioEjecutor, idPagina, motivo);
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
            public EventoWebsite sp_adminfe_publicarEventoWebsite(Evento eventoAgregar, int idUsuarioEjecutor, int idPagina){
                try
                {
                    return this.sp_adminfe_publicarOquitarEventoWebsite(eventoAgregar, idUsuarioEjecutor, idPagina, "");
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
            public EventoWebsite sp_adminfe_publicarOquitarEventoWebsite(Evento eventoAgregar, int idUsuarioEjecutor, int idPagina,string txtMotivoQuitar)
            {
                EventoWebsite eventoPublicado = null; Evento evento;
                Usuario usu = null;
                SPIUS sp = new SPIUS("sp_adminfe_publicarOquitarEventoWebsite");
                sp.agregarParametro("idEvento", eventoAgregar._idEvento);
                sp.agregarParametro("txtMotivo", txtMotivoQuitar);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina",idPagina);
                try
                {

                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    DataRow rowResultado = tb[1].Rows[0];
                    if (this.resultadoCorrecto(tb))
                    {
                        evento = new Evento((int)rowResultado["id_evento_fk"]);
                        evento._publicado = (bool)rowResultado["estado"];
                        usu = new Usuario((int)rowResultado["usuario_publicacion_fk"]);
                        eventoPublicado = new EventoWebsite((int)rowResultado["idEventoWeb"],(DateTime)rowResultado["fecha_publicacion"], usu, evento, evento._publicado);
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
