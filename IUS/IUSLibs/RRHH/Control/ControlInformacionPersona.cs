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
        using IUSLibs.RRHH.Entidades;
        using IUSLibs.FrontUI.Entidades;
namespace IUSLibs.RRHH.Control
{
    public class ControlInformacionPersona:PadreLib
    {
        #region "get"
            public Dictionary<object, object> sp_rrhh_getInformacionPersonas(int idPersona, int idUsuarioEjecutor, int idPagina)
            {
                Dictionary<object, object> retorno = null;
                // variables
                List<Pais> paises; Pais pais;/**/ List<EstadoCivil> estadosCiviles;EstadoCivil estadoCivil;
                // trayendo
                SPIUS sp = new SPIUS("sp_repo_deleteFile");
                sp.agregarParametro("idPersona", idUsuarioEjecutor);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            paises = new List<Pais>();
                            // paises 
                            foreach(DataRow row in tb[0].Rows ){
                                pais = new Pais((int)row["idPais"], row["pais"].ToString());
                                paises.Add(pais);
                            }
                        }
                        if (tb[1].Rows.Count > 0)
                        {
                            // estados civiles 
                            estadosCiviles = new List<EstadoCivil>();
                            foreach (DataRow row in tb[1].Rows) {
                                estadoCivil = new EstadoCivil((int)row["idEstadoCivil"],row["estado_civil"].ToString());
                                estadosCiviles.Add(estadoCivil);
                            }
                        }
                        if (tb[0].Rows.Count > 0)
                        {
                            // Informacion personas 
                            DataRow row = tb[0].Rows[0];
                        }
                        if (tb[0].Rows.Count > 0)
                        {
                            // mails 
                            DataRow row = tb[0].Rows[0];
                        }
                        if (tb[0].Rows.Count > 0)
                        {
                            // telefonos 
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
                return retorno;
            }
        #endregion
        #region "do"
            
        #endregion
    }
}
