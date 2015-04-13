using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// librerias 
    using IUSLibs.SEC.Entidades;
    using IUSLibs.SEC.Control;
    using IUSLibs.LOGS;
    
namespace IUSBack.Models.Page.GestionRolSubmenu.Acciones
{
    public class GestionRolSubmenuModel
    {
        #region "propiedades"
            public int _idPagina;
        #endregion
        #region "funciones publicas"
            #region "acciones"
                    public bool eliminarRolSubmenu(int idSubmenu, int idRol, int idUsuarioEjecutor)
                    {
                        return this.eliminarRolSubmenu(idSubmenu, idRol, idUsuarioEjecutor, this._idPagina);
                    }
                    public bool eliminarRolSubmenu(int idSubmenu,int idRol,int idUsuarioEjecutor,int idPagina)
                    {
                        ControlRolSubmenu control = new ControlRolSubmenu();
                        bool toReturn = false;
                        try
                        {
                            toReturn = control.eliminarRolSubmenu(idSubmenu, idRol, idUsuarioEjecutor, idPagina);

                        }
                        catch (ErroresIUS)
                        {

                        }
                        catch (Exception)
                        {

                        }
                        return toReturn;
                    }
                #region "agregar
                    #region "agregarRolSubMenu"
                        public bool agregarRolSubMenu(int idRol,int[] idSubmenus,int idUsuarioEjecutor)
                        {
                            return this.agregarRolSubMenu(idRol, idSubmenus, idUsuarioEjecutor, this._idPagina);
                        }
                        public bool agregarRolSubMenu(int idRol, int[] idSubmenus, int idUsuarioEjecutor,int idPagina)
                        {
                            bool toReturn = false;
                            ControlRolSubmenu control = new ControlRolSubmenu();
                            try
                            {
                                toReturn = control.agregarRolSubMenu(idRol, idSubmenus, idUsuarioEjecutor, idPagina);
                            }
                            catch (ErroresIUS)
                            {

                            }
                            catch (Exception)
                            {

                            }
                            return toReturn;
                        }
                    #endregion
                #endregion
            #endregion
        #endregion
        #region "contructores"
            public GestionRolSubmenuModel()
            {

            }
            public GestionRolSubmenuModel(int idPagina)
            {
                this._idPagina = idPagina;
            }
        #endregion
    }
}