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
    }
}
