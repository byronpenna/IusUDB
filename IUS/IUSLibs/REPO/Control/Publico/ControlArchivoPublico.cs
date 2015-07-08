using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// liberias
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    // repositorio
        using IUSLibs.REPO.Entidades.Publico;
        using IUSLibs.REPO.Entidades;
namespace IUSLibs.REPO.Control.Publico
{
    public class ControlArchivoPublico:PadreLib
    {
        #region "get"
        public List<ArchivoPublico> sp_repo_front_getArchivosPublicosByType(int idCarpeta,int idTipoArchivo, string ip, int idPagina)
            {
                List<ArchivoPublico> archivosPublicos = null; ArchivoPublico archivo;
                Archivo archivoNormal; ExtensionArchivo extension; TipoArchivo tipoArchivo;
                CarpetaPublica carpetaPublica;
                SPIUS sp = new SPIUS("sp_repo_front_getArchivosPublicosByType");
                /*En teoria este if esta demas pero para no dar error en caso de ataque */
                if (idCarpeta != -1)
                {
                    sp.agregarParametro("idCarpeta", idCarpeta);
                }
                else
                {
                    sp.agregarParametro("idCarpeta", null);
                }
                sp.agregarParametro("idTipoArchivo", idTipoArchivo);
                sp.agregarParametro("ip", ip);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            archivosPublicos = new List<ArchivoPublico>();
                            foreach (DataRow row in tb[0].Rows)
                            {

                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"]);
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                                archivoNormal = new Archivo((int)row["idArchivo"], extension);
                                if (row["id_carpetapublica_fk"] == DBNull.Value)
                                {
                                    carpetaPublica = new CarpetaPublica();
                                }
                                else
                                {
                                    carpetaPublica = new CarpetaPublica((int)row["id_carpetapublica_fk"]);
                                }
                                archivo = new ArchivoPublico((int)row["idArchivoPublico"], archivoNormal, carpetaPublica, row["nombre_publico"].ToString(), (bool)row["estado"]);
                                archivosPublicos.Add(archivo);
                            }
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
                return archivosPublicos;
            }
            public List<ArchivoPublico> sp_repo_front_getAllArchivosPublicos(int idCarpeta, string ip, int idPagina)
            {
                List<ArchivoPublico> archivosPublicos = null; ArchivoPublico archivo;
                Archivo archivoNormal; ExtensionArchivo extension; TipoArchivo tipoArchivo;
                CarpetaPublica carpetaPublica;
                SPIUS sp = new SPIUS("sp_repo_front_getAllArchivosPublicos");
                if (idCarpeta != -1)
                {
                    sp.agregarParametro("idCarpeta", idCarpeta);
                }
                else
                {
                    sp.agregarParametro("idCarpeta", null);
                }
                sp.agregarParametro("ip", ip);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            archivosPublicos = new List<ArchivoPublico>();
                            foreach (DataRow row in tb[0].Rows)
                            {

                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"]);
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                                archivoNormal = new Archivo((int)row["idArchivo"], extension);
                                if(row["id_carpetapublica_fk"] == DBNull.Value){
                                    carpetaPublica = new CarpetaPublica();
                                }else{
                                    carpetaPublica = new CarpetaPublica((int)row["id_carpetapublica_fk"]);
                                }
                                archivo = new ArchivoPublico((int)row["idArchivoPublico"], archivoNormal,carpetaPublica , row["nombre_publico"].ToString(), (bool)row["estado"]);
                                archivosPublicos.Add(archivo);
                            }
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
                return archivosPublicos;
            }
        #endregion
        #region "set"
            public ArchivoPublico sp_repo_compartirArchivoPublico(ArchivoPublico archivoAgregar,int idUsuarioEjecutor, int idPagina)
        {
            ArchivoPublico archivoCompartido = null;
            SPIUS sp = new SPIUS("sp_repo_compartirArchivoPublico");
            Archivo archivoNormal; ExtensionArchivo extension; TipoArchivo tipoArchivo;
            sp.agregarParametro("idArchivo", archivoAgregar._archivoUsuario._idArchivo);
            if (archivoAgregar._carpetaPublica._idCarpetaPublica == -1)
            {
                sp.agregarParametro("idCarpetaPublica", null);
            }
            else
            {
                sp.agregarParametro("idCarpetaPublica", archivoAgregar._carpetaPublica._idCarpetaPublica);
            }
            
            sp.agregarParametro("nombrePublico", archivoAgregar._nombre);

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
                        /*
                        CarpetaPublica carpetaPublica = null;
                        if(row["id_carpetapublica_fk"] != DBNull.Value){
                            carpetaPublica = new CarpetaPublica((int)row["id_carpetapublica_fk"]);
                        }
                        archivoCompartido = new ArchivoPublico((int)row["idArchivoPublico"], (int)row["id_archivousuario_fk"],carpetaPublica, row["nombre_publico"].ToString(), (bool)row["estado"]);*/
                        tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"]);
                        tipoArchivo._icono = row["icono"].ToString();
                        extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                        archivoNormal = new Archivo((int)row["idArchivo"], extension);
                        archivoCompartido = new ArchivoPublico((int)row["idArchivoPublico"], archivoNormal, (int)row["id_carpetapublica_fk"], row["nombre_publico"].ToString(), (bool)row["estado"]);
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
            return archivoCompartido;
        }
        #endregion
        
    }
}
