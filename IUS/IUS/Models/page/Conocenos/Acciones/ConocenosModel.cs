using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias internas
    using IUS.Models.general;
// librerias externas
    using IUSLibs.LOGS;
    using IUSLibs.ADMINFE.Control;
    using IUSLibs.ADMINFE.Entidades;
namespace IUS.Models.page.Conocenos.Acciones
{
    public class ConocenosModel:ModeloPadre
    {
        #region "propiedades"
            
        #endregion
        #region "funciones"
            #region "Get"
                public List<VersionDocumentoOficial> sp_adminfe_front_getDocumentosOficiales(string idioma, string ip, int idPagina)
                {
                    try
                    { 
                        ControlVersionDocumentoOficial control = new ControlVersionDocumentoOficial();
                        return control.sp_adminfe_front_getDocumentosOficiales(idioma, ip, idPagina);
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
                public DatosSalesianos sp_adminfe_front_getDatosSalesianos(string ip, int idPagina)
                {
                    try
                    {
                        ControlDatosSalesianos control = new ControlDatosSalesianos();
                        return control.sp_adminfe_front_getDatosSalesianos(ip, idPagina);
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
                public VersionDocumentoOficial sp_adminfe_front_getDocumentosOficialesById(int idVersion ,string idioma, string ip, int idPagina)
                {
                    try
                    {
                        ControlVersionDocumentoOficial control = new ControlVersionDocumentoOficial();
                        return control.sp_adminfe_front_getDocumentosOficialesById(idVersion,idioma, ip, idPagina);
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
                public DatosIUS sp_adminfe_front_getDatosIUS(string ip,int idPagina)
                {
                    try
                    {
                        ControlDatosIUS control = new ControlDatosIUS();
                        return control.sp_adminfe_front_getDatosIUS(ip, idPagina);
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
            #region "Set"
                
            #endregion
        #endregion
    }
}