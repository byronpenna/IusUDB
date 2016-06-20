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
    public class ControlTipoInstitucion:PadreLib
    {
        public List<TipoInstitucion> sp_frontui_getTiposInstituciones(int idUsuarioEjecutor, int idPagina)
        {
            TipoInstitucion tipoInstitucion;
            List<TipoInstitucion> tiposInstituciones = null;

            SPIUS sp = new SPIUS("sp_frontui_getTiposInstituciones");
            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrectoGet(tb))
                {
                    if (tb[0].Rows.Count > 0)
                    {
                        tiposInstituciones = new List<TipoInstitucion>();
                        foreach (DataRow row in tb[0].Rows)
                        {
                            tipoInstitucion = new TipoInstitucion((int)row["idTipoInstitucion"], row["tipoInstitucion"].ToString());
                            tiposInstituciones.Add(tipoInstitucion);
                        }
                    }
                }
                return tiposInstituciones;
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
    }
}
