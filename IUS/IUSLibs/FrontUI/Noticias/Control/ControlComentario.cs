using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas 
    using IUSLibs.FrontUI.Noticias.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.FrontUI.Noticias.Control
{
    public class ControlComentario:PadreLib
    {
        #region "backend"
            //public List<Comentario> 
        #endregion
        #region "front end"
            #region "get"
                public List<Comentario> sp_frontUi_noticias_getComentariosPost(int idPost,string ip,int idPagina)
                {
                    List<Comentario> comentarios = null;
                    Comentario comentario;
                    SPIUS sp = new SPIUS("sp_frontUi_noticias_getComentariosPost");
                    sp.agregarParametro("idPost", idPost);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try{
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                comentarios = new List<Comentario>();
                                foreach (DataRow row in tb[1].Rows)
                                {
                                    comentario = new Comentario((int)row["idComentario"], row["comentario"].ToString(), row["correo_electronico"].ToString(), row["ip"].ToString(), row["nombre"].ToString(), (int)row["id_post_fk"], (DateTime)row["fecha_publicacion"]);
                                    comentarios.Add(comentario);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);

                        }
                    }catch(ErroresIUS x){
                        throw x;
                    }catch(Exception x){
                        throw x;
                    }
                
                    return comentarios;
                }
            #endregion
            #region "set"
                public Comentario sp_frontUi_noticias_ponerComentario(Comentario comentarioAgregar,int idPagina)
                {
                    Comentario comentarioAgregado = null;
                    SPIUS sp = new SPIUS("sp_frontUi_noticias_ponerComentario");
                    sp.agregarParametro("comentario", comentarioAgregar._comentario);
                    sp.agregarParametro("email", comentarioAgregar._email);
                    sp.agregarParametro("ip", comentarioAgregar._ip);
                    sp.agregarParametro("idPost", comentarioAgregar._post._idPost);
                    sp.agregarParametro("nombre", comentarioAgregar._nombre);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                comentarioAgregado = new Comentario((int)row["idComentario"], row["comentario"].ToString(), row["correo_electronico"].ToString(), row["ip"].ToString(), row["nombre"].ToString(), (int)row["id_post_fk"], (DateTime)row["fecha_publicacion"]);
                            }
                            else
                            {
                                ErroresIUS x = new ErroresIUS("Error no controlados", ErroresIUS.tipoError.sql, 0);
                                throw x;
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
                    return comentarioAgregado;
                }
            #endregion
        #endregion
    }
}
