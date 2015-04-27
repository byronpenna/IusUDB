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
                public UsuarioEvento sp_adminfe_compartirEvento(UsuarioEvento agregar,int idUsuarioEjecutor,int idPagina)
                {
                    UsuarioEvento agregado = null;
                    Evento evento; Usuario usuario; PermisoEvento permiso;
                    SPIUS sp = new SPIUS("sp_adminfe_compartirEvento");
                    sp.agregarParametro("idEvento", agregar._evento._idEvento);
                    sp.agregarParametro("idPermiso",agregar._permiso._idPermiso);
                    sp.agregarParametro("idUsuario", agregar._usuario._idUsuario);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            
                            agregado = new UsuarioEvento();
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
                public Dictionary<object,object> sp_adminfe_getPermisosUsuarioEvento(Evento evento,Usuario usuario,int idUsuarioEjecutor,int idPagina){
                    // declare 
                        Dictionary<object,object> toReturn = null;
                        List<PermisoEvento> permisosFaltantes = null;
                        List<UsuarioEvento> permisosActuales = null;
                        PermisoEvento permiso; UsuarioEvento usuarioEvento;
                    // do it 
                    SPIUS sp = new SPIUS("sp_adminfe_getPermisosUsuarioEvento");
                    sp.agregarParametro("idEvento", evento._idEvento);
                    sp.agregarParametro("idUsuario", usuario._idUsuario);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                permisosFaltantes = new List<PermisoEvento>();
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    permiso = new PermisoEvento((int)row["idPermiso"], row["permiso"].ToString());
                                    permisosFaltantes.Add(permiso);
                                }
                            }
                            if (tb[2].Rows.Count > 0)
                            {
                                permisosActuales = new List<UsuarioEvento>();
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    permiso = new PermisoEvento((int)row["idPermiso"], row["permiso"].ToString());
                                    usuarioEvento = new UsuarioEvento((int)row["idUsuarioEvento"], evento, permiso, usuario);
                                    permisosActuales.Add(usuarioEvento);
                                }
                            }
                            toReturn = new Dictionary<object, object>();
                            toReturn.Add("permisosActuales",permisosActuales);
                            toReturn.Add("permisosFaltantes", permisosFaltantes);
                        }
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x) {
                        throw x;
                    }
                    return toReturn;
                }
                // carga solo los usuarios a los que se les compartio
                public List<List<Usuario>> sp_adminfe_loadCompartirEventos(int idEvento,int idUsuarioEjecutor,int idPagina)
                {
                    List<List<Usuario>> retorno = null;
                    List<Usuario> usuariosCompartido = null;
                    List<Usuario> usuariosNoCompartido = null;
                    Usuario usu; Persona persona;
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
                                usuariosCompartido = new List<Usuario>();
                                // usuarios que estan en el evento
                                foreach (DataRow row in tb[2].Rows)
                                {
                                    persona = new Persona((int)row["id_persona_fk"]);
                                    usu = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), persona, (bool)row["estado"]);
                                    usuariosCompartido.Add(usu);
                                }
                            }
                            retorno = new List<List<Usuario>>();
                            retorno.Add(usuariosCompartido);
                            retorno.Add(usuariosNoCompartido);
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
            #endregion
        #endregion
    }
}
