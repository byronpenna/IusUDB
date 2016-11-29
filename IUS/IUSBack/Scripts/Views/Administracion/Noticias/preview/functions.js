function btnEnviarSolicitud(frm){
    actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/ajax_rechazar", frm, function (data) {
        console.log("La respuesta del servidor", data);
        if (data.estado) {
            
        }
    })
}