using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Rol
    {
        #region "propiedades"
            public int _idRol;
            public String _rol;
            public bool _estado;
            public String stringEstado
            {
                get
                {
                    if (this._estado)
                    {
                        return "Activo";
                    }
                    else
                    {
                        return "Inactivo";
                    }
                }
            }
        #endregion
        #region "constructores"
            public Rol(int idRol, String rol, bool estado)
            {
                this._idRol = idRol;
                this._rol = rol;
                this._estado = estado;
            }
            public Rol(int idRol)
            {
                this._idRol = idRol;
            }
            // para agregar, no se necesita ID 
            public Rol(String rol, bool estado)
            {
                this._rol = rol;
                this._estado = estado;
            }
        #endregion    
    }
}
