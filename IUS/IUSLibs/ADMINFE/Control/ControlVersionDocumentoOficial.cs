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
    using IUSLibs.FrontUI.Entidades;
    // --
    using IUSLibs.ADMINFE.Entidades;
namespace IUSLibs.ADMINFE.Control
{
    public class ControlVersionDocumentoOficial:PadreLib
    {
        #region "get"
            public VersionDocumentoOficial sp_adminfe_front_getDocumentosOficialesById(int idVersion,string idioma, string ip, int idPagina)
            {
                VersionDocumentoOficial versionDocumentoOficial=null;
                try
                {
                    SPIUS sp = new SPIUS("sp_adminfe_front_getDocumentosOficialesById");
                    sp.agregarParametro("idVersion", idVersion);
                    sp.agregarParametro("idioma", idioma);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);
                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            DataRow row = tb[0].Rows[0];
                            versionDocumentoOficial = new VersionDocumentoOficial((int)row["idVersion"]);
                            versionDocumentoOficial._idioma = new TRL.Entidades.Idioma((int)row["id_idioma_fk"]);
                            versionDocumentoOficial._traduccion = row["traduccion"].ToString();
                            versionDocumentoOficial._documento = new DocumentoOficial((int)row["id_documento_fk"]);
                            versionDocumentoOficial._ruta = row["ruta"].ToString();
                        }
                    }
                    return versionDocumentoOficial;
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
            public List<VersionDocumentoOficial> sp_adminfe_front_getDocumentosOficiales(string idioma, string ip, int idPagina)
            {
                List<VersionDocumentoOficial> versionesDocumentosOficiales = null;
                VersionDocumentoOficial versionDocumentoOficial;
                try
                {
                    SPIUS sp = new SPIUS("sp_adminfe_front_getDocumentosOficiales");
                    sp.agregarParametro("idioma", idioma);
                    sp.agregarParametro("ip", ip);
                    sp.agregarParametro("idPagina", idPagina);

                    DataTableCollection tb = this.getTables(sp.EjecutarProcedimiento());
                    if (this.resultadoCorrectoGet(tb))
                    {
                        if (tb[0].Rows.Count > 0)
                        {
                            versionesDocumentosOficiales = new List<VersionDocumentoOficial>();
                            foreach (DataRow row in tb[0].Rows)
                            {
                                /*
                                    idDocumentoOficial,nombre
                                 */
                                versionDocumentoOficial = new VersionDocumentoOficial((int)row["idVersion"]);
                                versionDocumentoOficial._idioma = new TRL.Entidades.Idioma((int)row["id_idioma_fk"]);
                                versionDocumentoOficial._traduccion = row["traduccion"].ToString();
                                versionDocumentoOficial._documento = new DocumentoOficial((int)row["id_documento_fk"]);
                                versionDocumentoOficial._ruta = row["ruta"].ToString();
                                versionesDocumentosOficiales.Add(versionDocumentoOficial);
                            }
                        }
                    }

                    return versionesDocumentosOficiales;
                }
                catch (ErroresIUS x)
                {
                    throw x;
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        #endregion
        
    }
}
