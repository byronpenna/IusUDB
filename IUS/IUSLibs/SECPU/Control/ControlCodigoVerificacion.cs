using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// manejo de datos
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data;
// librerias internas
    using IUSLibs.BaseDatos;
    using IUSLibs.GENERALS;
    using IUSLibs.LOGS;
namespace IUSLibs.SECPU.Control
{
    public class ControlCodigoVerificacion:PadreLib
    {
        public bool sp_secpu_verificarCuenta(int num,int idUsuario)
        {
            
            bool estado = false;
            SPIUS sp = new SPIUS("sp_secpu_verificarCuenta");
            sp.agregarParametro("num", num);
            sp.agregarParametro("idUsuario", idUsuario);
            try
            {
                DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                if (this.resultadoCorrecto(tb))
                {
                    estado = true;
                }
            }
            catch (ErroresIUS x)
            {
                throw x;
            }
            catch (Exception x)
            {
                throw x;
            }
            return estado;
        }
    }
}
