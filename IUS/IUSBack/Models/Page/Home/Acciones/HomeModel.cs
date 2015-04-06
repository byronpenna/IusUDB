using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// propias
    // control
        using IUSBack.Models.General;
        using IUSLibs.SEC.Control;
    // entidades 
        using IUSLibs.SEC.Entidades;
namespace IUSBack.Models.Page.Home.Acciones
{
    public class HomeModel:PadreModel
    {
        #region "Abrir menu"
        #endregion
        public String abrirSubMenu(String subMenuString)
        {
            String toReturn = "<ul>" +
                       subMenuString;
            return toReturn;
        }
        
    }
}