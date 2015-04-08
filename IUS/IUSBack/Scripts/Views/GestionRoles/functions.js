
function doTablaRolesUsuario(roles) {
    tbody = "";
    $.each(roles, function (i, value) {
        tbody += "\
            <tr class='trEstadoRol'>\
                <td>" + value._rol + "</td>\
                <td>" + value.stringEstado + "</td>\
                <td><i class='fa fa-times'></i></td>\
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