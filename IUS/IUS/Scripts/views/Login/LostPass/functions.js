// scripts 
    function frmInvitado(frm) {
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_solicitarCambio", frm, function (data) {
            console.log("La data devuelta es", data);
            if (data.estado) {

            }
        })
    }
    function cambiarPass() {
        actualizarCatalogo(RAIZ + "/Login/sp_secpu_cambiarPassPublico", frm, function (data) {
            console.log("La data devuelta es: ", data);
            if (data.estado) {

            }
        })
    }