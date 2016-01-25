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
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control
{
    public class ControlEvento:PadreLib
    {
        #region "propiedades"

        #endregion 
        #region "funciones"
            #region "gets"
                public int sp_adminfe_countTodayEvents(int idPagina,int idUsuarioEjecutor)
                {
                    int cnEventos = 0;
                    try
                    {
                        SPIUS sp = new SPIUS("sp_adminfe_countTodayEvents");
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[0].Rows[0];
                                cnEventos   = (int)row["eventosHoy"];
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
                    return cnEventos;
                }
                public List<Evento> sp_adminfe_buscarAllEventosPersonalesByDate(DateTime fechaInicio,DateTime fechaFin,int idUsuarioEjecutor,int idPagina)
                {
                    List<Evento> eventos = null;
                    SPIUS sp = new SPIUS("sp_adminfe_buscarAllEventosPersonalesByDate");
                    sp.agregarParametro("fechaInicio", fechaInicio);
                    sp.agregarParametro("fechaFin", fechaFin);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    Usuario usu; Evento evento;
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                eventos = new List<Evento>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    usu = new Usuario((int)row["id_usuario_creador_fk"]);
                                    evento = new Evento((int)row["idEvento"], row["evento"].ToString(), (DateTime)row["fecha_inicio"], (DateTime)row["fecha_fin"], usu, (DateTime)row["fecha_creacion"], row["descripcion"].ToString());
                                    evento._publicado = (bool)row["publicado"];
                                    evento._propietario = (int)row["propietario"];
                                    eventos.Add(evento);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
                        }

                    }catch(ErroresIUS x){
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return eventos;
                }
                public List<Evento> sp_adminfe_eventosCalendario(int idUsuario, int idPagina)
                {
                    List<Evento> eventos = null;
                    Evento evento; Usuario usu;
                    SPIUS sp = new SPIUS("sp_adminfe_eventosCalendario");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            eventos = new List<Evento>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                usu = new Usuario((int)row["id_usuario_creador_fk"]);
                                evento = new Evento((int)row["idEvento"], row["evento"].ToString(), (DateTime)row["fecha_inicio"], (DateTime)row["fecha_fin"], usu, (DateTime)row["fecha_creacion"], row["descripcion"].ToString());
                                evento._publicado = (bool)row["publicado"];
                                evento._propietario = (int)row["propietario"];
                                eventos.Add(evento);
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
                    return eventos;
                }
            #endregion
            #region "acciones"
                public bool sp_adminfe_eliminarEvento(int idEvento, int idUsuarioEjecutor, int idPagina)
                {
                    bool eliminado = false;
                    SPIUS sp = new SPIUS("sp_adminfe_eliminarEvento");
                    sp.agregarParametro("idEvento", idEvento);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            eliminado = true;
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
                    return eliminado;
                }
                public Evento sp_adminfe_editarEventos(Evento eventoEditar, int idUsuario, int idPagina)
                {
                    Evento eventoEditado;
                    SPIUS sp = new SPIUS("sp_adminfe_editarEventos");
                    sp.agregarParametro("idEvento", eventoEditar._idEvento);
                    sp.agregarParametro("evento", eventoEditar._evento);
                    sp.agregarParametro("fechaInicio", eventoEditar._fechaInicio);
                    sp.agregarParametro("fechaFin", eventoEditar._fechaFin);
                    sp.agregarParametro("decripcion", eventoEditar._descripcion.Replace("\n","<br>"));
                    sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            Usuario usu = new Usuario((int)rowResultado["id_usuario_creador_fk"]);
                            eventoEditado = new Evento((int)rowResultado["idEvento"], rowResultado["evento"].ToString(),(DateTime)rowResultado["fecha_inicio"],(DateTime)rowResultado["fecha_fin"],usu,(DateTime)rowResultado["fecha_creacion"],rowResultado["descripcion"].ToString());
                        }
                        else
                        {
                            DataRow rowError = tb[0].Rows[0];
                            ErroresIUS x = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"], rowError["errorSql"].ToString());
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
                    return eventoEditado;
                }
                public Evento sp_adminfe_crearEvento(Evento eventoAgregar,int idUsuario,int idPagina)
                {
                    Evento eventoAgregado = null;
                    Usuario usu;
                    
                    SPIUS sp = new SPIUS("sp_adminfe_crearEvento");
                    sp.agregarParametro("evento", eventoAgregar._evento);
                    sp.agregarParametro("fecha_inicio", eventoAgregar._fechaInicio);
                    sp.agregarParametro("fecha_fin", eventoAgregar._fechaFin);
                    sp.agregarParametro("descripcion", eventoAgregar._descripcion);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if(this.resultadoCorrecto(tb)){
                            DataRow rowResultado = tb[1].Rows[0];
                            usu = new Usuario((int) rowResultado["id_usuario_creador_fk"]);
                            eventoAgregado = new Evento((int)rowResultado["idEvento"], rowResultado["evento"].ToString(), (DateTime)rowResultado["fecha_inicio"], (DateTime)rowResultado["fecha_fin"],usu, (DateTime)rowResultado["fecha_creacion"], rowResultado["descripcion"].ToString());
                            eventoAgregado._publicado = Convert.ToBoolean((int)rowResultado["publicado"]);
                            eventoAgregado._propietario = (int)rowResultado["propietario"];
                        }
                        else
                        {
                            DataRow rowResultado = tb[0].Rows[0];
                            ErroresIUS x = new ErroresIUS(rowResultado["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowResultado["errorCode"], rowResultado["errorSql"].ToString(),true);
                            throw x;
                        }
                    }catch(ErroresIUS x){
                        throw x;
                    }catch(Exception x){
                        throw x;
                    }
                    return eventoAgregado;
                }
            #endregion
        #endregion
    }
}
