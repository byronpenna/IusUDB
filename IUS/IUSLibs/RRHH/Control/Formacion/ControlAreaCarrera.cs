﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    //------------
        using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlAreaCarrera:PadreLib
    {
        #region "funciones"
            #region "do"
                
            #endregion
            #region "get"
                public List<AreaCarrera> sp_frontui_getAreasConoInstituciones(int idUsuarioEjecutor,int idPagina,int idInstitucion)
                {
                    List<AreaCarrera> areasCarreras = null;
                    AreaCarrera areaCarrera;
                    SPIUS sp = new SPIUS("sp_frontui_getAreasConoInstituciones");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    sp.agregarParametro("idInstitucion", idInstitucion);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                areasCarreras = new List<AreaCarrera>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    areaCarrera = new AreaCarrera((int)row["idArea"], row["area"].ToString(), row["codigo"].ToString());
                                    if ((int)row["isSelected"] == 1)
                                    {
                                        areaCarrera._selected = true;
                                    }
                                    areasCarreras.Add(areaCarrera);
                                }

                            }
                        }
                        return areasCarreras;
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
                public List<AreaCarrera> sp_rrhh_getAreasCarreras(int idUsuarioEjecutor, int idPagina)
                {
                    List<AreaCarrera> areasCarreras = null;
                    SPIUS sp = new SPIUS("sp_rrhh_getAreasCarreras");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                areasCarreras = new List<AreaCarrera>();
                                foreach(DataRow row in tb[0].Rows){
                                    AreaCarrera area = new AreaCarrera((int)row["idArea"], row["area"].ToString());
                                    areasCarreras.Add(area);
                                }
                                
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
                    return areasCarreras;
                }
            #endregion
        #endregion
    }
}
