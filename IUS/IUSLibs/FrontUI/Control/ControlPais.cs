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
    public class ControlPais:PadreLib
    {
        #region "funciones"
        #region "frontend"
            #region "get"
                public List<Pais> sp_frontui_getPaisesFromContinente(int idContinente,string lang,string ip,int idPagina)
                {
                    List<Pais> paises = null; Pais pais;
                    SPIUS sp = new SPIUS("sp_frontui_getPaisesFromContinente");
                    
                    sp.agregarParametro("idContinente", idContinente);
                    sp.agregarParametro("idioma", lang);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                paises = new List<Pais>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    pais = new Pais((int)row["idPais"], row["pais"].ToString(), (int)row["id_continente_fk"]);
                                    paises.Add(pais);
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
                    return paises;
                }
                public List<Pais> sp_frontui_getPaises()
                {
                    List<Pais> paises = null;
                    SPIUS sp = new SPIUS("sp_frontui_getPaises");
                    Pais pais;Continente continente;
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                paises = new List<Pais>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    continente = new Continente((int)row["id_continente_fk"],row["continente"].ToString());
                                    pais = new Pais((int)row["idPais"], row["pais"].ToString(), continente);
                                    paises.Add(pais);
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
                    catch(ErroresIUS x){
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return paises;
                }
            #endregion
        #endregion
        #endregion
    }
}
