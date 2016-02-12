function frmInvitado(frm) {
    actualizarCatalogo(RAIZ + "Login/sp_usu_cambiarPassUsuario", frm, function (data) {
        console.log("La respuesta fue", data);
        if (data.estado) {

        }
    })
}