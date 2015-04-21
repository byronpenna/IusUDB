function btnAsignarSubmenu(frm) {
    cargarObjetoGeneral("GestionRoles/agregarRolSubMenu", frm, function (data) {
        console.log("la respuesta del servidor es despues de agregar:", data);
        if (data.estado) {
            tbody = llenarTablaSubMenuRol(data.submenus);
            options = llenarCbSubmenuRol(data.submenuFaltante);
            $(".tbodySubmenuActuales").empty().append(tbody);
            $(".cbSubMenu").empty().append(options);
            resetChosen($(".cbSubMenu"));
        } else {
            alert("ocurio un error al tratar de ingresar");
        }
    })
}
function btnAsignarPermiso(frm) {
    cargarObjetoGeneral("GestionRoles/agregarPermisoSubmenuRol", frm, function (data) {
        if (data.estado) {
            tbody = llenarTablaPermisos(data.rolSubMenuPermiso);
            options = llenarSelectPermisos(data.permisosFaltantes);
            $(".tbodyTbPermisos").empty().append(tbody);
            $(".cbAsignarPermisos").empty().append(options);
            resetChosen($(".cbAsignarPermisos"));
        } else {
            alert("Ocurio un error al tratar de ingresar");
        }
    });
}
function clickIcoQuitarPermiso(trPermiso) {
    var frm = new Object();
    frm.idRolSubmenuPermiso = trPermiso.find(".txtHdIdRolSubMenuPermiso").val();
    cargarObjetoGeneral("GestionRoles/eliminarPermisoSubmenuRol", frm, function (data) {
        if (data.estado) {
            trPermiso.remove();
        } else {
            alert("ocurrio un error al tratar de eliminar");
        }
    });
}
function llenarSelectPermisos(permisos) {
    options = "";
    if (!(permisos === null)) {
        $.each(permisos, function (i, val) {
            options += "<option value='" + val._idPermisoRol + "'>" + val._permiso + "</option>";
        });
    } else {
        options = "<option value='-1' disabled selected>No hay permisos para agregar</option>";
    }
    return options;
}
function clickTrSubMenu(trSubMenu) {
    frm = new Object();
    frm.idSubMenu = trSubMenu.find(".txtIdSubMenu").val();
    frm.idRol = $(".cbRolTab2").val();
    cambioBackgroundColorTr(".trSubMenu", "yellow", ".activeTr");
    cargarObjetoGeneral("GestionRoles/getJSONPermisos", frm, function (data) {
        
        if (data) {
            options = llenarSelectPermisos(data.permisosFaltantes);
            $(".cbAsignarPermisos").empty().append(options);
            resetChosen($(".cbAsignarPermisos"));
            if ( !(data.permisos === null) ) {
                tbody = llenarTablaPermisos(data.permisos);
            } else {
                tbody = "\
                    <tr>\
                        <td class='text-center' colspan='2'>No posee ningun permiso</td>\
                    </tr>\
                    ";
            }
        } else {
            tbody = "\
                    <tr class='text-center'>\
                        <td colspan='2'>Ocurrio un Error al recoger permisos</td>\
                    </tr>\
                    "
        }
        $(".tbodyTbPermisos").empty().append(tbody);
    });
}
function llenarTablaPermisos(permisos) {
    tbody = "";
    $.each(permisos, function (i, val) {
        tbody += "\
            <tr>\
                <td class='hidden'><input class='txtHdIdRolSubMenuPermiso' value='" + val._idRolSubMenuPermiso + "'></td>\
                <td>" + val._permisoRol._permiso + "</td>\
                <td><i class='fa fa-times pointer icoQuitarPermiso'></td>\
            </tr>\
        ";
    });
    return tbody;
}
function llenarTablaSubMenuRol(submenu) {
    tbody = "";
    if ( !(submenu === null) ) {
        $.each(submenu, function (i, val) {
            tbody += "\
           <tr class='trSubMenu'>\
                <td class='hidden'>\
                    <input type='hidden' class='txtIdSubMenu' value='" + val._idSubMenu + "'>\
                </td>\
                <td>" + val._menu._menu + "</td>\
                <td>" + val._textoSubMenu + "</td>\
                <td>" + val._enlace + "</td>\
                <td><i class='fa fa-times pointer icoQuitarSubMenu'></td>\
           </tr>\
        ";
        });
    } else {
        tbody = "\
            <tr>\
                <td colspan='4' class='text-center'>No posee ningun sub menu asignado</td>\
            </tr>\
        ";
    }
    return tbody;
}
function quitarSubMenuArol(trSubMenu) {
    frm = new Object();
    frm.idSubMenu   = trSubMenu.find(".txtIdSubMenu").val();
    frm.idRol = $(".cbRolTab2").val();
    console.log("formulario a enviar",frm);
    cargarObjetoGeneral("GestionRoles/eliminarRolSubmenu", frm, function (data) {
        console.log("la data devuelta por el servidor es: ", data);
        if (data.estado) {
            trSubMenu.remove();
            // limpiar cb 
            options = llenarCbSubmenuRol(data.submenuFaltante);
            $(".cbSubMenu").empty().append(options);
            resetChosen($(".cbSubMenu"));
        } else {
            alert("ocurrio un error al tratar de quitar submenu");
        }
    });
}
function llenarCbSubmenuRol(submenu) {
    opcion = "";
    if (!(submenu === null)) {
        $.each(submenu, function (i, val) {
            opcion += "\
                <option value='" + val._idSubMenu + "'>" + val._textoSubMenu + "</option>\
            ";
        });
    } else {
        opcion += "\
            <option value='-1' disabled>No hay submenus para agregar</option>\
        ";
    }
    return opcion;
}
function changeRolTab2(frm) {
    cargarObjetoGeneral("GestionRoles/getJSONSubmenuFaltanteYactuales", frm, function (data) {
        if (data.estado) {
            actuales    = data.subMenusActuales;
            tbody       = llenarTablaSubMenuRol(actuales);
            faltantes   = data.subMenusFaltantes;
            opciones    = llenarCbSubmenuRol(faltantes);
            $(".tbodySubmenuActuales").empty().append(tbody);
            $(".cbSubMenu").empty().append(opciones);
            // resetear chosen
            resetChosen($(".cbSubMenu"));
        } else {
            alert("Error en la transaccion");
        }
    });
}
// este le asigna los roles al usuario
function agregarRoles(frm) {
    cargarObjetoGeneral("GestionRoles/agregarRoles", frm, function (data) {
        if (data.estado) {
            console.log("respuesta del servidor", frm);
            tbody = doTablaRolesUsuario(data.roles);
            $(".tbodyRolUsuario").empty().append(tbody);
            options = getOptionsRoles(data.rolesFaltantes);
            $(".cbRoles").empty().append(options);
            resetChosen($(".cbRoles"));
        } else {
            alert("Error al ingresar los datos");
        }
    });
}

