using System;
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
    // --------------
        using IUSLibs.RRHH.Entidades.Formacion; 
namespace IUSLibs.RRHH.Control.Formacion
{
    public class ControlNivelCarrera:PadreLib
    {
        #region "funciones"
            #region "do"
            #endregion
            #region "get"
                public List<NivelTitulo> sp_rrhh_getNivelesCarreras(int idUsuarioEjecutor,int idPagina)
                {
                    List<NivelTitulo> nivelesTitulos = null; NivelTitulo nivelTitulo;
                    SPIUS sp = new SPIUS("sp_rrhh_getNivelesCarreras");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                nivelesTitulos = new List<NivelTitulo>();
                                foreach(DataRow row in tb[0].Rows){
                                    nivelTitulo = new NivelTitulo((int)row["idNivel"], row["nombre_nivel"].ToString());
                                    nivelesTitulos.Add(nivelTitulo);
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
                    return nivelesTitulos;
                }
            #endregion
        #endregion
    }
}
