﻿using System;
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
    using IUSLibs.REPO.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Control
{
    public class ControlCarpeta:PadreLib
    {
        #region "Get"
            public Dictionary<object, object> sp_repo_entrarCarpeta(Carpeta carpeta,int idUsuarioEjecutor, int idPagina)
            {
                Dictionary<object, object> retorno = new Dictionary<object,object>();
                List<Carpeta> carpetas = new List<Carpeta>();
                List<Archivo> archivos = new List<Archivo>();
                Carpeta carpetita;Usuario usuario;Carpeta carpetaPadre = null;
                Carpeta carpetaPadreRuta = new Carpeta();
                carpetaPadreRuta._ruta = "";
                TipoArchivo tipoArchivo; ExtensionArchivo extension; Archivo archivo;
                SPIUS sp = new SPIUS("sp_repo_entrarCarpeta");
                sp.agregarParametro("idCarpeta", carpeta._idCarpeta);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if(tb[0].Rows.Count > 0){
                            foreach(DataRow row in tb[0].Rows){
                                usuario = new Usuario((int)row["id_usuario_fk"]);
                                carpetaPadre = new Carpeta((int)row["id_carpetapadre_fk"]);
                                carpetita = new Carpeta((int)row["idCarpeta"],row["nombre"].ToString(),usuario,carpetaPadre,row["ruta"].ToString());
                                carpetita._fechaCreacion = (DateTime)row["fecha_creacion"];
                                carpetas.Add(carpetita);
                            }
                            
                        }
                        if (tb[1].Rows.Count > 0)
                        {
                            carpeta = new Carpeta();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                // archivos tipo
                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                                archivo = new Archivo((int)row["idArchivo"], row["nombre"].ToString(), carpeta, extension);
                                archivo._fechaCreacion = (DateTime)row["fecha_creacion"];
                                archivos.Add(archivo);
                            }
                        }
                        if (tb[2].Rows.Count > 0)
                        {
                            DataRow row = tb[2].Rows[0];
                            carpetaPadreRuta = new Carpeta((int)row["idCarpeta"],row["nombre"].ToString());
                            carpetaPadreRuta._ruta = row["strRuta"].ToString();
                        }
                    }
                    else
                    {
                        DataRow row = tb[0].Rows[0];
                        ErroresIUS x =  this.getErrorFromExecProcedure(row);
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
                retorno.Add("carpetaPadre", carpetaPadreRuta);
                retorno.Add("carpetas", carpetas);
                retorno.Add("archivos", archivos);
                return retorno;
            }
            public Dictionary<object,object> sp_repo_getRootFolder(int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_getRootFolder");
                Dictionary<object, object> retorno = new Dictionary<object,object>();
                List<Carpeta> carpetas = new List<Carpeta>(); List<Archivo> archivos = new List<Archivo>();
                Carpeta carpeta; Usuario usuario; Carpeta carpetaPadre; Archivo archivo;ExtensionArchivo extension;TipoArchivo tipoArchivo;
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if(this.resultadoCorrectoGet(tb)){
                        if (tb[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in tb[0].Rows)
                            {
                                usuario = new Usuario((int)row["id_usuario_fk"]);
                                carpetaPadre = new Carpeta();
                                carpeta = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(),usuario, carpetaPadre, row["ruta"].ToString());
                                carpeta._fechaCreacion = (DateTime)row["fecha_creacion"];
                                carpetas.Add(carpeta);
                            }
                        }
                        if (tb[1].Rows.Count > 0)
                        {
                            carpeta = new Carpeta();
                            foreach (DataRow row in tb[1].Rows)
                            { 
                                tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"]);
                                tipoArchivo._icono = row["icono"].ToString();
                                extension = new ExtensionArchivo((int)row["idExtension"],tipoArchivo);
                                archivo = new Archivo((int)row["idArchivo"], row["nombre"].ToString(),carpeta, extension);
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
                retorno.Add("carpetas", carpetas);
                retorno.Add("archivos", archivos);
                Carpeta carpetaActual = new Carpeta();
                carpetaActual._ruta = "/";
                retorno.Add("carpetaPadre", carpetaActual);
                return retorno;
            }
            public Carpeta sp_repo_byRuta(string strRuta, int idUsuarioEjecutor,int idPagina)
            {
                Carpeta carpeta = null;
                SPIUS sp = new SPIUS("sp_repo_byRuta");
                
                sp.agregarParametro("strRuta", strRuta);
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
                            carpeta = new Carpeta((int)row["id_carpetapadre_fk"]);
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
                return carpeta;
            }
        #endregion
        #region "acciones"
            public List<Carpeta> sp_repo_deleteFolder(int idCarpetaPadre,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_deleteFolder");
                sp.agregarParametro("idFolder", idCarpetaPadre);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                List<Carpeta> carpetas = null;
                Carpeta carpeta;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            carpetas = new List<Carpeta>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                carpeta = new Carpeta((int)row["idDelete"]);
                                carpetas.Add(carpeta);
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
                return carpetas;
            }
            public Carpeta sp_repo_updateCarpeta(Carpeta carpetaActualizar, int idUsuarioEjecutor, int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_updateCarpeta");
                Carpeta carpetaActualizada=null,carpetaPadre;
                sp.agregarParametro("nombre", carpetaActualizar._nombre);
                sp.agregarParametro("idCarpeta", carpetaActualizar._idCarpeta);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (tb[1].Rows.Count > 0)
                    {
                        DataRow row = tb[1].Rows[0];
                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                        {
                            carpetaPadre = new Carpeta((int)row["id_carpetapadre_fk"]);
                        }
                        else
                        {
                            carpetaPadre = new Carpeta();
                        }
                        carpetaActualizada = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(), (int)row["id_usuario_fk"], carpetaPadre, row["ruta"].ToString());
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
                if (carpetaActualizada == null)
                {
                    ErroresIUS x = new ErroresIUS("Error desconocido", ErroresIUS.tipoError.generico, 0);
                    throw x;
                }
                return carpetaActualizada;
            }
            public Carpeta sp_repo_insertCarpeta(Carpeta carpetaIngresar,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_repo_insertCarpeta");
                sp.agregarParametro("nombre", carpetaIngresar._nombre);
                if (carpetaIngresar._carpetaPadre._idCarpeta > 0)
                {
                    sp.agregarParametro("idCarpetaPadre", carpetaIngresar._carpetaPadre._idCarpeta);
                }
                else
                {
                    sp.agregarParametro("idCarpetaPadre",null);
                }
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                Carpeta carpetaEditada = null;
                Carpeta carpetaPadre;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if(tb[1].Rows.Count > 0){
                            DataRow row = tb[1].Rows[0];
                            if(row["id_carpetapadre_fk"] != DBNull.Value){
                                carpetaPadre = new Carpeta((int)row["id_carpetapadre_fk"]);
                            }else{
                                carpetaPadre = new Carpeta();
                            }
                            carpetaEditada = new Carpeta((int)row["idCarpeta"], row["nombre"].ToString(), (int)row["id_usuario_fk"], carpetaPadre, row["ruta"].ToString());
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
                if (carpetaEditada == null)
                {
                    ErroresIUS x = new ErroresIUS("Error desconocido",ErroresIUS.tipoError.generico,0);
                    throw x;
                }
                return carpetaEditada;
            }
        #endregion
    }
}
