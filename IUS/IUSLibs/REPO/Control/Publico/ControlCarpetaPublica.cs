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
    using IUSLibs.REPO.Entidades;
namespace IUSLibs.REPO.Control.Publico
{
    public class ControlCarpetaPublica:PadreLib
    {
        #region "funciones"
            #region "get"
                #region "frontend"
                    public CarpetaPublica sp_repo_front_getCarpetaPublicaByRuta(string ruta,string ip, int idPagina)
                    {
                        CarpetaPublica carpetaPublica = null;
                        SPIUS sp = new SPIUS("sp_repo_front_getCarpetaPublicaByRuta");
                        sp.agregarParametro("strRuta", ruta);
                        sp.agregarParametro("ip", ip);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    DataRow row = tb[0].Rows[0];
                                    carpetaPublica = new CarpetaPublica((int)row["idCarpetaPublica"]);
                                }
                                else
                                {
                                    ErroresIUS x = new ErroresIUS("No se encontro carpeta", ErroresIUS.tipoError.generico, 0, "", true);
                                    throw x; 
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
                        return carpetaPublica;
                    }
                    public CarpetaPublica sp_repo_front_getCarpetaPublicaFromId(int idCarpetaPadre, string ip, int idPagina)
                    {
                        CarpetaPublica carpetaPublica = null;
                        SPIUS sp = new SPIUS("sp_repo_front_getCarpetaPublicaFromId");
                        if (idCarpetaPadre != -1)
                        {
                            sp.agregarParametro("idCarpetaPadre", idCarpetaPadre);
                        }
                        else
                        {
                            sp.agregarParametro("idCarpetaPadre", null);
                        }
                        sp.agregarParametro("ip", ip);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    DataRow row = tb[0].Rows[0];
                                    carpetaPublica          = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString());
                                    carpetaPublica._strRuta = row["strRuta"].ToString();
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
                        return carpetaPublica;
                    }
                    public List<CarpetaPublica> sp_repo_front_GetAllCarpetasPublica(int idCarpetaPadre,string ip, int idPagina)
                    {
                        List<CarpetaPublica> carpetasPublicas = null; CarpetaPublica carpeta;
                        SPIUS sp = new SPIUS("sp_repo_front_GetAllCarpetasPublica");
                        if (idCarpetaPadre != -1)
                        {
                            sp.agregarParametro("idCarpetaPadre", idCarpetaPadre);
                        }
                        else
                        {
                            sp.agregarParametro("idCarpetaPadre", null);
                        }
                        sp.agregarParametro("ip", ip);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    carpetasPublicas = new List<CarpetaPublica>();
                                    foreach (DataRow row in tb[0].Rows)
                                    {
                                        CarpetaPublica carpetaPadre;
                                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                                        {
                                            carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                        }
                                        else
                                        {
                                            carpetaPadre = new CarpetaPublica();
                                        }
                                        carpeta = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                                        carpetasPublicas.Add(carpeta);
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
                        return carpetasPublicas;
                    } 
                #endregion
                #region "backend"
                    public CarpetaPublica sp_repo_getPublicoByRuta( string strRuta,int idUsuarioEjecutor, int idPagina)
                    {
                        CarpetaPublica carpeta= null;
                        SPIUS sp = new SPIUS("sp_repo_getPublicoByRuta");
                        sp.agregarParametro("strRuta", strRuta);
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    DataRow row = tb[0].Rows[0];
                                    carpeta = new CarpetaPublica((int)row["idCarpetaPublica"]);
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
                        return carpeta;
                    }
                    public Dictionary<object, object> sp_repo_atrasCarpetaPublica(int idCarpeta, int idUsuarioEjecutor, int idPagina)
                    {
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                        int idCarpetaPadre; CarpetaPublica carpetaPadreTotal;
                        List<CarpetaPublica> carpetas = null; CarpetaPublica carpeta;
                        CarpetaPublica carpetaPadre;
                        SPIUS sp = new SPIUS("sp_repo_atrasCarpetaPublica");
                        sp.agregarParametro("idCarpeta", idCarpeta);
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                idCarpetaPadre = (int)tb[0].Rows[0]["idCarpetaPadre"];
                                carpetaPadreTotal = new CarpetaPublica(idCarpetaPadre);
                                carpetaPadreTotal._strRuta = tb[0].Rows[0]["strRuta"].ToString();
                                if (tb[1].Rows.Count > 0)
                                {
                                    carpetas = new List<CarpetaPublica>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {
                                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                                        {
                                            carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                        }
                                        else
                                        {
                                            carpetaPadre = new CarpetaPublica();
                                        }
                                        carpeta = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                                        carpetas.Add(carpeta);
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
                        retorno.Add("idCarpetaPadre", idCarpetaPadre);
                        retorno.Add("carpetaPadre", carpetaPadreTotal);
                        retorno.Add("carpetas", carpetas);
                        return retorno;
                    }
                    public Dictionary<object, object> sp_repo_entrarCarpetaPublica(int idCarpeta, int idUsuarioEjecutor, int idPagina)
                    {
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                        // para carpetas
                        List<CarpetaPublica> carpetas = null; CarpetaPublica carpeta; Carpeta carpetaNormal; Usuario usuarioCarpeta;
                        CarpetaPublica carpetaPadre; Archivo archivoNormal; ExtensionArchivo extension; TipoArchivo tipoArchivo;
                        // para archivos
                        List<ArchivoPublico> archivos = null; ArchivoPublico archivo;
                        CarpetaPublica carpetaPublicaPadre=null;
                        // do
                        SPIUS sp = new SPIUS("sp_repo_entrarCarpetaPublica");
                        sp.agregarParametro("idCarpeta", idCarpeta);
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    carpetas = new List<CarpetaPublica>();
                                    foreach (DataRow row in tb[0].Rows)
                                    {
                                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                                        {
                                            carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                        }
                                        else
                                        {
                                            carpetaPadre = new CarpetaPublica();
                                        }
                                        carpeta = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                                        carpeta._fechaCreacion = (DateTime)row["fecha_creacion"];
                                        carpetas.Add(carpeta);
                                    }
                                }
                                if (tb[1].Rows.Count > 0)
                                {
                                    archivos = new List<ArchivoPublico>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {

                                        tipoArchivo = new TipoArchivo((int)row["idTipoArchivo"], row["tipoArchivo"].ToString());
                                        tipoArchivo._icono = row["icono"].ToString();
                                        extension = new ExtensionArchivo((int)row["idExtension"], tipoArchivo);
                                        usuarioCarpeta = new Usuario((int)row["idUsuario"],row["usuario"].ToString());
                                        carpetaNormal = new Carpeta((int)row["idCarpeta"], usuarioCarpeta);
                                        archivoNormal = new Archivo((int)row["idArchivo"], extension,carpetaNormal);
                                        archivo = new ArchivoPublico((int)row["idArchivoPublico"], archivoNormal, (int)row["id_carpetapublica_fk"], row["nombre_publico"].ToString(), (bool)row["estado"]);
                                        archivo._fechaCreacion = (DateTime)row["creacion_publica"];
                                        archivos.Add(archivo);
                                    }
                                }
                                if (tb[2].Rows.Count > 0)
                                {
                                    DataRow row = tb[2].Rows[0];
                                    carpetaPublicaPadre = new CarpetaPublica((int)row["idCarpetaPublica"],row["nombre"].ToString());
                                    carpetaPublicaPadre._fechaCreacion = (DateTime)row["fecha_creacion"];
                                    carpetaPublicaPadre._strRuta = row["strRuta"].ToString();
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
                        retorno.Add("carpetas", carpetas);
                        retorno.Add("archivos", archivos);
                        retorno.Add("carpetaPadre", carpetaPublicaPadre);
                        return retorno;
                    }
                    public List<CarpetaPublica> sp_repo_getRootFolderPublico(int idUsuarioEjecutor, int idPagina)
                    {
                        List<CarpetaPublica> carpetas = null; CarpetaPublica carpeta;
                        CarpetaPublica carpetaPadre;
                        SPIUS sp = new SPIUS("sp_repo_getRootFolderPublico");
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("idPagina", idPagina);
                        try
                        {
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrectoGet(tb))
                            {
                                if (tb[0].Rows.Count > 0)
                                {
                                    carpetas = new List<CarpetaPublica>();
                                    foreach (DataRow row in tb[0].Rows)
                                    {
                                        if (row["id_carpetapadre_fk"] != DBNull.Value)
                                        {
                                            carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                        }
                                        else
                                        {
                                            carpetaPadre = new CarpetaPublica();
                                        }
                                        carpeta = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                                        carpeta._fechaCreacion = (DateTime)row["fecha_creacion"];
                                        carpetas.Add(carpeta);
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
                        return carpetas;
                    }    
                #endregion
                
                
            #endregion
            #region "set"
                
                #region "backend"
                    public bool sp_repo_deleteCarpetaPublica(int idCarpetaPublica, int idUsuarioEjecutor, int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_repo_deleteCarpetaPublica");
                    sp.agregarParametro("idCarpeta", idCarpetaPublica);

                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            estado = true;
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
                    return estado;
                }
                    public CarpetaPublica sp_repo_updateCarpetaPublica(CarpetaPublica carpetaPublicaUpdate, int idUsuarioEjecutor, int idPagina)
                {
                    CarpetaPublica carpetaPublica = null;
                    SPIUS sp = new SPIUS("sp_repo_updateCarpetaPublica");
                    
                    sp.agregarParametro("nombre", carpetaPublicaUpdate._nombre);
                    sp.agregarParametro("idCarpeta",carpetaPublicaUpdate._idCarpetaPublica);

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
                                CarpetaPublica carpetaPadre;
                                if (row["id_carpetapadre_fk"] != DBNull.Value)
                                {
                                    carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                }
                                else
                                {
                                    carpetaPadre = new CarpetaPublica();
                                }
                                carpetaPublica = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
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
                    return carpetaPublica;
                }
                    public CarpetaPublica sp_repo_insertCarpetaPublica(CarpetaPublica carpetaPublicaInsert,int idUsuarioEjecutor, int idPagina)
                {
                    CarpetaPublica carpetaPublica = null;
                    SPIUS sp = new SPIUS("sp_repo_insertCarpetaPublica");

                    sp.agregarParametro("nombre", carpetaPublicaInsert._nombre);
                    if(carpetaPublicaInsert._carpetaPadre._idCarpetaPublica > 0){
                        sp.agregarParametro("idCarpetaPadre", carpetaPublicaInsert._carpetaPadre._idCarpetaPublica);
                    }
                    else
                    {
                        sp.agregarParametro("idCarpetaPadre", null);
                    }
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
                                CarpetaPublica carpetaPadre;
                                if(row["id_carpetapadre_fk"] != DBNull.Value){
                                    carpetaPadre = new CarpetaPublica((int)row["id_carpetapadre_fk"]);
                                }else{
                                    carpetaPadre = new CarpetaPublica();
                                }
                                carpetaPublica = new CarpetaPublica((int)row["idCarpetaPublica"], row["nombre"].ToString(), carpetaPadre);
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
                            }
                        }
                        else
                        {

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
                    return carpetaPublica;

                }
                #endregion
            #endregion
        #endregion
    }
}
