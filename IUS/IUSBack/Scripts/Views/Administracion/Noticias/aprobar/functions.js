function btnCambiarEstado(frm,tr) {
    actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_cambiarEstado", frm, function (data) {
        console.log("La respuesta del servidor", data);
    })
}