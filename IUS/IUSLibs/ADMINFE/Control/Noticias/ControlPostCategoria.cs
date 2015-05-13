using System;
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
    public class ControlPostCategoria:PadreLib
    {
        #region "acciones"
        #endregion
        #region "get"
            public List<PostCategoria> sp_adminfe_noticias_getCategorias(int idUsuarioEjecutor,int idPagina)
            {
                List<PostCategoria> categorias = null;
                PostCategoria categoria;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_getCategorias");
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if(tb[1].Rows.Count > 0){
                            categorias = new List<PostCategoria>();
                            foreach(DataRow row in tb[1].Rows){
                                categoria = new PostCategoria((int)row["idPostCategoria"], row["categoria"].ToString());
                                categorias.Add(categoria);
                            }
                        }
                        
                    }
                }catch(ErroresIUS x){
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
                return categorias;
            }
        #endregion
    }
}
