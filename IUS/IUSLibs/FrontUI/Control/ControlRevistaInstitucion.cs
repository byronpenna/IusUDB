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
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.FrontUI.Control
{
    public class ControlRevistaInstitucion : PadreLib
    {
        #region "gets"
            public List<RevistaInstitucion> sp_frontui_getRevistasInstitucion(int idInstitucion,int idUsuarioEjecutor,int idPagina)
        {
            try
            {
                List<RevistaInstitucion> revistasInstituion = null;
                RevistaInstitucion revista;
                SPIUS sp = new SPIUS("sp_frontui_getRevistasInstitucion");
                sp.agregarParametro("idInstitucion", idInstitucion);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrectoGet(tb))
                {
                    if (tb[0].Rows.Count > 0)
                    {
                        revistasInstituion = new List<RevistaInstitucion>();
                        foreach (DataRow row in tb[0].Rows)
                        {
                            revista = new RevistaInstitucion((int)row["idRevistaInstitucion"]);
                            revista._anioPublicacion = (int)row["anioPublicacion"];
                            revista._categoria = row["categoria"].ToString();
                            revista._revista = row["revista"].ToString();
                            revista._institucion = new Institucion((int)row["id_institucion_fk"]);
                            revistasInstituion.Add(revista);
                        }
                    }
                }
                return revistasInstituion;
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
        #endregion
        #region "sets"
            public bool sp_frontui_deleteRevistaInstitucion(int idRevistaInstitucion,int idUsuarioEjecutor,int idPagina){
                try
                {
                    bool retorno = false;
                    SPIUS sp = new SPIUS("sp_frontui_deleteRevistaInstitucion");
                    sp.agregarParametro("idRevistaInstitucion", idRevistaInstitucion);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        retorno = true;
                    }
                    return retorno;
                }catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
            public RevistaInstitucion sp_frontui_updateRevistaInstitucion(RevistaInstitucion revistaActualizar, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    RevistaInstitucion revistaActualizada = null;
                    SPIUS sp = new SPIUS("sp_frontui_addRevistaInstitucion");
                    sp.agregarParametro("revista", revistaActualizar._revista);
                    sp.agregarParametro("categoria", revistaActualizar._categoria);
                    sp.agregarParametro("anioPublicacion", revistaActualizar._anioPublicacion);
                    sp.agregarParametro("idRevistaInstitucion", revistaActualizar._idRevistaInstitucion);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                        }
                    }
                    /**
                        @				varchar(200),
	                    @				char(150),
	                    @		int,
	                    @	int,
                     */
                    return revistaActualizada;
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
            
            public RevistaInstitucion sp_frontui_addRevistaInstitucion(RevistaInstitucion revistaAgregar,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    RevistaInstitucion revistaAgregada = null;
                    SPIUS sp = new SPIUS("sp_frontui_addRevistaInstitucion");
                    sp.agregarParametro("revista", revistaAgregar._revista);
                    sp.agregarParametro("categoria", revistaAgregar._categoria);
                    sp.agregarParametro("anioPublicacion", revistaAgregar._anioPublicacion);
                    sp.agregarParametro("idInstitucion", revistaAgregar._institucion._idInstitucion);
                    
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            revistaAgregada = new RevistaInstitucion((int)row["idRevistaInstitucion"], row["revista"].ToString(), row["categoria"].ToString(), (int)row["anioPublicacion"]);
                            revistaAgregada._institucion = new Institucion((int)row["id_institucion_fk"]);
                        }
                    }
                    return revistaAgregada;
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
        #endregion
    }
}

