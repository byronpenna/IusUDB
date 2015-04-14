using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos 
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.TRL.Entidades;
    using IUSLibs.BaseDatos;
    using IUSLibs.LOGS;
    using IUSLibs.GENERALS;
namespace IUSLibs.TRL.Control
{
    public class ControlLlave:PadreLib
    {
        #region "propiedades"

        #endregion
        #region "constructores"

        #endregion
        #region "funciones"
            public List<Llave> sp_trl_getLlaveFromPage(int idPaginaFront,int idUsuarioEjecutor,int idPagina)
            {
                List<Llave> llaves = null;
                Llave llave; Pagina pagina;// clases genericas
                SPIUS sp = new SPIUS("sp_trl_getLlaveFromPage");
                sp.agregarParametro("idPaginaFront",idPaginaFront);
                sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                sp.agregarParametro("idPagina", idPagina);
                try
                {
                    DataSet ds = sp.EjecutarProcedimiento();
                    if (!this.DataSetDontHaveTable(ds))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            llaves = new List<Llave>();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                pagina = new Pagina((int)row["id_pagina_fk"],true);
                                llave = new Llave((int)row["idLlave"],row["llave"].ToString(), pagina);
                                llaves.Add(llave);
                            }
                            
                        }
                        
                    }
                }
                catch (ErroresIUS x) 
                {
                    throw x;
                }
                catch (Exception x) {
                    throw x;
                }
                return llaves;
            }
        #endregion
    }
}
