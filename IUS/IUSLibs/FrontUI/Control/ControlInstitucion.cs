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
    public class ControlInstitucion:PadreLib
    {
        #region "private"
            private List<TelefonoInstitucion> getTelefonosByInstitucion(int idInstitucion,string ip,int idPagina)
            {
                List<TelefonoInstitucion> telefonos = null;
                try
                {
                    ControlTelefonoInstitucion controlTel = new ControlTelefonoInstitucion();
                    telefonos = controlTel.sp_frontui_spFront_getTelByInstitucion(idInstitucion, ip, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return telefonos;
            }
        #endregion
        #region "get"
            #region "frontend"
                public List<Institucion> sp_frontui_getInstitucionesByContinente(int idContinente,string ip,int idPagina)
                {
                    List<Institucion> instituciones = null; Institucion institucion; int idInstitucion;
                    SPIUS sp = new SPIUS("sp_frontui_getInstitucionesByContinente");
                    
                    sp.agregarParametro("idContinente", idContinente);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                instituciones = new List<Institucion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    Pais pais = new Pais((int)row["id_pais_fk"], row["pais"].ToString());
                                    idInstitucion = (int)row["idInstitucion"];
                                    List<TelefonoInstitucion> telefonos = this.getTelefonosByInstitucion(idInstitucion,ip,idPagina);
                                    institucion = new Institucion(idInstitucion, row["nombre"].ToString(), row["direccion"].ToString(), pais, (bool)row["estado"]);
                                    institucion._telefonos = telefonos;
                                    if (row["logo"] != DBNull.Value)
                                    {
                                        institucion._logo = (byte[])row["logo"];
                                    }
                                    instituciones.Add(institucion);
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
                    return instituciones;
                }
                public List<Institucion> sp_frontui_getInstituciones(int idUsuarioEjecutor, int idPagina)
                {
                    List<Institucion> instituciones = null;
                    Institucion institucion;
                    SPIUS sp = new SPIUS("sp_frontui_getInstituciones");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                instituciones = new List<Institucion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    Pais pais = new Pais((int)row["id_pais_fk"], row["pais"].ToString());
                                    institucion = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), pais, (bool)row["estado"]);
                                    instituciones.Add(institucion);
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
                    return instituciones;
                }
            #endregion
            #region "backend"
                public Institucion sp_frontui_getInstitucionById(int idInstitucion,int idUsuarioEjecutor,int idPagina)
                {
                    Institucion institucion = null; 
                    SPIUS sp = new SPIUS("sp_frontui_getInstitucionById");
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
                                DataRow row = tb[0].Rows[0];
                                institucion = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), (int)row["id_pais_fk"], (bool)row["estado"]);
                                if (row["logo"] != DBNull.Value)
                                {
                                    institucion._logo = (byte[])row["logo"];
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
                    return institucion;
                }
            #endregion
        #endregion
        #region "acciones"

            #region "backend"
                public bool sp_frontui_setLogoInstitucion(Institucion institucionActualizar,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_setLogoInstitucion");
                    sp.agregarParametro("idInstitucion", institucionActualizar._idInstitucion);
                    sp.agregarParametro("image", institucionActualizar._logo);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                public bool sp_frontui_deleteInstitucion(int idInstitucion,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_deleteInstitucion");
                    sp.agregarParametro("idInstitucion", idInstitucion);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                public Institucion sp_frontui_insertInstitucion(Institucion institucionAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    Institucion institucionAgregada = null;
                    SPIUS sp = new SPIUS("sp_frontui_insertInstitucion");

                    sp.agregarParametro("nombre", institucionAgregar._nombre);
                    sp.agregarParametro("direccion", institucionAgregar._direccion);
                    sp.agregarParametro("idPais", institucionAgregar._pais._idPais);
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
                                institucionAgregada = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), (int)row["id_pais_fk"],(bool)row["estado"]);

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
                    return institucionAgregada;
                }
            #endregion
        #endregion
    }
}
