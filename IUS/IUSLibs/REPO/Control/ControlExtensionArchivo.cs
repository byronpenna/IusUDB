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
namespace IUSLibs.REPO.Control
{
    public class ControlExtensionArchivo:PadreLib
    {
        #region "get"
        
        #endregion
        #region "do"
            public ExtensionArchivo sp_repo_actualizarTipoArchivoExt(ExtensionArchivo extension,int idUsuarioEjecutor,int idPagina )
            {
                /*
                 *  @idTipoArchivo		int,
	@idExtension		int,
	-- segurdad 
	@idUsuarioEjecutor	int,
	@idPagina			int
                 */
                ExtensionArchivo extensionEditada = null;
                SPIUS sp = new SPIUS("sp_repo_actualizarTipoArchivoExt");

                sp.agregarParametro("idTipoArchivo", extension._tipoArchivo._idTipoArchivo);
                sp.agregarParametro("idExtension", extension._idExtension);
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
                            extensionEditada = new ExtensionArchivo((int)row["idExtension"], row["extension"].ToString(), (int)row["id_tipoarchivo_fk"]);
                            extensionEditada._tipoArchivo._tipoArchivo = row["tipoArchivo"].ToString();
                        }
                    }
                    return extensionEditada;
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
        #endregion
    }
}
