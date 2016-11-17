using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
    using IUSLibs.TRL.Entidades;
namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class Post
    {
        #region "propiedades"
            // propiedades 
                public int      _idPost;
                public DateTime _fechaCreacion;
                public DateTime _fechaModificacion;
                public string   _titulo;
                public string   _contenido;
                public bool     _estado;
                public Usuario  _usuario;
                public byte[]   _miniatura;
                public Idioma   _idioma;
                public string   _descripcion;
            // externas a tabla 
                public int     _publicado=-1; 
            #region "operacionales"
                public string getTxtEstado
                {
                    get
                    {
                        switch (this._publicado)
                        {
                            case -1:
                                {
                                    if (this._estado)
                                    {
                                        return "Quitar solicitud";
                                    }
                                    else
                                    {
                                        return "Enviar solicitud";
                                    }
                                }
                            case 0:
                                {
                                    if (this._estado)
                                    {
                                        return "Cancelar revisión";
                                    }
                                    else
                                    {
                                        return "Enviar revisión";
                                    }
                                }
                            case 1:
                                {
                                    return "Publicado website";
                                }
                            default:
                                {
                                    return "__";
                                }
                        }
                        /*if (this._publicado)
                        {
                            
                        }
                        else
                        {*/
                            
                        //}
                        
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

                public string convertMiniatura
                {
                    get
                    {
                        if (this._miniatura != null)
                        {
                            return Convert.ToBase64String(this._miniatura);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            #endregion
            #region "otros"
                public DateTime? _fechaInicioBusqueda;
                public DateTime? _fechaFinBusqueda; // unicamente usado para las busquedas
            #endregion
        #endregion
        #region "constructores"
            public Post()
            {

            }
            public Post(int idPost)
            {
                this._idPost = idPost;
            }
            // full atributos
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
                public Post(string titulo, string contenido, Usuario usu,Idioma idioma)
                {
                    this._titulo = titulo;
                    this._contenido = contenido;
                    this._usuario = usu;
                    this._idioma = idioma;
                }
            // solo foto
                public Post(int idPost, byte[] foto)
                {
                    this._idPost = idPost;
                    this._miniatura = foto;
                }
            // para buscar
                
        #endregion
    }
}
