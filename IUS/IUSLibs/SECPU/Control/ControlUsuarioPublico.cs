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
    using IUSLibs.SECPU.Entidades;
namespace IUSLibs.SECPU.Control
{
    public class ControlUsuarioPublico:PadreLib
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
            #region "get"
                public Dictionary<object,object> sp_secpu_reenviarCorreo(string correo)
                {
                    CodigoVerificacion codigo = null;
                    UsuarioPublico usuarioPublico = null;
                    Dictionary<object, object> respuesta = null;
                    SPIUS sp = new SPIUS("sp_secpu_reenviarCorreo");
                    sp.agregarParametro("correo", correo);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            respuesta = new Dictionary<object, object>();
                            if (tb[0].Rows.Count > 0)
                            {
                                
                                DataRow row = tb[0].Rows[0];
                                codigo = new CodigoVerificacion((int)row["idCodigoVerificacion"], (int)row["numero"], (DateTime)row["fecha_vencimiento"], (int)row["id_usuariopublico_fk"]);
                                usuarioPublico = new UsuarioPublico((int)row["idUsuarioPublico"]);
                                usuarioPublico._nombres     = row["nombres"].ToString();
                                usuarioPublico._apellidos   = row["apellidos"].ToString();
                                usuarioPublico._email = row["email"].ToString();
                                
                            }
                            respuesta.Add("usuarioPublico", usuarioPublico);
                            respuesta.Add("codigo", codigo);
                        }
                        return respuesta;
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
                public UsuarioPublico sp_adminfe_front_getLogin(string usuario,string pass,string ip,int idPagina)
                {
                    UsuarioPublico usuarioLogs = null;
                    SPIUS sp = new SPIUS("sp_adminfe_front_getLogin");
                    sp.agregarParametro("usuario", usuario);
                    sp.agregarParametro("pass", pass);

                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                usuarioLogs = new UsuarioPublico((int)row["idUsuarioPublico"], row["nombres"].ToString(), row["apellidos"].ToString(), row["email"].ToString(), (DateTime)row["fecha_nacimiento"], (int)row["id_estadousuario_fk"]);
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
                    return usuarioLogs;
                }
            #endregion
            #region "set"
                public ValidadorPassPublico sp_secpu_solicitarCambio(string email)
                {
                    ValidadorPassPublico retorno = null;
                    SPIUS sp = new SPIUS("sp_secpu_solicitarCambio");
                    sp.agregarParametro("email", email);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[0].Rows[0];
                                retorno = new ValidadorPassPublico((int)row["idValidadorPass"], (int)row["codigo"], (DateTime)row["vencimiento"], (int)row["intentos"], (int)row["id_usuario_fk"]);

                            }
                        }
                        else
                        {
                            DataRow row = tb[0].Rows[0];
                            ErroresIUS x = this.getErrorFromExecProcedure(row);
                            throw x;
                        }
                        return retorno;
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
                public Dictionary<object,object> sp_secpu_addUsuario(UsuarioPublico usuarioAgregar)
                {
                    Dictionary<object, object> retorno = null;

                    UsuarioPublico usuarioAgregado = null; CodigoVerificacion codigo = null;
                    SPIUS sp = new SPIUS("sp_secpu_addUsuario");
                    
                    sp.agregarParametro("nombres", usuarioAgregar._nombres);
                    sp.agregarParametro("apellidos",usuarioAgregar._apellidos);
                    sp.agregarParametro("email",usuarioAgregar._email);
                    sp.agregarParametro("fechaNac",usuarioAgregar._fechaNac);
                    sp.agregarParametro("pass", usuarioAgregar._pass);
                    
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            retorno = new Dictionary<object, object>();
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[0].Rows[0];
                                usuarioAgregado = new UsuarioPublico((int)row["idUsuarioPublico"], row["nombres"].ToString(), row["apellidos"].ToString(), row["email"].ToString(), (DateTime)row["fecha_nacimiento"], row["pass"].ToString(),(int)row["id_estadousuario_fk"]);
                                
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                codigo = new CodigoVerificacion((int)row["idCodigoVerificacion"], (int)row["numero"], (DateTime)row["fecha_vencimiento"], (int)row["id_usuariopublico_fk"]);
                            }
                            retorno.Add("usuarioAgregado",usuarioAgregado);
                            retorno.Add("codigoVerificacion", codigo);
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
                    return retorno;
                }
            #endregion
        #endregion
        #region "constructores"
            public ControlUsuarioPublico()
            {
                
            }
        #endregion
    }
}
