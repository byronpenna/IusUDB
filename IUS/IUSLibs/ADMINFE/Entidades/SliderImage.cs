using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IUSLibs.ADMINFE.Entidades
{
    public class SliderImage
    {
        #region "propiedades"
            public int      _idSliderImage;
            public string   _nombre;
            // lo mismo pero la segunda funciona pra mandarlo en serialize
                public byte[]   _imagen;
                public string   _strImagen;
            public bool     _estado;
            public Pagina   _pagina;
            public DateTime _fechaCreacion;
            #region "con procedimiento"
                public string textoEstado {
                    get
                    {
                        if (this._estado)
                        {
                            return "Deshabilitar";
                        }
                        else
                        {
                            return "Habilitar";
                        }
                    }
                }
            #endregion
        #endregion
            #region "constructores"
            public SliderImage(int idSliderImage,string nombre,byte[] imagen,bool estado,Pagina pagina,DateTime fechaCreacion)
            {
                this._idSliderImage = idSliderImage;
                this._nombre        = nombre;
                this._imagen        = imagen;
                this._pagina        = pagina;
                this._fechaCreacion = fechaCreacion;
                this._estado        = estado;
            }
            // para mandarlo serializado 
            public SliderImage(int idSliderImage, string nombre, string strImagen, bool estado, Pagina pagina, DateTime fechaCreacion)
            {
                this._idSliderImage = idSliderImage;
                this._nombre        = nombre;
                this._strImagen     = strImagen;
                this._pagina        = pagina;
                this._fechaCreacion = fechaCreacion;
                this._estado        = estado;
            }
            // para agregar 
            public SliderImage(string nombre, byte[] imagen, bool estado, Pagina pagina)
            {
                this._nombre = nombre;
                this._imagen = imagen;
                this._pagina = pagina;
            }
        #endregion
    }
}
