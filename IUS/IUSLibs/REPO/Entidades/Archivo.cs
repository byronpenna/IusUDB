﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.REPO.Entidades
{
    public class Archivo
    {
        #region "propiedades"
            public int              _idArchivo;
            public string           _nombre;
            public Carpeta          _carpeta;
            public string           _src;
            public ExtensionArchivo _extension;
            public DateTime         _fechaCreacion;
            #region "con get y set"
                public string getFechaCreacion
                {
                    get
                    {
                        return String.Format("{0:dd/MM/yyyy}", this._fechaCreacion);
                    }
                }
            #endregion
        #endregion
        #region "constructores"
            public Archivo(int idArchivo)
            {
                this._idArchivo = idArchivo;
            }
            public Archivo(int idArchivo, ExtensionArchivo extension,Carpeta carpeta)
            {
                this._idArchivo = idArchivo;
                this._extension = extension;
                this._carpeta = carpeta;
            }
            public Archivo(int idArchivo,ExtensionArchivo extension)
            {
                this._idArchivo = idArchivo;
                this._extension = extension;
            }
            public Archivo(string nombre)
            {
                this._nombre = nombre;
            }
            public Archivo(int idArchivo, string nombre)
            {
                this._idArchivo = idArchivo;
                this._nombre = nombre;
            }
            // para agregar
                public Archivo(string nombre, int idCarpeta, string src, ExtensionArchivo extension)
                {
                    this._nombre = nombre;
                    Carpeta carpeta = new Carpeta(idCarpeta);
                    this._carpeta = carpeta;
                    this._src = src;
                    this._extension = extension;
                }
            public Archivo(int idArchivo, string nombre, Carpeta carpeta,ExtensionArchivo extension)
            {
                this._idArchivo = idArchivo;
                this._nombre = nombre;
                this._carpeta = carpeta;
                this._extension = extension;
            }
            public Archivo(int idArchivo,string nombre,int idCarpeta,string src,ExtensionArchivo extension)
            {
                this._idArchivo = idArchivo;
                this._nombre = nombre;
                Carpeta carpeta = new Carpeta(idCarpeta);
                this._carpeta = carpeta;
                this._src = src;
                this._extension = extension;
            }
        #endregion
    }
}
