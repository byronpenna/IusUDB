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
    public class ControlPublicacionEvento : PadreLib
    {
        #region "gets"
        public PublicacionEvento sp_adminfe_getPublicacionEventoById(int idPublicacionEvento, int idUsuarioEjecutor, int idPagina)
        {
            PublicacionEvento retorno = null;
            Evento evento = null;
            EventoWebsite eventoWebsite = null;
            SPIUS sp = new SPIUS("sp_adminfe_getPublicacionEventoById");
            sp.agregarParametro("idPublicacionEvento", idPublicacionEvento);
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
                        evento = new Evento((int)row["idEvento"], row["evento"].ToString(), (DateTime)row["fecha_inicio"], (DateTime)row["fecha_fin"], (int)row["id_usuario_creador_fk"], row["descripcion"].ToString());
                        eventoWebsite = new EventoWebsite((int)row["id_eventoweb_fk"]);
                        eventoWebsite._fechaPublicacion = (DateTime)row["fechaEw"];
                        eventoWebsite._usuarioPublicacion = new Usuario();
                        eventoWebsite._usuarioPublicacion._idUsuario = (int)row["usuario_publicacion_fk"];
                        eventoWebsite._evento = evento;
                        retorno = new PublicacionEvento((int)row["idPublicacionEvento"]);
                        retorno._eventoWeb = eventoWebsite;
                        retorno._estado = (bool)row["estadoPublicacionEvento"];
                        retorno._caducidad = (DateTime)row["caducidad"];
                        retorno._usuarioAutorizador = new Usuario((int)row["id_usuarioautorizador_fk"]);
                        retorno._principal = (bool)row["principal"];
                        /*
                         * pe.,
                         * pe.estado as ,
                         * pe.,
                         * pe.,
                         * pe.,ew.estado,
                         * ew.,
                         * ew. ,
                         * e.**/
                        
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
            return retorno;
        }
        #endregion
    }
}
