using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Laboral
{
    public class CargoEmpresa
    {
        #region "Propiedades"
            public int      _idCargoEmpresa;
            public string   _cargo;
            
        #endregion
        #region "Constructores"
            public CargoEmpresa(int idCargoEmpresa)
            {
                this._idCargoEmpresa = idCargoEmpresa;
            }
            public CargoEmpresa(int idCargoEmpresa,string cargo)
            {
                this._idCargoEmpresa    = idCargoEmpresa;
                this._cargo             = cargo;
            }  
        #endregion
    }
}
