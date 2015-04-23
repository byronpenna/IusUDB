using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class EventoWebsite
    {
        #region "propiedades"
            public int      _idEventoWeb;
            public Evento   _evento;
            public DateTime _fechaPublicacion;
            public Usuario  _usuarioPublicacion;
            public bool     _estado;
        #endregion 
        #region "constructores"
            public EventoWebsite(int idEventoWeb, DateTime fechaPublicacion, Usuario usuarioPublicacion, Evento evento, bool estado)
            {
                this._idEventoWeb = idEventoWeb;
                this._evento = evento;
                this._usuarioPublicacion = usuarioPublicacion;
                this._fechaPublicacion = fechaPublicacion;
                this._estado = estado;
            }
            public EventoWebsite()
            {

            }
        #endregion
    }
}
