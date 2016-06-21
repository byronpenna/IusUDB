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
    //---
    using IUSLibs.RRHH.Entidades.Formacion;
namespace IUSLibs.FrontUI.Control
{
    public class ControlInstitucion:PadreLib
    {
        #region "private"
            private List<TelefonoInstitucion> getTelefonosByInstitucion(int idInstitucion,string ip,int idPagina)
            {
                List<TelefonoInstitucion> telefonos = null;
                try
                {
                    ControlTelefonoInstitucion controlTel = new ControlTelefonoInstitucion();
                    telefonos = controlTel.sp_frontui_spFront_getTelByInstitucion(idInstitucion, ip, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return telefonos;
            }
            private List<EnlaceInstitucion> getEnlaceByInstitucion(int idInstitucion,string ip,int idPagina)
            {
                List<EnlaceInstitucion> enlaces = null;
                try
                {
                    ControlEnlaceInstitucion controlEnlace = new ControlEnlaceInstitucion();
                    enlaces = controlEnlace.sp_frontui_spFront_getEnlacesByInstitucion(idInstitucion, ip, idPagina);
                }
                catch (ErroresIUS)
                {

                }
                catch (Exception)
                {

                }
                return enlaces;
            }
        #endregion
        #region "get"
            #region "frontend"
                public Dictionary<object,object> sp_frontui_getInstitucionesByContinente(int idContinente,string idioma,string ip,int idPagina)
                {
                    Dictionary<object, object> respuesta = new Dictionary<object, object>();
                    List<Institucion> instituciones = null; Institucion institucion; int idInstitucion;
                    Continente continente=null;
                    SPIUS sp = new SPIUS("sp_frontui_getInstitucionesByContinente");
                    
                    sp.agregarParametro("idContinente", idContinente);
                    sp.agregarParametro("idioma", idioma);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                instituciones = new List<Institucion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    Pais pais = new Pais((int)row["id_pais_fk"], row["pais"].ToString());
                                    idInstitucion = (int)row["idInstitucion"];
                                    List<TelefonoInstitucion> telefonos = this.getTelefonosByInstitucion(idInstitucion,ip,idPagina);
                                    List<EnlaceInstitucion> enlaces = this.getEnlaceByInstitucion(idInstitucion, ip, idPagina);
                                    institucion = new Institucion(idInstitucion, row["nombre"].ToString(), row["direccion"].ToString(), pais, (bool)row["estado"]);
                                    institucion._telefonos = telefonos;
                                    institucion._enlaces = enlaces;
                                    institucion._ciudad = row["ciudad"].ToString();
                                    if (row["logo"] != DBNull.Value)
                                    {
                                        institucion._logo = (byte[])row["logo"];
                                    }
                                    instituciones.Add(institucion);
                                }
                            }
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                continente = new Continente((int)row["idContinente"], row["continente"].ToString());

                            }
                        }
                        respuesta.Add("continente", continente);
                        respuesta.Add("instituciones", instituciones);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return respuesta;
                }
                public List<Institucion> sp_frontui_getInstituciones(int idUsuarioEjecutor, int idPagina)
                {
                    List<Institucion> instituciones = null;
                    Institucion institucion;
                    SPIUS sp = new SPIUS("sp_frontui_getInstituciones");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                instituciones = new List<Institucion>();
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    Pais pais = new Pais((int)row["id_pais_fk"], row["pais"].ToString());
                                    institucion = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), pais, (bool)row["estado"]);
                                    institucion._ciudad = row["ciudad"].ToString();
                                    institucion._anioFundacion = (int)row["anio_fundacion"];
                                    institucion._tipoInstitucion = new TipoInstitucion((int)row["idTipoInstitucion"], row["tipoInstitucion"].ToString());
                                    instituciones.Add(institucion);
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
                    return instituciones;
                }
                public Dictionary<object, object> sp_frontui_front_getInstitucionById(int idInstitucion, string ip, int idPagina,string idioma = "es")
                {
                    // variables 
                        Dictionary<object, object> retorno = new Dictionary<object, object>();
                        // normales
                            Institucion institucion = null;
                            TelefonoInstitucion telefono;       EnlaceInstitucion   enlace;
                            NivelEducacion      nivelEducacion; AreaCarrera         areaCarrera;
                            
                            InstitucionNivel    institucionNivel;
                    // procedimiento
                        SPIUS sp = new SPIUS("sp_frontui_front_getInstitucionById");
                        sp.agregarParametro("idInstitucion", idInstitucion);
                        sp.agregarParametro("idioma", idioma);
                        sp.agregarParametro("ip", ip);
                        sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row                     = tb[0].Rows[0];
                                institucion                     = new Institucion((int)row["idInstitucion"],row["nombre"].ToString(),row["direccion"].ToString(),(int)row["id_pais_fk"],(bool)row["estado"]);
                                institucion._pais._pais         = row["pais"].ToString();
                                institucion._pais._continente   = new Continente((int)row["id_continente_fk"]);
                                if (row["ciudad"] == DBNull.Value)
                                {
                                    institucion._ciudad = "";
                                }
                                else
                                {
                                    institucion._ciudad = row["ciudad"].ToString();
                                }
                                if(DBNull.Value != row["logo"]){
                                    institucion._logo = (byte[])row["logo"];
                                }
                                if (tb[1].Rows.Count > 0) // telefono
                                {
                                    institucion._telefonos = new List<TelefonoInstitucion>();
                                    foreach (DataRow rowTelefono in tb[1].Rows)
                                    {
                                        telefono = new TelefonoInstitucion((int)rowTelefono["idTelefono"], rowTelefono["telefono"].ToString(), rowTelefono["texto_telefono"].ToString(), (int)rowTelefono["id_institucion_fk"]);
                                        telefono._institucion.getInstanciaPais((int)rowTelefono["idPais"]);
                                        //telefono._institucion._pais._idPais = ;
                                        string codigo = "";
                                        if (DBNull.Value != rowTelefono["codigo_pais"])
                                        {
                                            codigo = rowTelefono["codigo_pais"].ToString();
                                        }
                                        
                                        telefono._institucion._pais._codigoPais = codigo;
                                        institucion._telefonos.Add(telefono);
                                    }
                                }
                                if (tb[2].Rows.Count > 0) // enlace 
                                {
                                    institucion._enlaces = new List<EnlaceInstitucion>();
                                    foreach (DataRow rowEnlace in tb[2].Rows)
                                    {
                                        enlace = new EnlaceInstitucion((int)rowEnlace["idEnlace"], rowEnlace["enlace"].ToString(), rowEnlace["nombre_enlace"].ToString(), (int)rowEnlace["id_institucion_fk"]);
                                        institucion._enlaces.Add(enlace);
                                    }
                                }
                                if (tb[3].Rows.Count > 0) //niveles
                                {
                                    //institucion._niveles = new List<NivelEducacion>();
                                    institucion._institucionesNiveles = new List<InstitucionNivel>();

                                    foreach (DataRow rowNiveles in tb[3].Rows)
                                    {
                                        nivelEducacion = new NivelEducacion((int)rowNiveles["idNivelEducacion"], rowNiveles["codigo"].ToString(), rowNiveles["descripcion"].ToString());
                                        //institucion._niveles.Add(nivelEducacion);
                                        institucionNivel                    = new InstitucionNivel((int)rowNiveles["idInstitucionNivel"]);
                                        institucionNivel._nivelEducacion    = nivelEducacion;
                                        institucionNivel._numAlumnos = (int)rowNiveles["numAlumnos"];
                                        institucion._institucionesNiveles.Add(institucionNivel);
                                    }
                                }
                                if (tb[4].Rows.Count > 0) // areas de conocimiento
                                {
                                    institucion._areas = new List<AreaCarrera>();
                                    foreach (DataRow rowAreas in tb[4].Rows)
                                    {
                                        areaCarrera = new AreaCarrera((int)rowAreas["idArea"], rowAreas["area"].ToString(), rowAreas["codigo"].ToString());
                                        institucion._areas.Add(areaCarrera);
                                    }
                                }
                                retorno = new Dictionary<object, object>();
                                retorno.Add("institucion", institucion);
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
            #region "backend"
                public Institucion sp_frontui_getInstitucionById(int idInstitucion,int idUsuarioEjecutor,int idPagina)
                {
                    Institucion institucion = null; 
                    SPIUS sp = new SPIUS("sp_frontui_getInstitucionById");
                    sp.agregarParametro("idInstitucion", idInstitucion);
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
                                institucion = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), (int)row["id_pais_fk"], (bool)row["estado"]);
                                if (row["logo"] != DBNull.Value)
                                {
                                    institucion._logo = (byte[])row["logo"];
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
                    return institucion;
                }
            #endregion
        #endregion
        #region "acciones"

            #region "backend"
                #region "adicionales"
                    public List<AreaCarrera> sp_frontui_insertAreaConocimientoInstitucion(string strArea, int idInstitucion, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            List<AreaCarrera> areasCarreras = null;
                            AreaCarrera areaCarrera;
                            SPIUS sp = new SPIUS("sp_frontui_insertAreaConocimientoInstitucion");

                            sp.agregarParametro("strArea", strArea);
                            sp.agregarParametro("idInstitucion", idInstitucion);

                            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                            sp.agregarParametro("idPagina", idPagina);
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrecto(tb))
                            {
                                if (tb[1].Rows.Count > 0)
                                {
                                    areasCarreras = new List<AreaCarrera>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {
                                        areaCarrera = new AreaCarrera((int)row["idArea"], row["area"].ToString(), row["codigo"].ToString());
                                        areasCarreras.Add(areaCarrera);
                                    }
                                }
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
                            }
                            return areasCarreras;
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
                    public List<NivelEducacion> sp_frontui_insertNivelInstituciones(string strNiveles,string strNumAlumnos,int idInstitucion,int idUsuarioEjecutor,int idPagina)
                    {
                        try
                        {
                            List<NivelEducacion> nivelesEducacion = null;
                            NivelEducacion nivelEducacion;
                            SPIUS sp = new SPIUS("sp_frontui_insertNivelInstituciones");
                            
                            sp.agregarParametro("strNiveles", strNiveles);
                            sp.agregarParametro("strNumAlumnos", strNumAlumnos);

                            sp.agregarParametro("idInstitucion", idInstitucion);

                            sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                            sp.agregarParametro("idPagina", idPagina);
                            DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                            if (this.resultadoCorrecto(tb))
                            {
                                if (tb[1].Rows.Count > 0)
                                {
                                    nivelesEducacion = new List<NivelEducacion>();
                                    foreach (DataRow row in tb[1].Rows)
                                    {
                                        nivelEducacion = new NivelEducacion((int)row["idNivelEducacion"], row["codigo"].ToString(), row["descripcion"].ToString());
                                        nivelesEducacion.Add(nivelEducacion);
                                    }
                                }
                            }
                            else
                            {
                                DataRow row = tb[0].Rows[0];
                                ErroresIUS x = this.getErrorFromExecProcedure(row);
                                throw x;
                            }
                            return nivelesEducacion;
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

                public Institucion sp_frontui_editInstitucion(Institucion institucionEditar,int idUsuarioEjecutor,int idPagina)
                {
                    Institucion institucionEditada = null; Pais pais;
                    SPIUS sp = new SPIUS("sp_frontui_editInstitucion");
                    
                    sp.agregarParametro("nombre", institucionEditar._nombre);
                    sp.agregarParametro("direccion", institucionEditar._direccion);
                    sp.agregarParametro("idPais", institucionEditar._pais._idPais);
                    sp.agregarParametro("ciudad", institucionEditar._ciudad);
                    sp.agregarParametro("idInstitucion", institucionEditar._idInstitucion);
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
                                pais = new Pais((int)row["id_pais_fk"], row["pais"].ToString());
                                institucionEditada = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(), pais, (bool)row["estado"]);
                                institucionEditada._ciudad = row["ciudad"].ToString();
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
                    return institucionEditada;
                }
                public Institucion sp_frontui_getLogoInstitucion(int idInstitucion)
                {
                    Institucion institucionRetorno = null;
                    SPIUS sp = new SPIUS("sp_frontui_getLogoInstitucion");
                    sp.agregarParametro("idInstitucion", idInstitucion);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                DataRow row = tb[0].Rows[0];
                                institucionRetorno = new Institucion((int)row["idInstitucion"]);
                                institucionRetorno._logo = (byte[])row["logo"];
                            }
                        }
                        return institucionRetorno;
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
                public bool sp_frontui_setLogoInstitucion(Institucion institucionActualizar,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_setLogoInstitucion");
                    sp.agregarParametro("idInstitucion", institucionActualizar._idInstitucion);
                    sp.agregarParametro("image", institucionActualizar._logo);
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
                public bool sp_frontui_deleteInstitucion(int idInstitucion,int idUsuarioEjecutor,int idPagina)
                {
                    bool estado = false;
                    SPIUS sp = new SPIUS("sp_frontui_deleteInstitucion");
                    sp.agregarParametro("idInstitucion", idInstitucion);
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
                public Institucion sp_frontui_insertInstitucion(Institucion institucionAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    Institucion institucionAgregada = null; Pais pais;
                    SPIUS sp = new SPIUS("sp_frontui_insertInstitucion");
                    // parametros
                        sp.agregarParametro("nombre", institucionAgregar._nombre);
                        sp.agregarParametro("direccion", institucionAgregar._direccion);
                        sp.agregarParametro("idPais", institucionAgregar._pais._idPais);
                        sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                        sp.agregarParametro("ciudad", institucionAgregar._ciudad);
                        sp.agregarParametro("fundacion", institucionAgregar._anioFundacion);
                        sp.agregarParametro("idTipoInstitucion", institucionAgregar._tipoInstitucion._idTipoInstitucion);
                        sp.agregarParametro("idPagina", idPagina);
                    // acciones
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            if (tb[1].Rows.Count > 0)
                            {
                                DataRow row = tb[1].Rows[0];
                                pais = new Pais( (int)row["id_pais_fk"],row["pais"].ToString());
                                institucionAgregada = new Institucion((int)row["idInstitucion"], row["nombre"].ToString(), row["direccion"].ToString(),pais,(bool)row["estado"]);
                                institucionAgregada._ciudad = row["ciudad"].ToString();
                                institucionAgregada._anioFundacion = (int)row["anio_fundacion"];
                                institucionAgregada._tipoInstitucion = new TipoInstitucion((int)row["id_tipoinstitucion_fk"], row["tipoInstitucion"].ToString());
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
                    return institucionAgregada;
                }
            #endregion
        #endregion
    }
}
