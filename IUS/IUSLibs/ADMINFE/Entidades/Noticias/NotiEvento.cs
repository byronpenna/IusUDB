using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class NotiEvento
    {
        // propiedades 
            public int          _id;
            public string       _titulo;
            public string       _descripcion;
            public DateTime?    _fecha          = null;
            public DateTime     _fechaCaducidad;
            public int          _idTipoEntrada;
            public byte[]       _imagen         = null;
            public Tipo tipoEntrada
            {
                get
                {
                    return (Tipo)this._idTipoEntrada;
                }
            }
            public string getStrTipoEntrada
            {
                get
                {
                    if(tipoEntrada == Tipo.Evento){
                        return "Evento";
                    }
                    else if (tipoEntrada == Tipo.Noticia)
                    {
                        return "Noticia";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            public enum     Tipo {
                Noticia = 1, Evento = 2
            }
        // constructores
            public NotiEvento(int id)
            {
                this._id = id;
            }
            // full con enum
                public NotiEvento(int id,string titulo, string descripcion,Tipo tipo)
                {
                    this._id                = id;
                    this._titulo            = titulo;
                    this._descripcion       = descripcion;
                    this._idTipoEntrada     = (int)tipo;
                }
            // full with int 
                public NotiEvento(int id, string titulo, string descripcion, int tipo)
                {
                    this._id            = id;
                    this._titulo        = titulo;
                    this._descripcion   = descripcion;
                    this._idTipoEntrada = tipo;
                }
    }
}
