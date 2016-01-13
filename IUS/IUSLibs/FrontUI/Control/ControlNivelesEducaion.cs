using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias 
    // generales
        using IUSLibs.BaseDatos;
        using IUSLibs.GENERALS;
        using IUSLibs.LOGS;
    // manejo de datos
        using System.Data.Sql;
        using System.Data.SqlClient;
        using System.Data;
    // otras
        using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.FrontUI.Control
{
    public class ControlNivelesEducaion:PadreLib
    {
        #region "funciones"
            #region "get"
                public List<NivelEducacion> sp_frontui_getNivelesEducacion(int idUsuarioEjecutor,int idPagina)
                {
                    NivelEducacion nivelEducacion = null;
                    List<NivelEducacion> nivelesEducacion = null; List<NivelEducacion> nivelesSeleccionados = null;
                    SPIUS sp = new SPIUS("sp_frontui_getNivelesEducacion");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if(tb[0].Rows.Count > 0)
                            {
                                nivelesEducacion = new List<NivelEducacion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    nivelEducacion = new NivelEducacion((int)row["idNivelEducacion"], row["codigo"].ToString(), row["descripcion"].ToString());
                                    if ((int)row["isSelected"] == 1)
                                    {
                                        nivelEducacion._selected = true;
                                    }
                                    nivelesEducacion.Add(nivelEducacion);
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
                    return nivelesEducacion;
                }
            #endregion
        #endregion
    }
}
