using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// liberias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.FrontUI.Control
{
    public class ControlEnlaceInstitucion:PadreLib
    {
        #region "get"
            public List<EnlaceInstitucion> sp_frontui_getEnlacesByInstitucion(int idInstitucion,int idUsuarioEjecutor,int idPagina)
            {
                List<EnlaceInstitucion> enlaces =  null;
                SPIUS sp = new SPIUS("sp_frontui_getEnlacesByInstitucion");
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
                return enlaces;
            }
        #endregion
        #region "set"
            public EnlaceInstitucion sp_frontui_insertEnlaceInstituciones(EnlaceInstitucion enlaceAgregar,int idUsuarioEjecutor,int idPagina)
            {
                EnlaceInstitucion enlace = null;
                SPIUS sp = new SPIUS("sp_frontui_insertEnlaceInstituciones");
                
                sp.agregarParametro("enlace", enlaceAgregar._enlace);
                sp.agregarParametro("nombre_enlace", enlaceAgregar._nombreEnlace);
                sp.agregarParametro("idInstitucion", enlaceAgregar._institucion._idInstitucion);

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
                            enlace = new EnlaceInstitucion((int)row["idEnlace"],row["enlace"].ToString() ,row["nombre_enlace"].ToString(), (int)row["id_institucion_fk"]);
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
                return enlace;
            }
        #endregion
    }
}
