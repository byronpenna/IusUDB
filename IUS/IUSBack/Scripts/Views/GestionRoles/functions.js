function changeRolTab2(frm) {
    console.log("formulario a enviar", frm);
    cargarObjetoGeneral("GestionRoles/getJSONSubmenuFaltanteYactuales", frm, function (data) {
        if (data.estado) {
            console.log("la respuesta del server es:", data);
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
        console.log("la data devuelta es:", data);
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