// scripts
    function frmInvitado(frm) {
        //var frm = {};
        actualizarCatalogo(RAIZ + "/Login/sp_adminfe_front_getLogin", frm, function (data) {
            console.log("La data es", data);
            if (data.estado) {
                window.location = RAIZ + "Home/index";
            } else {
                var mjs = "";
                if (data.error !== undefined && data.error != null && data.error._mostrar) {
                    mjs = data.error.Message;
                } else {
                    mjs = "Error no controlado";
                }
                var targetMessage = $(".divMessageLogin");
                printMessage(targetMessage, mjs, false);
            }
        })
    }