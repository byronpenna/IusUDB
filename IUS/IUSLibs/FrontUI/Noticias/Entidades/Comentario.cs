using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas
    using IUSLibs.ADMINFE.Entidades.Noticias;
namespace IUSLibs.FrontUI.Noticias.Entidades
{
    public class Comentario
    {
        #region "propiedades"
            #region "fijas"
                public int      _idComentario;
                public string   _comentario;
                public string   _email;
                public string   _ip;
                public Post     _post;
                public string   _nombre;
                public DateTime _fecha;
            #endregion
            #region "dinamicas"
                public string getTxtFecha
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss tt}", this._fecha);
                    }
                }
            #endregion
        #endregion
        #region "constructores"
            // full propiedades
                public Comentario(int idComentario, string comentario, string email, string ip, string nombre, int idPost,DateTime fecha)
                {
                    this._idComentario = idComentario;
                    this._comentario = comentario;
                    this._email = email;
                    this._ip = ip;
                    this._nombre = nombre;
                    Post post = new Post(idPost);
                    this._post = post;
                    this._fecha = fecha;
                }
            // para agregar
                public Comentario(string comentario,string email,string ip,string nombre,int idPost)
                {
                    this._comentario = comentario;
                    this._email = email;
                    this._ip = ip;
                    this._nombre = nombre;
                    Post post = new Post(idPost);
                    this._post = post;
                }

        #endregion
    }
}
