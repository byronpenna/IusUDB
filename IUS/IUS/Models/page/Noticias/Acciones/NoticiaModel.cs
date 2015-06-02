using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// internas 
    using IUS.Models.general;
// externas 
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Control.Noticias;
namespace IUS.Models.page.Noticias.Acciones
{
    
        public class NoticiaModel:ModeloPadre
        {
            #region "propiedades"
                private ControlPost _controlPost;
            #endregion
            #region "acciones"
                public Dictionary<object, object> sp_adminfe_front_getNoticiaFromId(int idPost)
                {
                    try
                    {
                        return this._controlPost.sp_adminfe_front_getNoticiaFromId(idPost);
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
                public NoticiaModel()
                {
                    this._controlPost = new ControlPost();
                }
            #endregion
        }
   
}