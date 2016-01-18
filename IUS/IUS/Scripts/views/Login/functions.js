// scripts
    function frmInvitado(frm) {
        actualizarCatalogo(RAIZ + "/Login/sp_adminfe_front_getLogin", frm, function (data) {
            console.log("La data es", data);
        })
    }