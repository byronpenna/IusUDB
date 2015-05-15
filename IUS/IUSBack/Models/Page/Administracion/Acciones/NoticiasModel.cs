using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.LOGS;
    // entidades
        using IUSLibs.ADMINFE.Entidades;
        using IUSLibs.ADMINFE.Entidades.Noticias;
    // control
        using IUSLibs.ADMINFE.Control;
        using IUSLibs.ADMINFE.Control.Noticias;
    using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class NoticiasModel:PadreModel
    {
        #region "propiedades"
            public ControlPostCategoria _controlPostCategoria;
            public ControlPost _controlPost;
        #endregion 
        #region "constructores"
            public NoticiasModel()
            {
                this._controlPostCategoria  = new ControlPostCategoria();
                this._controlPost           = new ControlPost();
            }
        #endregion
        #region "acciones"
            public Post sp_adminfe_noticias_publicarPost(Post postAgregar,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlPost.sp_adminfe_noticias_publicarPost(postAgregar, idUsuarioEjecutor, idPagina);
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
            public List<PostCategoria> sp_adminfe_noticias_getCategorias(int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlPostCategoria.sp_adminfe_noticias_getCategorias(idUsuarioEjecutor, idPagina);
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
    }
}