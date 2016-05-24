using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// datos
    using System.Data;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class Evento
    {
        #region "propiedades"
            // propiedades de tabla
                public int      _idEvento;
                public string   _evento;
                public DateTime _fechaInicio;
                public DateTime _fechaFin;
                public Usuario  _usuarioCreador;
                public DateTime _fechaCreacion;
                public string   _descripcion;

                //public byte[]   _miniatura;
            // extras 
                public int      _propietario; // 1: evento propio, 2: evento compartido, 3: evento publico
                public bool     _publicado; // este no va en constructor porque no va con la naturalesa de la tabla
            #region "Con get y set"
                public string getFechaInicioUSA
                {
                    get
                    {
                        return String.Format("{0:yyyy-MM-dd HH:mm:ss}", this._fechaInicio);
                    }
                }
                public string getFechaFinUSA
                {
                    get
                    {
                        return String.Format("{0:yyyy-MM-dd HH:mm:ss}", this._fechaFin);
                    }
                }
                public string getFechaInicio
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}",this._fechaInicio);
                    }
                }
                public string getFechaFin
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", this._fechaFin);
                    }
                }
                public string txtClaseColor
                {
                    get
                    {
                        string claseColor = "";
                        switch (this._propietario)
                        {
                            case 1:
                                {
                                    claseColor = "colorEventoPropio";
                                    break;
                                }
                            case 2:
                                {
                                    claseColor = "colorEventoCompartido";
                                    break;
                                }
                            case 3:
                                {
                                    claseColor = "colorEventoPublico";
                                    break;
                                }
                        }
                        return claseColor;
                    }
                }
                public string txtBtnPublicar
                {
                    get
                    {
                        if (this._publicado)
                        {
                            return "Quitar de web";
                        }
                        else
                        {
                            return "Publicar";
                        }
                    }
                }
            #endregion
        #endregion
        #region "Constructores"
            public Evento(int idEvento)
            {
                this._idEvento = idEvento;
            }
            // para agregar 
            public Evento(string evento, DateTime fechaInicio, DateTime fechaFin, Usuario usuarioCreador, string descripcion)
            {
                this._evento = evento;
                this._fechaInicio = fechaInicio;
                this._fechaFin = fechaFin;
                this._usuarioCreador = usuarioCreador;
                this._descripcion = descripcion;
            }
            // para edit 
            public Evento(int idEvento, string evento, DateTime fechaInicio, DateTime fechaFin, Usuario usuarioCreador,string descripcion)
            {
                this._idEvento = idEvento;
                this._evento = evento;
                this._fechaInicio = fechaInicio;
                this._fechaFin = fechaFin;
                this._usuarioCreador = usuarioCreador;
                this._descripcion = descripcion;
            }
            public Evento(int idEvento, string evento, DateTime fechaInicio, DateTime fechaFin, int idUsuarioCreador, string descripcion)
            {
                this._idEvento = idEvento;
                this._evento = evento;
                this._fechaInicio = fechaInicio;
                this._fechaFin = fechaFin;
                Usuario usuarioCreador = new Usuario(idUsuarioCreador);
                this._usuarioCreador = usuarioCreador;
                this._descripcion = descripcion;
            }
            public Evento(int idEvento,string evento,DateTime fechaInicio,DateTime fechaFin,Usuario usuarioCreador,DateTime fechaCreacion,string descripcion)
            {
                this._idEvento          = idEvento;
                this._evento            = evento;
                this._fechaInicio       = fechaInicio;
                this._fechaFin          = fechaFin;
                this._usuarioCreador    = usuarioCreador;
                this._fechaCreacion     = fechaCreacion;
                this._descripcion       = descripcion;
            }
            
        #endregion 
        #region "funciones publicas"
            
        #endregion
    }
}
