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
    using IUSLibs.REPO.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Control.Compartido
{
    public class ControlArchivoCompartido:PadreLib
    {
        #region "get"
            public List<ArchivoCompartido> sp_repo_getFilesFromShareUserId(int idUserFile,int idUsuarioEjecutor, int idPagina)
            {
                List<ArchivoCompartido> archivosCompartidos= null; ArchivoCompartido archivoCompartido;
                //List<Archivo> archivos=null;
                Archivo archivo; 
                TipoArchivo tipoArchivo; ExtensionArchivo extension;
                SPIUS sp = new SPIUS("sp_repo_getFilesFromShareUserId");
                sp.agregarParametro("idUserFile", idUserFile);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            //archivos = new List<Archivo>();
                            archivosCompartidos = new List<ArchivoCompartido>();
                            foreach (DataRow row in tb[0].Rows) { 
                                
                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["id_extension_fk"], tipoArchivo);
                                int idCarpeta;
                                if (row["id_carpeta_fk"] == DBNull.Value)
                                {
                                    idCarpeta = 0;
                                }
                                else
                                {
                                    idCarpeta = (int)row["id_carpeta_fk"];
                                }
                                archivo = new Archivo((int)row["idArchivo"], row["nombre"].ToString(), idCarpeta, row["src"].ToString(), extension);
                                int idUsuarioArchivo = (int)row["id_usuario_fk"];
                                archivoCompartido = new ArchivoCompartido((int)row["idArchivoCompartido"], archivo,idUsuarioArchivo);
                                if (idUsuarioArchivo == idUsuarioEjecutor)
                                {
                                    archivoCompartido._propio = true;
                                }
                                //archivos.Add(archivo);
                                archivosCompartidos.Add(archivoCompartido);
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
                return archivosCompartidos;
            }    
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
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                usuarios = new List<Usuario>();
                                foreach(DataRow row in tb[0].Rows){
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
            public bool sp_repo_dejarDeCompartirTodo(int idUsuario,int idUsuarioEjecutor,int idPagina)
            {
                bool estado = false;
                SPIUS sp = new SPIUS("sp_repo_dejarDeCompartirTodo");
                sp.agregarParametro("idUsuario", idUsuario);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
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
            public bool sp_repo_removeShareFile(int idArchivo,int idUsuarioEjecutor, int idPagina)
            {
                bool estado = false;
                SPIUS sp = new SPIUS("sp_repo_removeShareFile");
                sp.agregarParametro("idArchivo", idArchivo);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
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
