using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas 
    using IUSBack.Models.General;
// librerias externas
    using IUSLibs.FrontUI.Noticias.Control;
    using IUSLibs.FrontUI.Noticias.Entidades;

    using IUSLibs.LOGS;
namespace IUSBack.Models.Page.Administracion.Acciones
{
    public class ComentarioModel:PadreModel
    {
        #region "funciones" 
            public List<Comentario> sp_frontUi_noticias_back_getComentariosPost(int idPost, int idUsuarioEjecutor, int idPagina)
            {
                try
                {
                    ControlComentario controlComentario = new ControlComentario();
                    return controlComentario.sp_frontUi_noticias_back_getComentariosPost(idPost, idUsuarioEjecutor, idPagina);
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
            public bool sp_frontUi_noticias_back_delComentarioPost(int idComentario,int idUsuarioEjecutor,int idPagina)
            {
                try
                {
                    ControlComentario control = new ControlComentario();
                    return control.sp_frontUi_noticias_back_delComentarioPost(idComentario, idUsuarioEjecutor, idPagina);
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