function desasociarRol(frm, trUsuarioRol) {
    actualizarCatalogo("/GestionRoles/desasociarRolUsuario", frm, function (data) {
        if (data) {
            trUsuarioRol.remove();
            // actualizar cbRoles
            optionCbRoles = getOptionsRoles(data.rolesFaltantes);
            $(".cbRoles").empty().append(optionCbRoles);
            resetChosen($(".cbRoles"));
        }
    });
}
function doTablaRolesUsuario(roles) {
    tbody = "";
    $.each(roles, function (i, value) {
        tbody += "\
            <tr class='trEstadoRol'>\
                <td class='hidden'><input type='hidden' class='txtIdRol' value='" + value._idRol + "' /></td>\
                <td>" + value._rol + "</td>\
                <td>" + value.stringEstado + "</td>\
                <td><i class='fa fa-times pointer iconQuitarRol '></i></td>\
            </tr>\
        "
    });
    return tbody;
}
function getOptionsRoles(roles) {
    options = "";
    if (!(roles === null)) {
        $.each(roles, function (i, value) {
            options += "<option value='" + value._idRol + "'>" + value._rol + "</option>";
        });
    } else {
        options = "<option value='-1' disabled>No hay roles para asignar</option>";
    }
    return options;
}
function llenarTablaRolesUsuario(idUsuario) {
    frm = new Object();
    frm.idUsuario = idUsuario;
    cargarObjetoGeneral("GestionRoles/getJSONroles", frm, function (data) {
        //console.log(data);
        roles = data.roles
        tbody = doTablaRolesUsuario(roles);
        $(".tbodyRolUsuario").empty().append(tbody);
    })
    cargarObjetoGeneral("GestionRoles/getJSONRolesFaltantes", frm, function (data) {
        if (data.estado) {
            roles = data.roles;
            optionCbRoles = getOptionsRoles(roles);
            $(".cbRoles").empty().append(optionCbRoles);
            resetChosen($(".cbRoles"));
        }
        
    });
}
// genericas
    function getTrRol(rol,permisos){
        tr = "\
            <tr>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdRol' class='txtHdIdRol' value='"+ rol._idRol + "' />\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtRol' class='txtRol form-control'  />\
                    </div>\
                    <div class='normalMode tdRol' >\
                        "+ rol._rol + "\
                    </div>\
                </td>\
                <td>\
                    <div class='normalMode'>\
                        "+ rol.stringEstado + "\
                    </div>\
                </td>\
                <td>\
                    <button class='btn btn-xs btnEditar' " + permisos.stringEditar + ">\
                        Editar\
                    </button>\
                    <button class='btn btn-xs btnEliminar' " + permisos.stringEliminar + ">Eliminar</button>\
                    <button class='btn btn-xs btnDeshabilitar' " + permisos.stringEditar + " >\
                        "+rol.txtBtnHabilitar+"\
                    </button>\
                </td>\
            </tr>\
        ";
        return tr;
    }
    function setTrRol(tr,rol) {
        tr.find(".tdRol").empty().append(rol._rol);
        tr.find(".txtRol").val("");
    }
    // hace un insert directo a la tabla roles
    function agregarRol(frm,tbody,trInsert) {
        cargarObjetoGeneral("GestionRoles/sp_sec_addRol", frm, function (data) {
            console.log("la respuesta del server es", data)
            if (data.estado) {
                rol = data.rol;
                tr = getTrRol(rol, data.permisos);
                tbody.prepend(tr);
                clearTr(trInsert);
                $(".txtEstado").val("Activo");
                addOptionSelect($(".cbRolTab2"), rol._idRol, rol._rol, true);
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function eliminarRol(frm,tr) {
        cargarObjetoGeneral("GestionRoles/sp_sec_eliminarRol", frm, function (data) {
            if (data.estado) {
                removeOptionSelect($(".cbRolTab2"), frm.txtHdIdRol, true);
                tr.remove();
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    
// acciones script 
    function btnDeshabilitar(tr) {
        frm = serializeSection(tr);
        cargarObjetoGeneral("GestionRoles/sp_sec_cambiarEstadoRol", frm, function (data) {
            if (data.estado) {
                rol = data.rol;
                tr.find(".tdEstadoRol").empty().append(rol.stringEstado);
                tr.find(".btnDeshabilitar").empty().append(rol.txtBtnHabilitar);
                if (rol._estado) {
                    addOptionSelect($(".cbRolTab2"), rol._idRol, rol._rol, true);
                } else {
                    removeOptionSelect($(".cbRolTab2"), rol._idRol,true);
                }
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function btnActualizar(tr) {
        frm = serializeSection(tr);
        cargarObjetoGeneral("GestionRoles/sp_sec_editarRol", frm, function (data) {
            if (data.estado) {
                rol = data.rol;
                setTrRol(tr, rol);
                updateOptionSelect($(".cbRolTab2"), rol._idRol, rol._rol, true);
                controlesEdit(false, tr);
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function btnEditar(tr) {
        txtRol = tr.find(".tdRol").text();
        tr.find(".txtRol").val(txtRol);
    }
    function btnEliminar(tr) {
        frm = serializeSection(tr);
        console.log("el formulario a enviar es:", frm);
        eliminarRol(frm,tr);
    }
    function btnAgregarRol(tr) {
        frm = serializeSection(tr);
        tbody = tr.parents("table").find("tbody");
        console.log("el formulario a enviar es", frm);
        agregarRol(frm,tbody,tr);
    }