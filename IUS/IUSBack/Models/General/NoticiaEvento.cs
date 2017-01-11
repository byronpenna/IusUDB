using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUSBack.Models.General
{
    public class NoticiaEvento
    {
        #region "propiedades"
            public int _id;
            public string _titulo;
            public string _descripcion;
            public DateTime? _fecha = null;
            public int _idTipoEntrada;
            public byte[] _imagen = null;
        #endregion
            public enum Tipo
            {
                Noticia = 1, Evento = 2
            }
        // constructores
            // full con enum
                public NoticiaEvento(int id, string titulo, string descripcion, Tipo tipo)
                {
                    this._id = id;
                    this._titulo = titulo;
                    this._descripcion = descripcion;
                    this._idTipoEntrada = (int)tipo;
                }
            // full with int 
                public NoticiaEvento(int id, string titulo, string descripcion, int tipo)
                {
                    this._id = id;
                    this._titulo = titulo;
                    this._descripcion = descripcion;
                    this._idTipoEntrada = tipo;
                }
    }
}