using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.RRHH.Entidades.Laboral
{
    public class ActividadEmpresa
    {
        #region "Propiedades"
            public int              _idActividadesEmpresa;
            public LaboralPersona   _laboralPersona;
            public string           _actividad;
            
        #endregion
        #region "Constructores"
                public ActividadEmpresa(int idActividadesEmpresa)
                {
                    this._idActividadesEmpresa = idActividadesEmpresa;
                }

            // full atributos
                public ActividadEmpresa(int idActividadesEmpresa,int idLaboralPersona,string actividad)
                {
                    this._idActividadesEmpresa = idActividadesEmpresa;
                    LaboralPersona laboral = new LaboralPersona(idLaboralPersona);
                    this._laboralPersona = laboral;
                    this._actividad = actividad;
                }
        #endregion
    }
}
