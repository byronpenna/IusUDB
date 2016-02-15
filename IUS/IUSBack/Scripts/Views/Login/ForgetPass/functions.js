function frmInvitado(frm) {
    var target = $(".divMessageLogin");
    actualizarCatalogo(RAIZ + "Login/sp_usu_cambiarPassUsuario", frm, function (data) {
        console.log("La respuesta fue", data);
        var mjs = "";
        if (data.estado) {
            mjs = "Contraseña fue cambiada exitosamente";
        } else {
            if (data.error !== undefined && data.error != null && data.error._mostrar) {
                mjs = data.error.Message;
            } else {
                mjs = "Ocurrio un error";
            }
        }
        printMessage(target,mjs, data.estado);
    }, function () {
        target.empty().append("\
            <div class='row marginNull'>\
                Espere por favor\
            </div>\
            <img src='"+ IMG_GENERALES + "ajax-loader.gif'>\
        ");
    })
}