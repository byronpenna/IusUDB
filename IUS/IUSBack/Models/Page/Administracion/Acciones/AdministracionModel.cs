﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades;
    using IUSLibs.ADMINFE.Control;
    using IUSLibs.SEC.Entidades;
    
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class AdministracionModel:PadreModel
    {
        #region "constructores"
            public AdministracionModel()
            {
                this._controlEvento                 = new ControlEvento();
                this._controlUsuarioEvento          = new ControlUsuarioEvento();
                this._controlPermisoUsuarioEvento   = new ControlPermisoUsuarioEvento();
            }
        #endregion
        #region "propiedades"
            public ControlEvento _controlEvento;
            public ControlUsuarioEvento _controlUsuarioEvento;
            public ControlPermisoUsuarioEvento _controlPermisoUsuarioEvento;
        #endregion 
        #region "funciones publicas"
            #region "eventos"
                #region "gets"
                    public List<Usuario> sp_adminfe_getUsuariosFaltantesEvento(int idEvento,int idUsuarioEjecutor,int idPagina)
                    {
                        try
                        {
                            return this._controlUsuarioEvento.sp_adminfe_getUsuariosFaltantesEvento(idEvento, idUsuarioEjecutor, idPagina);
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
                    public Dictionary<object,object> sp_adminfe_getPermisosUsuarioEvento(int idUsuarioEvento,int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlPermisoUsuarioEvento.sp_adminfe_getPermisosUsuarioEvento(idUsuarioEvento,idUsuarioEjecutor, idPagina);
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
                    public List<Evento> sp_adminfe_eventosPropios(int idUsuario, int idPagina)
                    {
                        List<Evento> eventos = null;
                        ControlEvento control = new ControlEvento();
                        try
                        {
                            eventos = control.sp_adminfe_eventosPropios(idUsuario,idPagina);
                        }
                        catch (ErroresIUS)
                        {

                        }
                        catch (Exception)
                        {

                        }
                        return eventos;
                    }
                    public Dictionary<string,object> sp_adminfe_loadCompartirEventos(int idEvento, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlUsuarioEvento.sp_adminfe_loadCompartirEventos(idEvento, idUsuarioEjecutor, idPagina);
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
                #region "edicion"
                    public Evento sp_adminfe_editarEventos(Evento eventoAgregado,int idUsuarioEjecutor,int idPagina)
                    {
                        ControlEvento control = new ControlEvento();
                        try
                        {
                            return control.sp_adminfe_editarEventos(eventoAgregado, idUsuarioEjecutor, idPagina);
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
                #region "eliminacion"
                    public bool sp_adminfe_removeUsuarioEvento(int idUsuarioEvento,int idUsuarioEjecutor)
                    {
                        try
                        {
                            return this._controlUsuarioEvento.sp_adminfe_removeUsuarioEvento(idUsuarioEvento, idUsuarioEjecutor);
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
                    public EventoWebsite sp_adminfe_quitarEventoWebsite(int idEventoQuitar, string motivo, int idUsuarioEjecutor, int idPagina)
                    {
                        ControlEventoWebsite control = new ControlEventoWebsite();
                        try
                        {
                            return control.sp_adminfe_quitarEventoWebsite(idEventoQuitar, motivo, idUsuarioEjecutor, idPagina);
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
                #region "creacion"
                    public List<PermisoUsuarioEvento> sp_adminfe_agregarPermisoUsuarioEvento(int[] idPermisos,int idUsuarioEvento,int idUsuarioEjecutor,int idPagina)
                    {
                        List<PermisoUsuarioEvento> toReturn = null;
                        PermisoUsuarioEvento permisoUsuarioEvento; PermisoEvento permiso;
                        PermisoUsuarioEvento agregado;
                        UsuarioEvento usuarioEvento = new UsuarioEvento(idUsuarioEvento);
                        bool estadoGeneral = false;
                        try{
                            // try catch general para un error fatal;
                            foreach (int idPermiso in idPermisos)
                            {
                                permiso = new PermisoEvento(idPermiso);
                                permisoUsuarioEvento = new PermisoUsuarioEvento(usuarioEvento,permiso);
                                try
                                {
                                    agregado = this._controlPermisoUsuarioEvento.sp_adminfe_agregarPermisoUsuarioEvento(permisoUsuarioEvento, idUsuarioEjecutor, idPagina);
                                    if (agregado != null)
                                    {
                                        estadoGeneral = true;
                                        toReturn.Add(agregado);
                                    }
                                }
                                catch (ErroresIUS)
                                {

                                }
                                catch (Exception)
                                {

                                }
                            }
                            if (!estadoGeneral)
                            {
                                ErroresIUS x = new ErroresIUS("No agrego ningun permiso", ErroresIUS.tipoError.generico, 0);
                                throw x;
                            }
                        }catch(ErroresIUS x){
                            throw x;
                        }catch(Exception x){
                            throw x;
                        }
                        return toReturn;
                    }
                    public EventoWebsite sp_adminfe_publicarEventoWebsite(Evento eventoAgregar, int idUsuarioEjecutor, int idPagina)
                    {
                        EventoWebsite eventoPublicado = null;
                        ControlEventoWebsite control = new ControlEventoWebsite();
                        try
                        {
                            eventoPublicado = control.sp_adminfe_publicarEventoWebsite(eventoAgregar, idUsuarioEjecutor, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                        return eventoPublicado;
                    }
                    public Evento sp_adminfe_crearEvento(Evento eventoAgregar,int idUsuarioEjecutor,int idPagina)
                    {
                        Evento eventoAgregado = null;
                        ControlEvento control = new ControlEvento();
                        try
                        {
                            eventoAgregado = control.sp_adminfe_crearEvento(eventoAgregar, idUsuarioEjecutor, idPagina);
                        }
                        catch (ErroresIUS x)
                        {
                            throw x;
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                        return eventoAgregado;
                    }
                    public UsuarioEvento sp_adminfe_compartirEventoUsuario(UsuarioEvento agregar, int idUsuarioEjecutor, int idPagina)
                    {
                        try
                        {
                            return this._controlUsuarioEvento.sp_adminfe_compartirEventoUsuario(agregar, idUsuarioEjecutor, idPagina);
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
            #endregion
            #region "Noticias"

            #endregion
        #endregion

    }
}