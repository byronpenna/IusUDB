// scripts
    function frmInvitado(frm) {
        //var frm = {};
        actualizarCatalogo(RAIZ + "/Login/sp_adminfe_front_getLogin", frm, function (data) {
            console.log("La data es", data);
        })
    }