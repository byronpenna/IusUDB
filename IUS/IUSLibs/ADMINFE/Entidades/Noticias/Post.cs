using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class Post
    {
        #region "propiedades"
            public int      _idPost;
            public DateTime _fechaCreacion;
            public DateTime _fechaModificacion;
            public string   _titulo;
            public string   _contenido;
            public bool     _estado;
            public Usuario  _usuario;

        #endregion
        #region "constructores"
            public Post(int idPost)
            {
                this._idPost = idPost;
            }
            // para agregar 
            public Post(string titulo, string contenido, Usuario usu)
            {
                this._titulo    = titulo;
                this._contenido = contenido;
                this._usuario = usu;
            }
        #endregion
    }
}
