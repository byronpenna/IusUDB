using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.REPO.Entidades.Publico
{
    public class CarpetaPublica
    {
        #region "propiedades"
            public int              _idCarpetaPublica;
            public string           _nombre;
            public CarpetaPublica   _carpetaPadre;
            public DateTime         _fechaCreacion;
            // extras
            public string _strRuta;
            private string icono        = "folder-green.png";
            private string iconoFront   = "folder.png";
            #region "get y set"
                public string getFechaCreacion
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy}", this._fechaCreacion);
                    }
                }
                public string getIconoFront
                {
                    get
                    {
                        return this.iconoFront;
                    }
                }
                public string getIcono
                {
                    get
                    {
                        return this.icono;
                    }
                }
            #endregion
        #endregion
            #region "constructores"
            public CarpetaPublica() { 
            }
            public CarpetaPublica(int idCarpetaPublica)
            {
                this._idCarpetaPublica = idCarpetaPublica;
            }
            public CarpetaPublica(int idCarpetaPublica,string nombre,int idCarpetaPadre)
            {
                this._idCarpetaPublica      = idCarpetaPublica;
                CarpetaPublica carpetaPadre = new CarpetaPublica(idCarpetaPadre);
                this._carpetaPadre          = carpetaPadre;
            }
            public CarpetaPublica(int idCarpetaPublica, string nombre, CarpetaPublica carpetaPadre)
            {
                this._idCarpetaPublica = idCarpetaPublica;
                this._nombre = nombre;
                this._carpetaPadre = carpetaPadre;
            }
            // para cambiar nombre 
                public CarpetaPublica(int idCarpeta,string nombre)
                {
                    this._idCarpetaPublica = idCarpeta;
                    this._nombre = nombre;

                }
            // para agregar
                public CarpetaPublica(string nombre, int idCarpetaPadre)
                {
                    this._nombre = nombre;
                    CarpetaPublica carpetaPadre = new CarpetaPublica(idCarpetaPadre);
                    this._carpetaPadre = carpetaPadre;
                }
            
        #endregion

    }
}
