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
            #region "operacionales"
                public string getTxtEstado
                {
                    get
                    {
                        if (this._estado)
                        {
                            return "Quitar web";
                        }
                        else
                        {
                            return "Publicar web";
                        }
                    }
                }
                public string getFechaCreacion
                {
                    get{
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", this._fechaCreacion);
                    }                    
                }
                public string getFechaModificacion
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", this._fechaModificacion);
                    }
                }
            #endregion
        #endregion
            #region "constructores"
            public Post()
            {

            }
            public Post(int idPost, DateTime fechaCreacion, DateTime fechaModificacion, string titulo, string contenido, bool estado, int usuarioCreador) {
                Usuario usu = new Usuario(usuarioCreador);
                this._idPost = idPost;
                this._fechaCreacion = fechaCreacion;
                this._fechaModificacion = fechaModificacion;
                this._titulo = titulo;
                this._contenido = contenido;
                this._estado = estado;
                this._usuario = usu;
            }
            public Post(int idPost, DateTime fechaCreacion, DateTime fechaModificacion, string titulo, string contenido, bool estado, Usuario usuarioCreador)
            {
                this._idPost            = idPost;
                this._fechaCreacion     = fechaCreacion;
                this._fechaModificacion = fechaModificacion;
                this._titulo            = titulo;
                this._contenido         = contenido;
                this._estado            = estado;
                this._usuario           = usuarioCreador;
            }
            public Post(int idPost)
            {
                this._idPost = idPost;
            }
            // para actualizar 
                public Post(int idPost, string titulo, string contenido)
                {
                    this._titulo    = titulo;
                    this._contenido = contenido;
                    this._idPost    = idPost;
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
