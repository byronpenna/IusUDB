// scripts 
    function frmInvitado(frm) {
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_solicitarCambio", frm, function (data) {
            console.log("La data devuelta es", data);
            if (data.estado) {

            }
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