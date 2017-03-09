using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// librerias 
using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class PublicacionEvento
    {
        #region "propiedades"
        public int              _idPublicacionEvento;
        public DateTime         _caducidad;
        public Usuario          _usuarioAutorizador;
        public EventoWebsite    _eventoWeb;
        public bool             _principal;
        public DateTime         _fecha;
        public bool             _estado;
        /*
            * idPublicacionEvento	4	int identity
                caducidad	-9	date
                id_usuarioautorizador_fk	4	int
                id_eventoweb_fk	4	int
                principal	-7	bit
                fecha	11	datetime
                estado	-7	bit
            */
        #endregion
        #region "constructores"
            public PublicacionEvento(int idPublicacionEvento)
            {
                this._idPublicacionEvento = idPublicacionEvento;
            }
        #endregion
    }
}
