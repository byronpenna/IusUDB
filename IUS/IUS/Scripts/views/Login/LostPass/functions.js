// scripts 
    function frmInvitado(frm) {
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_solicitarCambio", frm, function (data) {
            console.log("La data devuelta es", data);
        })
    }