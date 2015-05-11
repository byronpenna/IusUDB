using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IUSLibs.ADMINFE.Entidades
{
    public class SliderImage
    {
        #region "propiedades"
            //idSliderImages,nombre,imagen,estado,id_pagina_fk,fechaCreacion
            public int      _idSliderImage;
            public string   _nombre;
            public byte[]   _imagen;
            public bool     _estado;
            public Pagina   _pagina;
            public DateTime _fechaCreacion;
        #endregion 
        #region "constructores"
            public SliderImage(int idSliderImage,string nombre,byte[] imagen,bool estado,Pagina pagina,DateTime fechaCreacion)
            {
                this._idSliderImage = idSliderImage;
                this._nombre        = nombre;
                this._imagen        = imagen;
                this._pagina        = pagina;
                this._fechaCreacion = fechaCreacion;
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
