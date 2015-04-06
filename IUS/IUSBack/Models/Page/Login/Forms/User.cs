using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace IUSBack.Models.Page.Login.Forms
{
    public partial class User
    {
        #region "propiedades"
        [Required(ErrorMessage="Favor ingresa nombre",AllowEmptyStrings=false)]
        public string usuario{get;set;}
        [Required(ErrorMessage="Por favor ingrese una contraseña",AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string pass { get; set; }
        #endregion 
        public User() {
        }
    }
}