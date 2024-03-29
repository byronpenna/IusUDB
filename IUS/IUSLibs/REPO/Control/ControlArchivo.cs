﻿using System;
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
    using IUSLibs.REPO.Entidades;    
namespace IUSLibs.REPO.Control
{
    public class ControlArchivo:PadreLib
    {
        #region "get"
            public Archivo sp_repo_getDownloadFile(int idArchivo, int idUsuarioEjecutor, int idPagina)
            {
                Archivo archivo = null; TipoArchivo tipoArchivo; ExtensionArchivo extension;
                SPIUS sp = new SPIUS("sp_repo_getDownloadFile");
                sp.agregarParametro("idArchivo", idArchivo);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                            tipoArchivo._icono = row["icono"].ToString();
                            extension = new ExtensionArchivo((int)row["id_extension_fk"], row["extension"].ToString(), tipoArchivo);
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
            public List<Archivo> sp_repo_searchArchivo(string nombre,int idUsuarioEjecutor, int idPagina)
            {
                List<Archivo> archivos = null; Archivo archivo; TipoArchivo tipoArchivo;
                ExtensionArchivo extension; Carpeta carpeta;
                SPIUS sp = new SPIUS("sp_repo_searchArchivo");
                sp.agregarParametro("nombre", nombre);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            archivos = new List<Archivo>();
                            foreach (DataRow row in tb[0].Rows)
                            {
                                if (row["id_carpeta_fk"] != DBNull.Value)
                                {
                                    carpeta = new Carpeta((int)row["id_carpeta_fk"]);
                                }
                                else
                                {
                                    carpeta = null;
                                }
                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                                archivo = new Archivo((int)row["idArchivo"], row["nombre"].ToString(), carpeta, extension);
                                archivo._fechaCreacion = (DateTime)row["fecha_creacion"];
                                archivos.Add(archivo);
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
                return archivos;
            }
        #endregion
        #region "do"
            public Archivo sp_repo_deleteFile(int idArchivo, int idUsuarioEjecutor, int idPagina)
            {
                TipoArchivo tipoArchivo; ExtensionArchivo extension; Archivo archivo = null;
                SPIUS sp = new SPIUS("sp_repo_deleteFile");
                sp.agregarParametro("idArchivo", idArchivo);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                            tipoArchivo._icono = row["icono"].ToString();
                            extension = new ExtensionArchivo((int)row["id_extension_fk"], row["extension"].ToString(), tipoArchivo);
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
                return archivo;
            }
            public Archivo sp_repo_changeFileName(Archivo archivoModificar, int idUsuarioEjecutor, int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_changeFileName");
                TipoArchivo tipoArchivo; ExtensionArchivo extension; Archivo archivo = null;
                sp.agregarParametro("idArchivo", archivoModificar._idArchivo);
                sp.agregarParametro("nombre", archivoModificar._nombre);
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
                return archivo;
            }
            public Archivo sp_repo_refreshSourceFile(Archivo archivoModificar,int idUsuarioEjecutor,int idPagina)
            {
                Archivo archivo = null; TipoArchivo tipoArchivo; ExtensionArchivo extension;
                SPIUS sp = new SPIUS("sp_repo_refreshSourceFile");
                sp.agregarParametro("idArchivo", archivoModificar._idArchivo);
                sp.agregarParametro("src", archivoModificar._src);
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
                return archivo;
            }
            public Archivo sp_repo_uploadFile(Archivo archivoAgregar,int idUsuarioEjecutor,int idPagina)
            {
                Archivo archivoAgregado=null; ExtensionArchivo extension; TipoArchivo tipoArchivo;
                SPIUS sp = new SPIUS("sp_repo_uploadFile");
                sp.agregarParametro("nombre", archivoAgregar._nombre);
                if (archivoAgregar._carpeta._idCarpeta != -1)
                {
                    sp.agregarParametro("idCarpeta", archivoAgregar._carpeta._idCarpeta);
                }
                else
                {
                    sp.agregarParametro("idCarpeta", null);
                }
                //sp.agregarParametro("src", archivoAgregar._src);
                sp.agregarParametro("extension", archivoAgregar._extension._extension);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb)) {
                        if (tb[1].Rows.Count > 0)
                        {
                            foreach (DataRow row in tb[1].Rows)
                            {
                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["id_extension_fk"],tipoArchivo);
                                int idCarpeta;
                                if(row["id_carpeta_fk"] == DBNull.Value){
                                    idCarpeta = 0;
                                }
                                else
                                {
                                    idCarpeta = (int)row["id_carpeta_fk"];
                                }
                                archivoAgregado = new Archivo((int)row["idArchivo"], row["nombre"].ToString(), idCarpeta, row["src"].ToString(), extension);
                            
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
                return archivoAgregado;
            }
        #endregion
    }
}
