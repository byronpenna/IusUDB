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
    public class ControlEmailInstitucion:PadreLib
    {
        #region "get"
            public List<EmailInstitucion> sp_frontui_getEmailInstitucion(int idInstitucion,int idUsuarioEjecutor, int idPagina)
            {
                /*
                    @idInstitucion		int,
	                -- seguridad 
	                @idUsuarioEjecutor	int,
	                @idPagina			int
                 */

                List<EmailInstitucion> emailsInstituciones = null; EmailInstitucion emailInstitucion;
                SPIUS sp = new SPIUS("sp_frontui_getEmailInstitucion");

                sp.agregarParametro("idInstitucion", idInstitucion);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            emailsInstituciones = new List<EmailInstitucion>();
                            DataRow row = tb[0].Rows[0];
                            emailInstitucion = new EmailInstitucion((int)row["idEmailInstituciones"], (int)row["id_institucion_fk"], row["email"].ToString());
                            emailsInstituciones.Add(emailInstitucion);

                        }
                    }
                    return emailsInstituciones;
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
        #region "set"
            public bool sp_frontui_eliminarEmailInstitucion(int idEmailInstitucion,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_eliminarEmailInstitucion");
                    sp.agregarParametro("idEmailInstitucion", idEmailInstitucion);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
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
            public EmailInstitucion sp_frontui_agregarEmailInstitucion(EmailInstitucion emailInstitucionAgregar,int idUsuarioEjecutor,int idPagina)
            {
                EmailInstitucion emailAgregado = null;
                try
                {
                    SPIUS sp = new SPIUS("sp_frontui_agregarEmailInstitucion");

                    sp.agregarParametro("email", emailInstitucionAgregar._email);
                    sp.agregarParametro("idInstitucion", emailInstitucionAgregar._institucion._idInstitucion);
                    
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrecto(tb))
                    {
                        if (tb[1].Rows.Count > 0)
                        {
                            DataRow row = tb[1].Rows[0];
                            emailAgregado = new EmailInstitucion((int)row["idEmailInstituciones"], (int)row["id_institucion_fk"], row["email"].ToString());
                            
                        }
                    }
                    return emailAgregado;
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
