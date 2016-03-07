using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// internas
    using IUSLibs.REPO.Entidades;
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.REPO.Entidades.Compartido
{
    public class ArchivoCompartido
    {
        #region "propiedades"
            public int      _idArchivoCompartido;
            public Archivo  _archivo;
            public Usuario  _usuario;
            public DateTime _fecha;
            public bool     _propio = false;
        #endregion
        #region "constructores"
            public ArchivoCompartido(int idArchivo, int idUsuario)
            {
                Archivo archivo = new Archivo(idArchivo);
                Usuario usuario = new Usuario(idUsuario);
                this._archivo = archivo;
                this._usuario = usuario;
            }
            public ArchivoCompartido(int idArchivoCompartido, Archivo archivo, int idUsuario)
            {
                this._idArchivoCompartido = idArchivoCompartido;
                this._archivo = archivo;
                Usuario usuario = new Usuario(idUsuario);
                this._usuario = usuario;
            }
            public ArchivoCompartido(int idArchivoCompartido,int idArchivo, int idUsuario, DateTime fecha)
            {
                Archivo archivo = new Archivo(idArchivo);
                Usuario usuario = new Usuario(idUsuario);
                this._idArchivoCompartido = idArchivoCompartido;
                this._archivo = archivo;
                this._usuario = usuario;
                this._fecha = fecha;
            }
        #endregion
    }
}
