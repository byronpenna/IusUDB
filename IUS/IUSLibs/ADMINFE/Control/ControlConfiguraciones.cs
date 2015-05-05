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
    public class ControlConfiguraciones:PadreLib
    {
        public Dictionary<object, object> sp_adminfe_getConfiguraciones(int idUsuarioEjecutor,int idPagina)
        {
            Dictionary<object, object> respuesta= null;
            SPIUS sp = new SPIUS("sp_adminfe_getConfiguraciones");
            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            Configuracion config = null;
            List<RedSocial> redesSociales = null;
            RedSocial redSocial;
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrecto(tb))
                {
                    if (tb[2].Rows.Count > 0)
                    {
                        redesSociales = new List<RedSocial>();
                        foreach (DataRow row in tb[2].Rows) {
                            redSocial = new RedSocial((int)row["idRedSocial"], row["nombre"].ToString(), row["enlace"].ToString(), row["clase_icono"].ToString());
                            redesSociales.Add(redSocial);
                        }
                    }
                    if (tb[1].Rows.Count > 0)
                    {
                        DataRow rowConfiguracion = tb[1].Rows[0];
                        int idConfiguracion = Convert.ToInt32((bool)tb[1].Rows[0]["idConfiguracion"]);
                        config = new Configuracion(idConfiguracion, (int)rowConfiguracion["id_idioma_fk"], rowConfiguracion["vision"].ToString(), rowConfiguracion["mision"].ToString(), rowConfiguracion["historia"].ToString());
                    }
                    respuesta = new Dictionary<object, object>();
                    respuesta.Add("configuracion", config);
                    respuesta.Add("redesSociales", redesSociales);
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
            return respuesta;
        }
    }
}
