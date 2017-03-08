function btnAprobar(frm) {

    actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_cambiarEstado", frm, function (data) {
        console.log("La respuesta del servidor", data);
        if (data.estado) {
            alert("Publicada correctamente");
        } else {
            alert("Ocurrio un error");
        }
    })
}