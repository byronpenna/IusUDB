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
    public class ControlArchivo:PadreLib
    {
        public Archivo sp_repo_uploadFile(Archivo archivoAgregar,int idUsuarioEjecutor,int idPagina)
        {
            Archivo archivoAgregado=null; ExtensionArchivo extension; TipoArchivo tipoArchivo;
            SPIUS sp = new SPIUS("sp_repo_uploadFile");
            sp.agregarParametro("nombre", archivoAgregar._nombre);
            if (archivoAgregar._carpeta._idCarpeta != -1)
            {
                sp.agregarParametro("idCarpeta", archivoAgregar._carpeta._idCarpeta);
            }
            else
            {
                sp.agregarParametro("idCarpeta", null);
            }
            sp.agregarParametro("src", archivoAgregar._src);
            sp.agregarParametro("extension", archivoAgregar._extension._extension);
            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrecto(tb)) {
                    if (tb[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in tb[1].Rows)
                        {
                            tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                            tipoArchivo._icono = row["icono"].ToString();
                            extension = new ExtensionArchivo((int)row["id_extension_fk"],tipoArchivo);
                            int idCarpeta;
                            if(row["id_carpeta_fk"] == DBNull.Value){
                                idCarpeta = 0;
                            }
                            else
                            {
                                idCarpeta = (int)row["id_carpeta_fk"];
                            }
                            archivoAgregado = new Archivo((int)row["idArchivo"], row["nombre"].ToString(), idCarpeta, row["src"].ToString(), extension);
                            
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
            return archivoAgregado;
        }
    }
}
