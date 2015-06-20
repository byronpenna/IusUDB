using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// manejo de archivos
using System.IO;
namespace IUSBack.Models.General
{
    public class GestionFileServerModel
    {
        // si no encuentra la ruta sera creada
        public string getPathWithCreate(string path, string fileName)
        {
            string retorno = "";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                retorno = Path.Combine(path, fileName);
            }
            catch (Exception x)
            {
                throw x;
            }
            return retorno;
        }
    }
}