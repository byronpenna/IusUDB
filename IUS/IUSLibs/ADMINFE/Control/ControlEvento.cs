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
    public class ControlEvento:PadreLib
    {
        #region "propiedades"

        #endregion 
        #region "funciones"
            #region "gets"
                
            #endregion
            #region "acciones"
                public Evento sp_adminfe_crearEvento(Evento eventoAgregar,int idUsuario,int idPagina)
                {
                    Evento eventoAgregado = null;
                    Usuario usu;
                    SPIUS sp = new SPIUS("sp_adminfe_crearEvento");
                    sp.agregarParametro("evento", eventoAgregar._evento);
                    sp.agregarParametro("fecha_inicio", eventoAgregar._fechaInicio);
                    sp.agregarParametro("fecha_fin", eventoAgregar._fechaFin);
                    sp.agregarParametro("descripcion", eventoAgregar._descripcion);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuario);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        DataRow rowResultado = tb[1].Rows[0];
                        if(this.resultadoCorrecto(tb)){
                            usu = new Usuario((int) rowResultado["id_usuario_creador_fk"]);
                            eventoAgregado = new Evento((int)rowResultado["idEvento"], rowResultado["evento"].ToString(), (DateTime)rowResultado["fecha_inicio"], (DateTime)rowResultado["fecha_fin"],usu, (DateTime)rowResultado["fecha_creacion"], rowResultado["descripcion"].ToString());
                        }
                    }catch(ErroresIUS x){
                        throw x;
                    }catch(Exception x){
                        throw x;
                    }
                    return eventoAgregado;
                }
            #endregion
        #endregion
    }
}
