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

    using IUSLibs.REPO.Entidades;
namespace IUSLibs.REPO.Pantalla
{
    class PantallaControlConfig:PadreLib
    {
        public Dictionary<object, object> sp_repo_inicialesConfigRepo(string lang,int idUsuarioEjecutor,int idPagina)
        {
            Dictionary<object, object> retorno = null;
            /*
                @				varchar(10),
	            -- segurdad 
	            @idUsuarioEjecutor	int,
	            @idPagina			int
             */
            List<ExtensionArchivo> extensiones = null;
            ExtensionArchivo extension;
            SPIUS sp = new SPIUS("sp_repo_inicialesConfigRepo");
            sp.agregarParametro("lang", lang);
            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrectoGet(tb))
                {
                    if (tb[0].Rows.Count > 0)
                    {
                        foreach(DataRow row in tb[0].Rows){
                            extension = new ExtensionArchivo(row[""])
                        }
                }
            }
        }
    }
}
