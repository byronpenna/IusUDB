using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.SEC.Entidades
{
    // CLASE ES UN INTENTO FALLIDO PERO SIGUE AQUI PORQUE SE OCUPA EN ALGUNOS LUGARES
    public class Permiso
    {
        #region "propiedades"
            #region "propiedades simples"
                    #region "publicas"
                        public bool _crear;
                        public bool _editar;
                        public bool _eliminar;
                        public bool _ver;
                    #endregion            
                    #region "privadas"
                        private String habilidatado = "enabled";
                        private String deshabilitado = "disabled";
                    #endregion
            #endregion
            #region "Propiedades compuestas"
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
                public List<String> arrPermisos
                {
                    get
                    {
                        return this.getArrPermisos();
                    }
                }
            #endregion
        #endregion
        #region "funciones privadas"
                private List<String> getArrPermisos()
                {
                    List<String> toReturn = new List<String>();
                    if (this._crear)
                    {
                        toReturn.Add("Crear");
                    }
                    if (this._editar) {
                        toReturn.Add("Editar");
                    }
                    if (this._eliminar)
                    {
                        toReturn.Add("Eliminar");
                    }
                    if (this._ver)
                    {
                        toReturn.Add("Ver");
                    }
                    if(toReturn.Count == 0){
                        toReturn = null;
                    }
                    return toReturn;
                }
        #endregion
        #region "Constructores"
            public Permiso(bool editar, bool crear, bool eliminar)
            {
                this._editar = editar;
                this._crear = crear;
                this._eliminar = eliminar;
            }
            public Permiso(bool crear, bool editar, bool eliminar, bool ver)
            {
                this._crear = crear;
                this._editar = editar;
                this._eliminar = eliminar;
                this._ver = ver;
            }
            public Permiso()
            {

            }
        #endregion
    }
}
