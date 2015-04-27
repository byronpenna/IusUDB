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
            #region "gets"
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
