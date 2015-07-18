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
    using IUSLibs.REPO.Entidades.Compartido;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Control.Compartido
{
    public class ControlArchivoCompartido:PadreLib
    {
        #region "get"
            public List<Usuario> sp_repo_getUsuariosArchivosCompartidos(int idUsuarioEjecutor, int idPagina)
            {
                List<Usuario> usuarios = null; Usuario usuario;
                SPIUS sp = new SPIUS("sp_repo_getUsuariosArchivosCompartidos");
                //sp.agregarParametro("idUsuario", idUsuario);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            usuarios = new List<Usuario>();
                            foreach(DataRow row in tb[1].Rows){
                                usuario = new Usuario((int)row["idUsuario"],row["usuario"].ToString());
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
        #region "set"
            public ArchivoCompartido sp_repo_compartirArchivo(ArchivoCompartido archivoAgregar,int idUsuarioEjecutor, int idPagina)
        {
            SPIUS sp = new SPIUS("sp_repo_compartirArchivo");
            ArchivoCompartido archivo = null;
            sp.agregarParametro("idArchivo",archivoAgregar._archivo._idArchivo);
            sp.agregarParametro("idUsuario", archivoAgregar._usuario._idUsuario);
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
                        archivo = new ArchivoCompartido((int)row["idArchivoCompartido"], (int)row["id_archivo_fk"], (int)row["id_usuario_fk"], (DateTime)row["fecha"]);
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
            return archivo;
        }
        #endregion
    }
}
