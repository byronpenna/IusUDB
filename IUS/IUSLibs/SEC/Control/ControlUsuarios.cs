using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
// librerias internas
    using IUSLibs.GENERALS;
    
    using IUSLibs.BaseDatos;
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSLibs.SEC.Control
{
    public class ControlUsuarios:PadreLib
    {
        #region "propiedades"
            private Usuario _usuario;
            private List<Submenu> _subMenu = new List<Submenu>();
            private Permiso _permiso;
            #region "Get y set"
                public List<Submenu> getSubMenu
                {
                    get
                    {
                        return this._subMenu;
                    }

                }
                public Usuario getUsuario
                {
                    get
                    {
                        return this._usuario;
                    }
                }
                public Permiso permisoGestion
                {
                    get
                    {
                        Permiso permisoTmp = this._permiso;
                        this._permiso = null;
                        return permisoTmp;
                    }
                }
            #endregion
        #endregion
        #region "funciones privadas"
            private Usuario getObjectoUsuarioDeRow(DataRow row)
            {
                Usuario usu;
                Persona persona;
                persona = new Persona((int)row["id_persona_fk"], row["nombres"].ToString(), row["apellidos"].ToString());
                usu = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), (DateTime)row["fecha_creacion"], (bool)row["estado"], persona);
                return usu;
            }
            private Dictionary<string, Object> getParametrosActualizarUsuarios(Usuario usuario,int idUsuarioEjecutor,int idPagina)
            {
                Dictionary<string, Object> toReturn = new Dictionary<string, object>();
                toReturn.Add("idUsuario",usuario._idUsuario);
                toReturn.Add("usuario",usuario._usuario);
                toReturn.Add("idPersona",usuario._persona._idPersona);
                toReturn.Add("idPagina",idUsuarioEjecutor);
                toReturn.Add("usuarioEjecutor", idPagina);
                return toReturn;
            }
        #endregion
        #region "Funciones publicas"
            public bool permisoPagina(int idUsuario,int idPagina,int nivelPermiso)
            {
                bool toReturn = false;
                SPIUS sp = new SPIUS("sp_sec_tienePemiso");
                sp.agregarParametro("idUsuario", idUsuario);
                sp.agregarParametro("idPagina", idPagina);
                sp.agregarParametro("nivelPermiso", nivelPermiso);
                DataSet ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    bool tienePermiso = (bool)row["tienePermiso"];
                    if (tienePermiso)
                    {
                        toReturn = true;
                    }
                }
                return toReturn;
            }
            #region "getUsuarios"
                private Permiso setPemisos(DataRowCollection rows)
                {
                    Permiso permisos;
                    bool edicion=false, creacion=false, eliminacion=false;
                    foreach (DataRow row in rows)
                    {
                        switch ((int)row["permiso"])
                        {
                            case 1:
                                {
                                    creacion = true;
                                    break;
                                }
                            case 2:
                                {
                                    edicion = true;
                                    break;
                                }
                            case 3:
                                {
                                    eliminacion = true;
                                    break;
                                }
                        }
                    }
                    permisos = new Permiso(edicion, creacion, eliminacion);
                    return permisos;
                }
                public List<Usuario> getUsuarios(int idUsuarioEjecutor,int idPagina)
                {
                    List<Usuario> usuarios = this.getUsuarios(idUsuarioEjecutor,idPagina,-1, -1);
                    return usuarios;
                }
                public List<Usuario> sp_sec_getAllUsuarios(int idUsuarioEjecutor, int idPagina)
                {
                    List<Usuario> usuarios = new List<Usuario>();Usuario usuario;
                    SPIUS sp = new SPIUS("sp_sec_getAllUsuarios");
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrectoGet(tb))
                        {
                            if (tb[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in tb[0].Rows)
                                {
                                    usuario = new Usuario((int)row["idUsuario"], row["usuario"].ToString());
                                    usuarios.Add(usuario);
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
                    return usuarios;
                }
                public List<Usuario> getUsuarios(int idUsuarioEjecutor, int idPagina, int l1, int l2)
                {
                    List<Usuario> usuarios = new List<Usuario>();
                    Usuario usuario;
                    SPIUS sp = new SPIUS("sp_sec_getUsuarios");
                    sp.agregarParametro("l1", l1);
                    sp.agregarParametro("l2", l2);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    try
                    {
                        DataSet ds = sp.EjecutarProcedimiento();
                        DataTableCollection tablas = this.getTables(ds);
                        if (tablas != null)
                        {
                            DataTable tb = tablas[1];
                            if (tb.Rows.Count > 0)
                            {
                                foreach (DataRow row in tb.Rows)
                                {
                                    usuario = this.getObjectoUsuarioDeRow(row);
                                    usuarios.Add(usuario);
                                }
                            }
                            else
                            {
                                usuarios = null;
                            }
                        }
                        else
                        {
                            usuarios = null;
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
                    return usuarios;
                }
            #endregion
            #region "Acciones"
                public Usuario sp_sec_agregarUsuario(Usuario usuarioAgregar,int idUsuarioEjecutor,int idPagina)
                {
                    Usuario usuarioAgregado = null;
                    SPIUS sp = new SPIUS("sp_sec_agregarUsuario");
                    int idPersona = usuarioAgregar._persona._idPersona;
                    sp.agregarParametro("usuario", usuarioAgregar._usuario);
                    sp.agregarParametro("pass", usuarioAgregar._pass);
                    sp.agregarParametro("fechaCreacion", usuarioAgregar._fechaCreacion);
                    sp.agregarParametro("estado", usuarioAgregar._estado);
                    sp.agregarParametro("idUsuarioEjecutor", idUsuarioEjecutor);
                    sp.agregarParametro("idPagina", idPagina);
                    if(idPersona != -1){
                        sp.agregarParametro("idPersona", idPersona);
                    }
                    else
                    {
                        sp.agregarParametro("idPersona",DbType.Int32);
                    }
                    try
                    {
                        DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                        if (this.resultadoCorrecto(tb))
                        {
                            DataRow rowResultado = tb[1].Rows[0];
                            usuarioAgregado = this.getObjectoUsuarioDeRow(rowResultado);
                            
                        }
                        else
                        {
                            DataRow rowError = tb[0].Rows[0];
                            ErroresIUS x = new ErroresIUS(rowError["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)rowError["errorCode"], rowError["errorSql"].ToString());
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
                    return usuarioAgregado;
                }
                public bool actualizarUsuario(List<Usuario> usuarios, int idUsuarioEjecutor, int idPagina)
            {
                bool estado = false;
                SPIUS sp = new SPIUS("sp_sec_updateUsuarioParaMultiple");
                foreach (Usuario usuario in usuarios)
                {
                    Dictionary<string, Object> parametros = this.getParametrosActualizarUsuarios(usuario, idUsuarioEjecutor, idPagina);
                    sp.agregarParametro(parametros);
                }
                
                try
                {
                    estado = sp.ejecutarInsertMultiple();
                }catch(ErroresIUS x){
                    throw x;
                }catch(Exception x)
                {
                    throw x;
                }
                return estado;
            }
                public Usuario actualizarUsuario(Usuario usu,int idUsuarioEjecutor,int idPagina)
            {
                Usuario toReturn = null;
                SPIUS sp = new SPIUS("sp_sec_updateUsuario");
                sp.agregarParametro("idUsuario",usu._idUsuario);
                sp.agregarParametro("usuario",usu._usuario);
                sp.agregarParametro("idPersona",usu._persona._idPersona);
                sp.agregarParametro("idPagina",idPagina);
                sp.agregarParametro("usuarioEjecutor", idUsuarioEjecutor);
                DataSet ds = sp.EjecutarProcedimiento();
                if (!this.DataSetDontHaveTable(ds))
                {
                    DataTable tablaEstado = ds.Tables[0];
                    if (Convert.ToBoolean((int)tablaEstado.Rows[0]["estadoUpdate"]))
                    {
                        DataRow rowUsuario = ds.Tables[1].Rows[0];
                        Persona persona = new Persona((int)rowUsuario["id_persona_fk"],rowUsuario["nombres"].ToString(),rowUsuario["apellidos"].ToString());
                        toReturn = new Usuario((int)rowUsuario["idUsuario"], rowUsuario["usuario"].ToString(), persona,(bool)rowUsuario["estado"]);
                    }
                    else
                    {
                        ErroresIUS x = new ErroresIUS(tablaEstado.Rows[0]["errorMessage"].ToString(), ErroresIUS.tipoError.sql, (int)tablaEstado.Rows[0]["errorCode"]);
                        throw x;
                    }
                }
                return toReturn;
            }
            #endregion
            
            public bool /*Submenu*/ getTodoMenu(int idUsuario)
            {
                // true significa que posee menus y false que no posee ninguno
                bool toReturn = false;
                List<Submenu> s = new List<Submenu>();
                Submenu subMenu;
                Menu menu;
                SPIUS sp = new SPIUS("sp_sec_getSubmenuRol");
                sp.agregarParametro("idUsuario", idUsuario);
                DataSet ds = sp.EjecutarProcedimiento();
                DataTable tb = ds.Tables[0];
                if (tb.Rows.Count > 0)
                {
                    toReturn = true;
                    foreach (DataRow row in tb.Rows)
                    {
                        
                        menu = new Menu((int)row["idMenu"], row["menu"].ToString(), row["enlaceMenu"].ToString());
                        subMenu = new Submenu((int)row["idSubMenu"], menu, row["submenu"].ToString(), row["enlace"].ToString());
                        //this._subMenu.Add(subMenu);
                        s.Add(subMenu);
                    }
                }
                this._subMenu = s;
                return toReturn;
            }
            
            public bool login(string usuario,string pass)
                {
                    bool toReturn = false;
                    SPIUS sp = new SPIUS("sp_sec_login");
                    sp.agregarParametro("usuario", usuario);
                    sp.agregarParametro("pass", pass);
                    DataSet ds = sp.EjecutarProcedimiento();
                    DataTable tb = ds.Tables[0];
                    if (tb.Rows.Count == 1)
                    {
                        toReturn = true;
                        DataRow row = tb.Rows[0];
                        Persona persona = new Persona((int)row["id_persona_fk"],row["nombres"].ToString(),row["apellidos"].ToString(),(DateTime)row["fecha_nacimiento"]);
                        this._usuario = new Usuario((int)row["idUsuario"], row["usuario"].ToString(), (DateTime)row["fecha_creacion"],true, persona, row["pass"].ToString());
                    }
                    return toReturn;
                }
            public Usuario cambiarEstadoUsuario(int idUsuario,int subMenu,int usuarioEjecutor)
            {
                Usuario usu;
                SPIUS sp = new SPIUS("sp_sec_cambiarEstadoUsuario");
                sp.agregarParametro("idUsuarioModificar", idUsuario);
                sp.agregarParametro("idSubMenu",subMenu);
                sp.agregarParametro("usuarioEjecutor",usuarioEjecutor);
                DataSet ds = sp.EjecutarProcedimiento();
                DataTable resultado = ds.Tables[0];
                String modifico = resultado.Rows[0]["modifico"].ToString();
                if (modifico == "1")
                {
                    DataRow drUsuario = ds.Tables[1].Rows[0];
                    usu = new Usuario((int)drUsuario["idUsuario"],drUsuario["usuario"].ToString(),(bool)drUsuario["estado"]);
                }
                else
                {
                    usu = null;
                }
                return usu;
            }
        #endregion
        #region "Constructores"
        public ControlUsuarios()
        {
            
        }
        #endregion
    }
}
