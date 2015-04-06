using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    public class Permiso
    {
        #region "propiedades"
        public bool _editar;
        private String habilidatado = "enabled";
        private String deshabilitado = "disabled";
        public string stringEditar
        {
            get
            {
                if (this._editar)
                {
                    return habilidatado;
                }
                else
                {
                    return deshabilitado;
                }
            }
        }
        public String stringCrear
        {
            get
            {
                if (this._crear)
                {
                    return habilidatado;
                }
                else
                {
                    return deshabilitado;
                }
                
            }
        }
        public String stringEliminar
        {
            get
            {
                if (this._eliminar)
                {
                    return habilidatado;
                }
                else
                {
                    return deshabilitado;
                }
            }
        }
        public bool _crear;
        public bool _eliminar;
        #endregion
        public Permiso(bool editar, bool crear, bool eliminar)
        {
            this._editar = editar;
            this._crear = crear;
            this._eliminar = eliminar;
        }
    }
}
