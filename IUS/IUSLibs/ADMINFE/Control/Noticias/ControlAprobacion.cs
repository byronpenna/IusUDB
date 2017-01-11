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
namespace IUSLibs.ADMINFE.Control.Noticias
{
    public class ControlAprobacion:PadreLib
    {
        //sp_adminfe_cambiarEstadoPublicacion
        #region "backend"
            public List<NotiEvento> sp_adminfe_aprobarNoticia_getNoticiasRevision(int idUsuarioEjecutor,int idPagina)
            {
                List<NotiEvento> noticiasEventos = null;
                NotiEvento noticiaEvento;
                SPIUS sp = new SPIUS("sp_adminfe_aprobarNoticia_getNoticiasRevision");

                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);

                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            noticiasEventos = new List<NotiEvento>();
                            foreach(DataRow row in tb[0].Rows){
                                //,,,,estado,caducidad
                                noticiaEvento = new NotiEvento((int)row["id"],row["titulo"].ToString(),row["descripcion"].ToString(),(int)row["tipoEntrada"]);
                                noticiaEvento._fechaCaducidad = (DateTime)row["fecha"];
                                noticiaEvento._estado = (bool)row["estado"];
                                noticiasEventos.Add(noticiaEvento);
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
                return noticiasEventos;
            }
            #region "set"
                public bool sp_adminfe_eliminarSolicitudPublicacion(NotiEvento notiEventoEliminar, int idUsuarioEjecutor, int idPagina)
                {
                    SPIUS sp = new SPIUS("sp_adminfe_eliminarSolicitudPublicacion");
                    bool estado = false;
                    /*
                        @					int,
	                    @			int,
                     */
                    sp.agregarParametro("tipo", notiEventoEliminar._idTipoEntrada);
                    sp.agregarParametro("idPublicacion", notiEventoEliminar._id);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
                        }
                        return estado;
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
                public NotiEvento sp_adminfe_cambiarEstadoPublicacion(NotiEvento notiEventoToChange, int idUsuarioEjecutor, int idPagina)
                {
                    NotiEvento noticiaEvento = null;
                    SPIUS sp = new SPIUS("sp_adminfe_cambiarEstadoPublicacion");
                    /*
                        @					int,
		                @				date,
		                @			int,
                     */
                    sp.agregarParametro("tipo", notiEventoToChange._idTipoEntrada);
                    sp.agregarParametro("caducidad", notiEventoToChange._fechaCaducidad);
                    sp.agregarParametro("idPublicacion", notiEventoToChange._id);

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
                                noticiaEvento = new NotiEvento((int)row["id"]);

                                noticiaEvento._fecha = (DateTime)row["fecha"];
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
                    return noticiaEvento;
                }
            #endregion
            
        #endregion
    }
}
