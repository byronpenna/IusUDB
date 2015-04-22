using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class Evento
    {
        #region "propiedades"
            public int      _idEvento;
            public string   _evento;
            public DateTime _fechaInicio;
            public DateTime _fechaFin;
            public Usuario  _usuarioCreador;
            public DateTime _fechaCreacion;
            public string   _descripcion;
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
                        return String.Format("{0:dd/MM/yyyy HH:mm:ss}",this._fechaInicio);
                    }
                }
                public string getFechaFin
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy HH:mm:ss}", this._fechaFin);
                    }
                }
            #endregion
        #endregion
            #region "Constructores"
            // para agregar 
            public Evento(string evento, DateTime fechaInicio, DateTime fechaFin, Usuario usuarioCreador, string descripcion)
            {
                this._evento = evento;
                this._fechaInicio = fechaInicio;
                this._fechaFin = fechaFin;
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
