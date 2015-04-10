function clickIcoQuitarPermiso(trPermiso) {
    var frm = new Object();
    //frm.idPermisoRol = trPermiso. 
}
function clickTrSubMenu(trSubMenu) {
    frm = new Object();
    frm.idSubMenu = trSubMenu.find(".txtIdSubMenu").val();
    frm.idRol = $(".cbRolTab2").val();
    cambioBackgroundColorTr(".trSubMenu", "yellow", ".activeTr");
    console.log("el formulario a enviar es", frm);
    cargarObjetoGeneral("GestionRoles/getJSONPermisos", frm, function (data) {
        console.log("La respuesta del servidor es", data);
        if (data) {
            if ( !(data.permisos === null) ) {
                tbody = llenarTablaPermisos(data.permisos.arrPermisos);
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
    console.log("los permisos son:",permisos);
    $.each(permisos, function (i, val) {
        tbody += "\
            <tr>\
                <td>"+val+"</td>\
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
    frm.idSubMenu = trSubMenu.find(".txtIdSubMenu").val();
    console.log("formulario a enviar", frm);
    /*cargarObjetoGeneral("GestionRoles/getJSONSubmenuFaltanteYactuales", frm, function () {
    });*/
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
function agregarRoles(frm) {
    cargarObjetoGeneral("GestionRoles/agregarRoles", frm, function (data) {
        console.log("la data del server:", data);
        if (data.estado) {
            tbody = doTablaRolesUsuario(data.roles);
            $(".tbodyRolUsuario").empty().append(tbody);
        } else {
            alert("Error al ingresar los datos");
        }
    });
}
function desasociarRol(frm, trUsuarioRol) {
    actualizarCatalogo("/GestionRoles/desasociarRolUsuario", frm, function (data) {
        if (data) {
            trUsuarioRol.remove();
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
                <td><i class='fa fa-times iconQuitarRol'></i></td>\
            </tr>\
        "
    });
    return tbody;
}
function getOptionsRoles(roles) {
    options = "";
    $.each(roles, function (i, value) {
        options += "<option value='"+value._idRol+"'>"+value._rol+"</option>";
    });
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
        console.log("Respuesta de roles faltantes", data);
        if (data.estado) {
            roles = data.roles;
            optionCbRoles = getOptionsRoles(roles);
            $(".cbRoles").empty().append(optionCbRoles);
            resetChosen($(".cbRoles"));
        }
        
    });
}