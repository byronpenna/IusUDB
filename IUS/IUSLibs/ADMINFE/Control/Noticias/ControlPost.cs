﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas 
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlPost:PadreLib
    {
        #region "Get"
            public List<Post> sp_adminfe_noticias_getPosts(int idUsuarioEjecutor, int idPagina) { 
                List<Post> posts = null;
                Post post;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_getPosts");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    Usuario usuarioCreador;
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            posts = new List<Post>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                usuarioCreador = new Usuario((int)row["id_usuario_fk"],row["usuario"].ToString());
                                post = new Post((int)row["idPost"], (DateTime)row["fecha_creacion"], (DateTime)row["ultima_modificacion"], row["titulo"].ToString(), row["contenido"].ToString(), (bool)row["estado"], usuarioCreador);
                                posts.Add(post);
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
                return posts;
            }
        #endregion
        #region "Acciones"
            public Post sp_adminfe_noticias_publicarPost(Post postAgregar,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_publicarPost");
                Post postRegresar = null;
                sp.agregarParametro("titulo", postAgregar._titulo);
                sp.agregarParametro("contenido", postAgregar._contenido);
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
                            postAgregar = new Post(1);
                            postRegresar = new Post((int)row["idPost"]);

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
                return postRegresar;
            }
            public Post sp_adminfe_noticias_cambiarEstadoPost(int idPost,int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_cambiarEstadoPost");
                Post post = null;
                sp.agregarParametro("idPost", idPost);
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
                            post = new Post((int)row["idPost"]);
                            post._estado = (bool)row["estado"];
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
                return post;
            }
        #endregion
    }
}
