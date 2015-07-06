using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// liberias
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.REPO.Entidades.Publico;    
namespace IUSLibs.REPO.Control.Publico
{
    public class ControlArchivoPublico:PadreLib
    {
        #region "get"
        public ArchivoPublico sp_repo_compartirArchivoPublico(ArchivoPublico archivoAgregar,int idUsuarioEjecutor, int idPagina)
        {
            ArchivoPublico archivoCompartido = null;
            SPIUS sp = new SPIUS("sp_repo_compartirArchivoPublico");
            /*
                @			int,
	            @	int,
	            @		varchar(100),
             */
            sp.agregarParametro("idArchivo", archivoAgregar._idArchivoPublico);
            sp.agregarParametro("idCarpetaPublica", archivoAgregar._carpetaPublica._idCarpetaPublica);
            sp.agregarParametro("nombrePublico", archivoAgregar._nombre);

            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
            sp.agregarParametro("idPagina", idPagina);
            return archivoAgregar;
        }
        #endregion
        #region "set"
            
        #endregion
    }
}
