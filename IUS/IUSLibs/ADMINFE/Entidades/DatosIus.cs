using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUSLibs.ADMINFE.Entidades
{
    public class DatosIus
    {
        #region "propiedades" 
            public int      _salesianosMundo;
            public int      _paisesPresencia;
            public int      _provincias;
            public int      _gruposFamilia;
            public string   _websiteSalesiano;
        #endregion
        #region "constructores"
            public DatosIus(int salesianosMundo,int paisesPresencia,int provincias,int gruposFamilia,string websiteSalesiano)
            {
                this._salesianosMundo   = salesianosMundo;
                this._paisesPresencia   = paisesPresencia;
                this._provincias        = provincias;
                this._gruposFamilia     = gruposFamilia;
                this._websiteSalesiano  = websiteSalesiano;
            }
        #endregion
    }
}
