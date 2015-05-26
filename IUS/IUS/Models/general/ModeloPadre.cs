using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Entidades.Noticias;
    using IUSLibs.ADMINFE.Control.Noticias;
namespace IUS.Models.general 
{
    public class ModeloPadre
    {
        #region "propiedades"
            private ControlPost _controlPost;
        #endregion
        #region "funciones"
            public List<Post> sp_adminfe_front_getTopNoticias(int n)
            {
                try
                {
                    return this._controlPost.sp_adminfe_front_getTopNoticias(n);
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        #endregion
        #region "constructores"
            public ModeloPadre()
            {
                this._controlPost = new ControlPost();
            }
        #endregion
    }
}