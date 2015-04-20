using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data;
    using System.Data.SqlClient;
    
namespace IUSLibs.GENERALS
{
    public class PadreLib
    {
        #region "propiedades"
            public enum Permisos
            {
                Crear=1,Editar=2,Eliminar=3,ver=4
            }
        #endregion
        #region "funciones heredadas"
        // contendra todas los metodos y atributos de uso general
            protected bool DataSetDontHaveTable(DataSet ds)
            {
                bool toReturn = false;
                if(ds.Tables.Count == 0)
                {
                    toReturn = true;
                }
                return toReturn;
            }
            protected DataTableCollection getTables(DataSet ds)
            {
                DataTableCollection tables = null;
                if (!this.DataSetDontHaveTable(ds))
                {
                    tables = ds.Tables;
                }
                return tables;
            }
            protected bool objToBool(string val)
            {
                int ival = Convert.ToInt32(val);
                if (ival <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        #endregion
    }
}
