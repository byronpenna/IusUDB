function cbIdioma(idIdioma) {
    frm = { idIdioma: idIdioma };
    console.log("formulario a enviar es: ", frm);
    actualizarCatalogo(RAIZ + "Home/sp_trl_getIdiomaFromIds", frm, function (data) {
        console.log("La respuesta del servidor es: ", data);
        if (data.estado) {

        } else {
            alert("ocurrio un error");
        }
    });
}