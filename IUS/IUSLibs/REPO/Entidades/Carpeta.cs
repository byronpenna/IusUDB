using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas 
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Entidades
{
    public class Carpeta
    {
        #region "propiedades"
            public int _idCarpeta;
            public string _nombre;
            public Usuario _usuario;
            public Carpeta _carpetaPadre;
            public string _ruta;
            // extras
            private string icono = "folder.png";
            public string getIcono{
                get {
                    return this.icono;
                }
            }
                                     
        #endregion
        #region "constructores"
            public Carpeta() {
                this._carpetaPadre = null;
            }
            public Carpeta(int idCarpeta)
            {
                this._idCarpeta = idCarpeta;
            }
            // para ingresar 
            public Carpeta(string nombre, Usuario usuario, Carpeta carpetaPadre)
            {
                this._nombre = nombre;
                this._usuario = usuario;
                this._carpetaPadre = carpetaPadre;
            }
            public Carpeta(int idCarpeta, string nombre, int idUsuario, Carpeta carpetaPadre, string ruta)
            {
                Usuario usuario = new Usuario(idUsuario);
                this._idCarpeta = idCarpeta;
                this._nombre = nombre;
                this._usuario = usuario;
                this._carpetaPadre = carpetaPadre;
                this._ruta = ruta;
            }
            public Carpeta(int idCarpeta, string nombre, Usuario usuario, Carpeta carpetaPadre, string ruta)
            {
                this._idCarpeta = idCarpeta;
                this._nombre = nombre;
                this._usuario = usuario;
                this._carpetaPadre = carpetaPadre;
                this._ruta = ruta;
            }
        #endregion
    }
}
