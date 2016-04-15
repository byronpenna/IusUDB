using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace IUSLibs.BaseDatos
{
    public class ConexionIUS
    {
        #region "propiedades"
        private string usuario;
        private string password;
        private bool SSPI;
        private string servidor;
        private string db;
        public SqlConnection cn;
        #endregion 
        
        #region "funciones"
        private string getStrConexion()
        {
            string toReturn = "";
            try
            {
                toReturn = String.Format("Server={0};Database={1};", servidor, db);
                if (!SSPI)
                {
                    toReturn += String.Format("user id={0};password={1}", usuario, password);
                }
                else
                {
                    toReturn += "Integrated Security=SSPI";
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            return toReturn;
        }
        #endregion 

        #region "Constructores"
        public ConexionIUS()
        {
            /*this.servidor = "PROGRAMADOR";
            this.db = "IUS";
            this.usuario = "sa";
            this.password = "123456";
            this.SSPI = false;
             * */
            /*
            this.servidor = "168.243.3.62";
            this.db = "IUSDEV";
            this.usuario = "IUS";
            this.password = "123456";
            this.SSPI = false;
             */
            
            this.servidor = "168.243.3.62";
            this.db = "IUS";
            this.usuario = "desarrollo";
            this.password = "123456";
            this.SSPI = false;
            
            try
            {
                string strConexion = this.getStrConexion();
                if (strConexion != "")
                {
                    this.cn = new SqlConnection(strConexion);
                }
            }
            catch (Exception x) {
                this.cn = null;
                throw x;
            }
        }
        #endregion
    }
}
