using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class DatosIUS
    {
        #region "propiedades"
            public int _idDatosIuss;    
            public int _continentesPresentes;
            public int _paisesPresentes;
            public int _totalInstituciones;
            public int _totalEstudiantes;
            public int _totalSalesianos;
        #endregion
        #region "constructores"
            public DatosIUS(int idDatosIus,int continentesPresentes,int paisesPresentes,int totalInstituciones,int totalEstudiantes,int totalSalesianos)
            {
                this._idDatosIuss           = idDatosIus;
                this._continentesPresentes  = continentesPresentes;
                this._paisesPresentes       = paisesPresentes;
                this._totalInstituciones    = totalInstituciones;
                this._totalEstudiantes      = totalEstudiantes;
                this._totalSalesianos       = totalSalesianos;
            }
        #endregion
    }
}
