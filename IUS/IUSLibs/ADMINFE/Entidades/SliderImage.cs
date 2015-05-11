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
            public int      idSliderImage;
            public string   nombre;
            public byte[]   imagen;
            public bool     estado;
            public Pagina   pagina;

        #endregion 
    }
}
