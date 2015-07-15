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
            #endregion
            #region "set"
                public UsuarioPublico sp_secpu_addUsuario(UsuarioPublico usuarioAgregar)
                {
                    UsuarioPublico usuarioAgregado = null;
                    SPIUS sp = new SPIUS("sp_secpu_addUsuario");
                    
                    sp.agregarParametro("nombres", usuarioAgregar._nombres);
                    sp.agregarParametro("apellidos",usuarioAgregar._apellidos);
                    sp.agregarParametro("email",usuarioAgregar._email);
                    sp.agregarParametro("fechaNac",usuarioAgregar._fechaNac);
                    sp.agregarParametro("pass", usuarioAgregar._pass);
                    
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                usuarioAgregado = new UsuarioPublico((int)row["idUsuarioPublico"], row["nombres"].ToString(), row["apellidos"].ToString(), row["email"].ToString(), (DateTime)row["fecha_nacimiento"], row["pass"].ToString(),(int)row["id_estadousuario_fk"]);
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
                    return usuarioAgregado;
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
