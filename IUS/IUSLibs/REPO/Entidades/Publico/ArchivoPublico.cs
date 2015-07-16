using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.REPO.Entidades.Publico
{
    public class ArchivoPublico
    {
        #region "propiedades"
            public int              _idArchivoPublico;
            public Archivo          _archivoUsuario;
            public CarpetaPublica   _carpetaPublica;
            public string           _nombre;
            public bool             _estado;
        #endregion
        #region "constructores"
            public ArchivoPublico(int idArchivoPublico, string nombre)
            {
                this._idArchivoPublico = idArchivoPublico;
                this._nombre = nombre;
            }
            public ArchivoPublico(int idArchivoPublico,int idArchivoUsuario, int idCarpetaPublica, string nombre,bool estado) { 
                this._idArchivoPublico  = idArchivoPublico;
                Archivo archivoUsuario  = new Archivo(idArchivoUsuario);
                this._archivoUsuario    = archivoUsuario;
                CarpetaPublica carpeta  = new CarpetaPublica(idCarpetaPublica);
                this._carpetaPublica    = carpeta;
                this._nombre            = nombre;
                this._estado            = estado;
                
            }
            public ArchivoPublico(int idArchivoPublico, Archivo archivoUsuario, int idCarpetaPublica, string nombre, bool estado)
            {
                this._idArchivoPublico = idArchivoPublico;
                this._archivoUsuario = archivoUsuario;
                CarpetaPublica carpeta = new CarpetaPublica(idCarpetaPublica);
                this._carpetaPublica = carpeta;
                this._nombre = nombre;
                this._estado = estado;
            }
            public ArchivoPublico(int idArchivoPublico, Archivo archivoUsuario, CarpetaPublica carpeta, string nombre, bool estado)
            {
                this._idArchivoPublico = idArchivoPublico;
                this._archivoUsuario = archivoUsuario;
                this._carpetaPublica = carpeta;
                this._nombre = nombre;
                this._estado = estado;
            }
            public ArchivoPublico(int idArchivoPublico, int idArchivoUsuario, CarpetaPublica carpetaPublica, string nombre, bool estado)
            {
                this._idArchivoPublico = idArchivoPublico;
                Archivo archivoUsuario = new Archivo(idArchivoUsuario);
                this._archivoUsuario = archivoUsuario;
                this._carpetaPublica = carpetaPublica;
                this._nombre = nombre;
                this._estado = estado;
            }
            // para agregar
            public ArchivoPublico(int idArchivoUsuario,int idCarpetaPublica,string nombre){
                Archivo archivo = new Archivo(idArchivoUsuario);
                this._archivoUsuario = archivo;
                CarpetaPublica carpetaPublica = new CarpetaPublica(idCarpetaPublica);
                this._carpetaPublica = carpetaPublica;
                this._nombre = nombre;
                
            }
        #endregion
    }
}
