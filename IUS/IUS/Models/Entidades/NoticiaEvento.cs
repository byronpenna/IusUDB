using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias externas 
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.ADMINFE.Entidades;
namespace IUS.Models.Entidades
{
    public class NoticiaEvento
    {
        // propiedades 
            public int          _id;
            public string       _titulo;
            public string       _descripcion;
            public DateTime?    _fecha = null;
            public int          _idTipoEntrada;
            public byte[]       _imagen = null;
            /*public Tipo tipoEntrada
            {
                get
                {
                    return (Tipo)this._idTipoEntrada;
                }
            }  */
            
            public enum     Tipo {
                Noticia = 1, Evento = 2
            }
        // constructores
            // full con enum
                public NoticiaEvento(int id,string titulo, string descripcion,Tipo tipo)
                {
                    this._id                = id;
                    this._titulo            = titulo;
                    this._descripcion       = descripcion;
                    this._idTipoEntrada     = (int)tipo;
                }
            // full with int 
                public NoticiaEvento(int id, string titulo, string descripcion, int tipo)
                {
                    this._id            = id;
                    this._titulo        = titulo;
                    this._descripcion   = descripcion;
                    this._idTipoEntrada = tipo;
                }
    }
}