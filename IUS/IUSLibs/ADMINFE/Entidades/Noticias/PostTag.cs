using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades.Noticias
{
    public class PostTag
    {
        #region "propiedades"
            public int  _idPostTag;
            public Post _post;
            public Tag  _tag;
        #endregion
        #region "constructor"
            public PostTag(int idPostTag, Post post, Tag tag)
            {
                this._idPostTag = idPostTag;
                this._post = post;
                this._tag = tag;
            }
            public PostTag(int idPostTag,int idPost,int idTag)
            {
                Post post = new Post(idPost);
                Tag tag = new Tag(idTag);
                this._post = post;
                this._tag = tag;
            }
        #endregion
    }
}
