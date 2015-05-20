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
    public class ControlPostTag:PadreLib
    {
        #region "acciones"
            public List<Tag> sp_adminfe_noticias_updateTag(string tags,int idPost,int idUsuarioEjecutor,int idPagina) {
                List<Tag> tagsList = null; Tag objTag;
                SPIUS sp = new SPIUS("sp_adminfe_noticias_updateTag");
                sp.agregarParametro("tags", tags);
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
                            tagsList = new List<Tag>();
                            foreach (DataRow row in tb[1].Rows)
                            {
                                objTag = new Tag((int)row["idTag"], row["tag"].ToString());
                                tagsList.Add(objTag);
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
                return tagsList;
            }
            public PostTag sp_adminfe_noticias_agregarTag(int idPost,string tag, int idUsuarioEjecutor,int idPagina)
            {
                SPIUS sp = new SPIUS("sp_adminfe_noticias_agregarTag");
                sp.agregarParametro("idPost", idPost);
                sp.agregarParametro("tag", tag);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                PostTag toReturn = null;
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            Post post = new Post((int)row["id_post_fk"]);
                            Tag objtag = new Tag((int)row["id_tags_fk"], row["tag"].ToString());
                            toReturn = new PostTag((int)row["idPostTag"], post, objtag);
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
                return toReturn;
            }
        #endregion
    }
}
