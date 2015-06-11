using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class CategoriaPost
    {
        #region "propiedades"
            public int _idCategoriaPost;
            public Post _post;
            public PostCategoria _categoria;
        #endregion
        #region "constructores"
            public CategoriaPost(int idCategoriaPost, Post post, PostCategoria categoria)
            {
                this._idCategoriaPost = idCategoriaPost;
                this._post = post;
                this._categoria = categoria;
            }
            public CategoriaPost(int idCategoriaPost, int idPost, int idCategoria)
            {
                this._idCategoriaPost = idCategoriaPost;
                this._post = new Post(idPost);
                this._categoria = new PostCategoria(idCategoria);
            }
        #endregion
    }
}
