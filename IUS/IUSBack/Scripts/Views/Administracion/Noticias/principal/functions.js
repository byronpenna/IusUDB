function btnPublicacion(frm, tr) {
    console.log("formulario a enviar es: ", frm);
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_noticias_cambiarEstadoPost", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
        if (data.estado) {

        } else {
            alert("Ocurrio un error");
        }
    })
}