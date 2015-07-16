using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// librerias externas
    using IUSLibs.SEC.Control;
    using IUSLibs.SEC.Entidades;
    using IUSLibs.LOGS;
namespace IUSBack.Models.General
{
    public class PadreModel
    {
        #region "propiedades"
            protected ErroresIUS _errorIUS; // aun no se a usado en nada.
            public enum permisos
            {
                Crear,Editar,Eliminar,Ver
            }
            private permisos _permiso;
            
            #region "Permisos"
                public permisos permiso
                {
                    get
                    {
                        return this._permiso;
                    }
                    set
                    {
                        this._permiso = value;
                    }
                }
            #endregion
        #endregion
        #region "funciones publicas"
            #region "menu"
                #region "privadas"
                    private List<Submenu> getMenuCompleto(int idUsuario)
                    {
                        List<Submenu> subMenu;
                        ControlUsuarios control = new ControlUsuarios();
                        control.getTodoMenu(idUsuario);
                        subMenu = control.getSubMenu;
                        return subMenu;
                    }
                    private String abrirMenu(Submenu sub)
                    {
                        String toReturn =
                            "<li>" +
                                "<a href='" + sub._menu._enlace + "'>" +
                                    sub._menu._menu +
                                "</a>";
                        return toReturn;
                    }
                    private String abrirMenu(Submenu sub, String subMenuString)
                    {
                        String toReturn =
                            "<li>" +
                                "<a href='" + sub._menu._enlace + "'>" +
                                    sub._menu._menu +
                                "</a><ul>" + subMenuString;
                        return toReturn;
                    }
                #endregion
                public List<Menu> sp_sec_getMenu(int idUsuario)
                {
                    try
                    {
                        ControlMenu control = new ControlMenu();
                        return control.sp_sec_getMenu(idUsuario);
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
                public List<Submenu> sp_sec_getSubmenu(int idMenu, int idUsuario)
                {
                    try
                    {
                        ControlSubMenu control = new ControlSubMenu();
                        return control.sp_sec_getSubmenu(idMenu, idUsuario);
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
                public String getMenuUsuario(int idUsuario)
                {
                    List<Submenu> subMenu = this.getMenuCompleto(idUsuario);
                    String toReturn = "<ul>";
                    int menuActual = -1;
                    bool abierto = false;
                    String subMenuString;
                    foreach (Submenu sub in subMenu)
                    {
                        subMenuString = "<li>" +
                                            "<a href='" + sub._enlace + "'>" + sub._textoSubMenu + "</a>" +
                                        "</li>";
                        if (menuActual != sub._menu._idMenu)
                        {

                            if (!abierto)
                            {
                                toReturn += this.abrirMenu(sub, subMenuString);
                                abierto = true;
                            }
                            else
                            {
                                toReturn +=
                                    "</ul>" +
                                "</li>";
                                abierto = false;
                            }
                            menuActual = sub._menu._idMenu;

                        }
                        else
                        {
                            if (abierto)
                            {
                                toReturn += subMenuString;
                            }
                            else
                            {
                                toReturn += this.abrirMenu(sub, subMenuString);
                                abierto = true;
                            }

                        }
                        if (menuActual == sub._menu._idMenu && !abierto)
                        {
                            toReturn += this.abrirMenu(sub, subMenuString);
                            abierto = true;
                        }
                    }
                    toReturn += "</ul>"+"<li><a id='menuLogOut' href='#'>Cerrar sesion</a></li>"+"</ul>";
                    return toReturn;
                }
            #endregion
            #region "Permisos"
                private int getNumPermiso(permisos varPermiso)
                    {
                        int toReturn;
                        switch (varPermiso)
                        {
                            case permisos.Crear:
                                {
                                    toReturn = 1;
                                    break;
                                }
                            case permisos.Editar:
                                {
                                    toReturn = 2;
                                    break;
                                }
                            case permisos.Eliminar:
                                {
                                    toReturn = 3;
                                    break;
                                }
                            case permisos.Ver:
                                {
                                    toReturn = 4;
                                    break;
                                }
                            default:
                                {
                                    toReturn = -1;
                                    break;
                                }
                        }
                        return toReturn;
                    }
                public Permiso sp_trl_getAllPermisoPagina(int idUsuarioEjecutor, int idPagina)
                {
                    Permiso permisos = null;
                    ControlPermiso control = new ControlPermiso();
                    try
                    {
                        permisos = control.sp_trl_getAllPermisoPagina(idUsuarioEjecutor, idPagina);
                    }
                    catch (ErroresIUS x)
                    {
                        throw x;
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                    return permisos;
                }
                public bool tienePermiso(int idUsuario, int idPagina, permisos nivelPermiso)
                    {
                        bool toReturn = false;
                        int idPermiso = this.getNumPermiso(nivelPermiso);
                        ControlUsuarios control = new ControlUsuarios();
                        if (idPermiso != -1)
                        {
                            toReturn = control.permisoPagina(idUsuario, idPagina, idPermiso);
                        }
                        return toReturn;
                    }   
            #endregion
            #region "genericas"
               
            #endregion
        #endregion
    }
}