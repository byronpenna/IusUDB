// scripts 
    function frmInvitado(frm) {
        var target = $(".divMessageLogin");
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_solicitarCambio", frm, function (data) {
            console.log("La data devuelta es", data);
            if (data.estado) {
                mjs = "Correo enviado correctamente, revise su bandeja de entrada";
                $(".txtEmail").val("");
            } else {
                if (data.error !== undefined && data.error != null && data.error._mostrar) {
                    mjs = data.error;
                } else {
                    mjs = "Ocurrio un error";
                }
            }
            printMessage(target, mjs, data.estado);
        }, function () {
            target.empty().append("Espere por favor \
                        <div class='row marginNull'>\
                            <img src='" + IMG_GENERALES + "ajax-loader.gif'/>\
                        </div>\
            ");
        })
    }
    function cambiarPass(frm) {
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_cambiarPassPublico", frm, function (data) {
            console.log("La data devuelta es: ", data);
            if (data.estado) {
                printMessage($(".divMessageLogin"), "Clave cambiada correctamente", true);
            } else {
                var mjs = "Error no controlado";
                if (data.error !== undefined && data.error != null && data.error._mostrar) {
                    mjs = data.error.Message;
                }
                printMessage($(".divMessageLogin"), mjs, false);
            }
        })
    }