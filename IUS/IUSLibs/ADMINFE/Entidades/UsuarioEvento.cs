﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// librerias internas
    using IUSLibs.SEC.Entidades;
namespace IUSLibs.ADMINFE.Entidades
{
    public class UsuarioEvento
    {
        #region "propiedades"
            public int              _idEventoUsuario;
            public Evento           _evento;
            public Usuario          _usuario;
        #endregion
        #region "constructores"
            public UsuarioEvento(int idEventoUsuario)
            {
                this._idEventoUsuario = idEventoUsuario;
            }
            public UsuarioEvento(int idEventoUsuario, Evento evento, Usuario usuario)
            {
                this._idEventoUsuario = idEventoUsuario;
                this._evento = evento;
                this._usuario = usuario;
            }
            // para agregar 
                public UsuarioEvento(Evento evento,Usuario usuario)
                {
                    this._evento = evento;
                    this._usuario = usuario;
                }
        #endregion
    }
}
