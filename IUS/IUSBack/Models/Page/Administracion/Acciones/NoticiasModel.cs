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
            public ControlPostCategoria _controlNoticias;
        #endregion 
        #region "constructores"
            public NoticiasModel()
            {
                this._controlNoticias = new ControlPostCategoria();
            }
        #endregion
        #region "acciones"
            public List<PostCategoria> sp_adminfe_noticias_getCategorias(int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    return this._controlNoticias.sp_adminfe_noticias_getCategorias(idUsuarioEjecutor, idPagina);
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