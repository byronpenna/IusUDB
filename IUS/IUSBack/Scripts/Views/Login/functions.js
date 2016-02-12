function aOlvidoContra(frm) {
    console.log("Entro aqui");
    var url = RAIZ + "/Login/sp_usu_solicitarCambioPass";
    //console.log("url",url);
    var target = $(".spanMensajes");
    actualizarCatalogo(url, frm, function (data) {
        console.log("La data devuelta es:", data);
        var mjs = "";
        if (data.estado) {
            mjs = "Correo electronico de reestablecimiento enviado correctamente"
        } else {
            if (data.error !== undefined && data.error != null && data.error._mostrar) {
                mjs = data.error.Message;
            } else {
                mjs = "Error no controlado";
            }
        }
        printMessage(target, mjs, data.estado);
    }, function () {
        target.empty().append("\
            <div class='row marginNull'>\
                Espere por favor\
            </div>\
            <img src='"+IMG_GENERALES+"ajax-loader.gif'>\
        ");
    })
}
//#################
function cambiarActivePestania(pestania) {
    $(".btnTabsLogin").removeClass("activeMenu");
    switch (pestania) {
        case 'admin': {
            $("#btnAdminLogin").addClass("activeMenu");
            break;
        }
        case 'usuario': {
            $("#btnUsuarioLogin").addClass("activeMenu");
            break;
        }
    }
}