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
    using IUSLibs.SEC.Entidades;
    using IUSLibs.ADMINFE.Entidades;
namespace IUSLibs.FrontUI.Eventos.Control
{
    public class ControlEventos:PadreLib
    {
        #region "get"
            public List<Evento> sp_adminfe_front_getMonthEvents(string ip,int idPagina)
            {
                List<Evento> eventos = null;
                Evento evento;
                SPIUS sp = new SPIUS("sp_adminfe_front_getMonthEvents");
                /*
                    @			varchar(50),
		            @	int
                 */
                sp.agregarParametro("ip", ip);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            eventos = new List<Evento>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                Usuario usuario = new Usuario((int)row["id_usuario_creador_fk"]);
                                evento = new Evento((int)row["idEvento"], row["evento"].ToString(), (DateTime)row["fecha_inicio"], (DateTime)row["fecha_fin"], usuario, (DateTime)row["fecha_creacion"], row["descripcion"].ToString());
                                eventos.Add(evento);
                            }
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
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
            
            public List<Evento> sp_adminfe_front_getTodayEvents(string ip,int idPagina)
            {
                List<Evento> eventos = null;
                SPIUS sp = new SPIUS("sp_adminfe_front_getTodayEvents");
                Evento evento;
                sp.agregarParametro("ip", ip);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            eventos = new List<Evento>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                Usuario usuario = new Usuario((int)row["id_usuario_creador_fk"]);
                                evento = new Evento((int)row["idEvento"], row["evento"].ToString(), (DateTime)row["fecha_inicio"], (DateTime)row["fecha_fin"], usuario,(DateTime)row["fecha_creacion"] ,row["descripcion"].ToString());
                                eventos.Add(evento);
                            }
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x = this.getErrorFromExecProcedure(row);
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
    }
}
