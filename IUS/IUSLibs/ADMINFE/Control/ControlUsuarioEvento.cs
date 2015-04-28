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
    public class ControlUsuarioEvento:PadreLib
    {
        #region "propiedades"

        #endregion
        #region "funciones"
            #region "acciones"
                public bool sp_adminfe_removeUsuarioEvento(int idUsuarioEvento,int idUsuarioEjecutor)
                {
                    bool estado=false;
                    SPIUS sp = new SPIUS("sp_adminfe_removeUsuarioEvento");
                    sp.agregarParametro("idUsuarioEvento", idUsuarioEvento);
                    sp.agregarParametro("idUsuarioEjecutor",idUsuarioEjecutor);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                public UsuarioEvento sp_adminfe_compartirEventoUsuario(UsuarioEvento agregar, int idUsuarioEjecutor, int idPagina)
                {
                    UsuarioEvento agregado = null;
                    Evento evento; Usuario usuario;
                    SPIUS sp = new SPIUS("sp_adminfe_compartirEventoUsuario");
                    sp.agregarParametro("idEvento", agregar._evento._idEvento);
                    sp.agregarParametro("idUsuario", agregar._usuario._idUsuario);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            usuario = new Usuario((int)rowResultado["idUsuario"], rowResultado["usuario"].ToString(), (bool)rowResultado["estado"]);
                            evento = new Evento((int)rowResultado["id_evento_fk"]);
                            agregado = new UsuarioEvento((int)rowResultado["idUsuarioEvento"],evento,usuario);
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
                    return agregado;
                }
            #endregion
            #region "gets"
                // carga solo los usuarios a los que se les compartio
                public Dictionary<string,object> sp_adminfe_loadCompartirEventos(int idEvento,int idUsuarioEjecutor,int idPagina)
                {
                    Dictionary<string,object> retorno = null;
                    List<UsuarioEvento> usuariosCompartido = null;
                    List<Usuario> usuariosNoCompartido = null;
                    Usuario usu; Persona persona; UsuarioEvento usuEvento; Evento evento;
                    SPIUS sp = new SPIUS("sp_adminfe_loadCompartirEventos");
                    sp.agregarParametro("idEvento", idEvento);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            
                            if (tb[1].Rows.Count > 0)
                            {
                                usuariosNoCompartido = new List<Usuario>();
                                // usuarios que no estan en el evento 
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    persona = new Persona((int)row["id_persona_fk"]);
                                    usu = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), persona,(bool) row["estado"]);
                                    usuariosNoCompartido.Add(usu);
                                }
                            }
                            if (tb[2].Rows.Count > 0) {
                                usuariosCompartido = new List<UsuarioEvento>();
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    persona = new Persona((int)row["id_persona_fk"]);
                                    usu = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), persona, (bool)row["estado"]);
                                    evento = new Evento((int)row["id_evento_fk"]);
                                    usuEvento = new UsuarioEvento((int)row["idUsuarioEvento"], evento, usu);
                                    usuariosCompartido.Add(usuEvento);
                                }
                            }
                            retorno = new Dictionary<string, object>();
                            retorno.Add("usuariosCompartido", usuariosCompartido);
                            retorno.Add("usuariosNoCompartido", usuariosNoCompartido);
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
                    return retorno;
                }
                public List<Usuario> sp_adminfe_getUsuariosFaltantesEvento(int idEvento, int idUsuarioEjecutor, int idPagina)
                {
                    List<Usuario> usuarios = null;
                    Usuario usuario;
                    SPIUS sp = new SPIUS("sp_adminfe_getUsuariosFaltantesEvento");
                    sp.agregarParametro("idEvento", idEvento);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                usuarios = new List<Usuario>();
                                foreach(DataRow row in tb[1].Rows){
                                    usuario = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), (bool)row["estado"]);
                                    usuarios.Add(usuario);
                                }
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
                    return usuarios;
                }
            #endregion
        #endregion
    }
}
