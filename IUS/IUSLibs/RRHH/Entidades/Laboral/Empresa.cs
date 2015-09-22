using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Laboral
{
    public class Empresa
    {
        #region "Propiedades"
            public int          _idEmpresa;
            public string       _nombre;
            public string       _direccion;
            public RubroEmpresa _rubro;
        #endregion
        #region "Constructores"
            public Empresa(int idEmpresa)
            {
                this._idEmpresa = idEmpresa;
            }
            public Empresa(int idEmpresa, string nombre, string direccion, int idRubro)
            {
                this._idEmpresa = idEmpresa;
                this._nombre = nombre;
                this._direccion = direccion;
                RubroEmpresa rubro = new RubroEmpresa(idRubro);
                this._rubro = rubro;
            }
        #endregion
    }
}
